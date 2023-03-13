using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;

using DrivenAdapters.Mongo.DbModels;

using MongoDB.Driver;
using System.Collections.Immutable;
using System.Diagnostics;
using MongoDB.Bson;

namespace DrivenAdapters.Mongo.Adapters;

public class RepositorioDeClientesAdapter : IRepositorioDeClientesGateway
{
    private readonly IMongoCollection<DocumentoCliente> _collecionDeClientes;

    private readonly IMapper _mapper;

    public RepositorioDeClientesAdapter(IContext db, IMapper mapper)
    {
        _collecionDeClientes = db.Clientes;

        _mapper = mapper;
    }

    public async Task<Credito> AnexarCreditoAClienteAsync(Cliente cliente, Credito credito)
    {
        DocumentoCredito documentoCredito = _mapper.Map<DocumentoCredito>(credito);
        documentoCredito.Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
        FilterDefinition<DocumentoCliente> filter = Builders<DocumentoCliente>.Filter
            .Eq(c => c.Id, cliente.Id);
        UpdateDefinition<DocumentoCliente> update = Builders<DocumentoCliente>.Update
            .AddToSet(c => c.Creditos, documentoCredito);

        await _collecionDeClientes.UpdateOneAsync(filter, update);

        return _mapper.Map<Credito>(documentoCredito);
    }

    public async Task<Credito> AnexarCuotaACreditoAsync(Cliente cliente, Credito credito, Pago pago)
    {
        pago.Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
        ImmutableList<DocumentoCredito> creditosDelCliente = cliente.Creditos
            .Select(x =>
            {
                if (x.Id == credito.Id) x.PagosRealizados.Add(pago);
                return _mapper.Map<DocumentoCredito>(x);
            })
            .ToImmutableList();
        FilterDefinition<DocumentoCliente> documentFilter = Builders<DocumentoCliente>
            .Filter
            .Eq(c => c.Id, cliente.Id);
        UpdateDefinition<DocumentoCliente> documentChanges = Builders<DocumentoCliente>
            .Update
            .Set(
                cliente => cliente.Creditos,
                creditosDelCliente
            );

        await _collecionDeClientes.UpdateOneAsync(documentFilter, documentChanges);
        DocumentoCredito creditoModificado = creditosDelCliente
            .FirstOrDefault(x => x.Id == credito.Id)!;

        return _mapper.Map<Credito>(creditoModificado);
    }

    public async Task<bool> ClienteConDocumentoDeIdentidadExisteAsync(string documentoDeIdentidad)
    {
        IAsyncCursor<DocumentoCliente> results = await _collecionDeClientes
            .FindAsync((c => c.DocumentoDeIdentidad == documentoDeIdentidad));
        return await results.AnyAsync();
    }

    public async Task<Cliente> GuardarAsync(Cliente cliente)
    {
        DocumentoCliente documentoCliente = _mapper.Map<DocumentoCliente>(cliente);
        await _collecionDeClientes.InsertOneAsync(documentoCliente);

        return _mapper.Map<Cliente>(documentoCliente);
    }

    public async Task<Cliente> ObtenerPorDocumentoDeIdentidadAsync(string documentoDeIdentidad)
    {
        IAsyncCursor<DocumentoCliente> results = await _collecionDeClientes
            .FindAsync((c => c.DocumentoDeIdentidad == documentoDeIdentidad));
        DocumentoCliente cliente = await results.FirstOrDefaultAsync();

        return cliente == null ? null : _mapper.Map<Cliente>(cliente);
    }

    public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
    {
        IAsyncCursor<DocumentoCliente> cursor = await _collecionDeClientes
            .FindAsync(Builders<DocumentoCliente>.Filter.Empty);

        return cursor
            .ToList()
            .Select(doc => _mapper.Map<Cliente>(doc))
            .ToList();
    }
}