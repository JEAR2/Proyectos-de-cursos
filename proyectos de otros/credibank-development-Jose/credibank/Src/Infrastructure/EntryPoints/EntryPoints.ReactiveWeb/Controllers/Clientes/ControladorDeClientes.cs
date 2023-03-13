using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;

using Domain.Model.Entities;
using Domain.Model.Interfaces.UseCases;

using EntryPoints.ReactiveWeb.Base;

using Microsoft.AspNetCore.Mvc;
using EntryPoints.Commons.DTOs.Respuestas;
using EntryPoints.Commons.DTOs.Comandos;
using EntryPoints.Commons.DTOs.Validaciones;
using Microsoft.Extensions.Options;
using Helpers.ObjectsUtils;

namespace EntryPoints.ReactiveWeb.Controllers.Clientes;

[ApiController]
[Route("clientes")]
public class ControladorDeClientes : AppControllerBase<ControladorDeClientes>
{
    private readonly ICrearClienteUseCase _crearCliente;
    private readonly IObtenerTodosLosClientesUseCase _obtenerTodosLosClientes;
    private readonly IObtenerClientePorDocumentoDeIdentidadUseCase _obtenerClientePorDocumentoDeIdentidad;
    private readonly ISolicitarCreditoUseCase _solicitarCredito;
    private readonly IPagarCuotasUseCase _pagarCuota;

    private readonly IMapper _mapper;

    private readonly decimal _tasaDeInteresEfectivoAnual;

    public ControladorDeClientes(ISolicitarCreditoUseCase solicitarCredito, IPagarCuotasUseCase pagarCuota, ICrearClienteUseCase crearCliente, IObtenerTodosLosClientesUseCase obtenerTodosLosClientes, IObtenerClientePorDocumentoDeIdentidadUseCase obtenerClientePorDocumentoDeIdentidad, IMapper mapper, IOptions<ConfiguradorAppSettings> appSettings)
    {
        _crearCliente = crearCliente;
        _obtenerTodosLosClientes = obtenerTodosLosClientes;
        _obtenerClientePorDocumentoDeIdentidad = obtenerClientePorDocumentoDeIdentidad;
        _solicitarCredito = solicitarCredito;
        _pagarCuota = pagarCuota;

        _tasaDeInteresEfectivoAnual = appSettings.Value.TasaDeInteresEfectivoAnual;
        _mapper = mapper;
    }

    [HttpGet("todos")]
    public Task<IActionResult> ObtenerTodosLosClientesAsync() => HandleRequest(
        async () =>
        {
            IEnumerable<Cliente> clientes = await _obtenerTodosLosClientes.ObtenerTodos();
            return _mapper.Map<IEnumerable<RespuestaCliente>>(clientes);
        },
        ""
    );

    [HttpGet("{documentoDeIdentidad}")]
    public Task<IActionResult> ObtenerClientePorDocumentoDeIdentidad([FromRoute(Name = "documentoDeIdentidad")] string documentoDeIdentidad) => HandleRequest(
        async () =>
        {
            Cliente cliente = await _obtenerClientePorDocumentoDeIdentidad.Obtener(documentoDeIdentidad);
            return _mapper.Map<RespuestaCliente>(cliente);
        },
        ""
    );

    [HttpPost("crear")]
    public Task<IActionResult> CrearCliente([FromBody] CrearCliente dto) => HandleRequest(
        async () =>
        {
            CreacionDeCliente validations = new();
            await validations.ValidateAndThrowAsync(dto);
            Cliente clienteGuardado = await _crearCliente.Crear(_mapper.Map<Cliente>(dto));

            return _mapper.Map<RespuestaCliente>(clienteGuardado);
        },
        ""
    );

    [HttpPost("{documentoDeIdentidad}/creditos/solicitar")]
    public Task<IActionResult> SolicitarCredito([FromRoute(Name = "documentoDeIdentidad")] string documentoDeIdentidad, [FromBody] SolicitarCredito dto) => HandleRequest(
        async () =>
        {
            SolicitudDeCredito validaciones = new();
            await validaciones.ValidateAndThrowAsync(dto);
            Credito creditoSolicitado = await _solicitarCredito.Solicitar(
                documentoDeIdentidad,
                _mapper.Map<Credito>(dto),
                _tasaDeInteresEfectivoAnual
            );

            return _mapper.Map<RespuestaCredito>(creditoSolicitado);
        },
        ""
    );

    [HttpPost("{documentoDeIdentidad}/creditos/{idDelCredito}/pagar")]
    public Task<IActionResult> PagarCuota(
        [FromRoute(Name = "documentoDeIdentidad")] string documentoDeIdentidad,
        [FromRoute(Name = "idDelCredito")] string idCredito,
        [FromBody] PagarCuota dto
    ) => HandleRequest(
        async () =>
        {
            PagoDeCuota validaciones = new();
            await validaciones.ValidateAndThrowAsync(dto);
            Credito creditoPagado = await _pagarCuota.Pagar(
                documentoDeIdentidad,
                idCredito,
                _mapper.Map<Pago>(dto)
            );

            return _mapper.Map<RespuestaCredito>(creditoPagado);
        },
        ""
    );
}