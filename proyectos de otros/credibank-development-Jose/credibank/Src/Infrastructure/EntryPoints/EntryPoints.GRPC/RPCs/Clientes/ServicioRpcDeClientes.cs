using AutoMapper;

using Domain.Model.Interfaces.UseCases;

using EntryPoints.Commons.DTOs.Comandos;
using EntryPoints.Commons.DTOs.Validaciones;

using GrpcModels = EntryPoints.GRPC.Protos;

using FluentValidation;

using Grpc.Core;
using DomainModels = Domain.Model.Entities;
using Microsoft.Extensions.Options;
using Helpers.ObjectsUtils;
using EntryPoints.GRPC.RPCs.Base;

using EntryPoints.GRPC.Protos;

namespace EntryPoints.GRPC.Controllers.Clientes;

public class ServicioRpcDeClientes : GrpcModels.ServicioDeClientes.ServicioDeClientesBase
{
    private readonly ICrearClienteUseCase _crearCliente;
    private readonly IObtenerClientePorDocumentoDeIdentidadUseCase _obtenerClientePorDocumentoDeIdentidad;
    private readonly ISolicitarCreditoUseCase _solicitarCredito;
    private readonly IPagarCuotasUseCase _pagarCuota;

    private readonly IMapper _mapper;
    private readonly decimal _tasaDeInteresEfectivoAnual;

    public ServicioRpcDeClientes(ICrearClienteUseCase crearCliente, ISolicitarCreditoUseCase solicitarCredito, IPagarCuotasUseCase pagarCuota, IObtenerClientePorDocumentoDeIdentidadUseCase obtenerClientePorDocumentoDeIdentidad, IMapper mapper, IOptions<ConfiguradorAppSettings> appSettings)
    {
        _crearCliente = crearCliente;
        _solicitarCredito = solicitarCredito;
        _pagarCuota = pagarCuota;
        _obtenerClientePorDocumentoDeIdentidad = obtenerClientePorDocumentoDeIdentidad;

        _tasaDeInteresEfectivoAnual = appSettings.Value.TasaDeInteresEfectivoAnual;
        _mapper = mapper;
    }

    public override Task<StandardResponse> CrearCliente(GrpcModels.CrearClienteRequest request, ServerCallContext context) => RPCServiceHandler.ProcessResponse(
        async () =>
        {
            CreacionDeCliente validaciones = new();
            CrearCliente dto = _mapper.Map<CrearCliente>(request);
            await validaciones.ValidateAndThrowAsync(dto);

            DomainModels.Cliente clienteGuardado = await _crearCliente.Crear(
                _mapper.Map<DomainModels.Cliente>(dto)
            );
            return _mapper.Map<GrpcModels.Cliente>(clienteGuardado);
        }
    );

    public override Task<StandardResponse> ObtenerClientePorDocumentoDeIdentidad(GrpcModels.DocumentoDeIdentidad request, ServerCallContext context) => RPCServiceHandler.ProcessResponse(
        async () =>
        {
            DomainModels.Cliente cliente = await _obtenerClientePorDocumentoDeIdentidad.Obtener(request.DocumentoDeIdentidad_);
            return _mapper.Map<GrpcModels.Cliente>(cliente);
        }
    );

    public override Task<StandardResponse> PagarCuota(GrpcModels.PagarCuotaRequest request, ServerCallContext context) => RPCServiceHandler.ProcessResponse(
        async () =>
        {
            PagoDeCuota validaciones = new();
            PagarCuota dto = _mapper.Map<PagarCuota>(request);
            await validaciones.ValidateAndThrowAsync(dto);

            DomainModels.Credito creditoModificado = await _pagarCuota.Pagar(
                request.DocumentoDeIdentidad,
                request.IdDelCredito,
                _mapper.Map<DomainModels.Pago>(dto)
            );

            return _mapper.Map<GrpcModels.Credito>(creditoModificado);
        }
    );

    public override Task<StandardResponse> SolicitarCredito(GrpcModels.SolicitarCreditoRequest request, ServerCallContext context) => RPCServiceHandler.ProcessResponse(
        async () =>
        {
            SolicitudDeCredito validaciones = new();
            SolicitarCredito dto = _mapper.Map<SolicitarCredito>(request);
            await validaciones.ValidateAndThrowAsync(dto);

            DomainModels.Credito creditoSolicitado = await _solicitarCredito.Solicitar(
                request.DocumentoDeIdentidad,
                _mapper.Map<DomainModels.Credito>(dto),
                _tasaDeInteresEfectivoAnual
            );

            return _mapper.Map<GrpcModels.Credito>(creditoSolicitado);
        }
    );
}