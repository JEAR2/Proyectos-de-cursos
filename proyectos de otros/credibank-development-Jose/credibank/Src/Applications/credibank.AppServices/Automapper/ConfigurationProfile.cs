using System;

using AutoMapper;

using Domain.Model.Entities;

using DrivenAdapters.Mongo.DbModels;

using EntryPoints.Commons.DTOs.Comandos;
using EntryPoints.Commons.DTOs.Respuestas;

using GRPCModels = EntryPoints.GRPC.Protos;

namespace credibank.AppServices.Automapper;

/// <summary>
/// EntityProfile
/// </summary>
public class ConfigurationProfile : Profile
{
    /// <summary>
    /// ConfigurationProfile
    /// </summary>
    public ConfigurationProfile()
    {
        #region Domain Models to Mongo Docs

        CreateMap<Cliente, DocumentoCliente>()
            .ReverseMap();
        CreateMap<Credito, DocumentoCredito>()
            .ReverseMap();
        CreateMap<Pago, DocumentoPago>()
            .ReverseMap();

        #endregion Domain Models to Mongo Docs

        #region Domain Models To REST Responses

        CreateMap<Pago, RespuestaPago>();
        CreateMap<Cliente, RespuestaCliente>();
        CreateMap<Credito, RespuestaCredito>();

        #endregion Domain Models To REST Responses

        #region REST Commands To Domain Models

        CreateMap<CrearCliente, Cliente>();
        CreateMap<SolicitarCredito, Credito>();
        CreateMap<PagarCuota, Pago>();

        #endregion REST Commands To Domain Models

        #region GRPC Requests To REST Commands

        CreateMap<GRPCModels.CrearClienteRequest, CrearCliente>();
        CreateMap<GRPCModels.SolicitarCreditoRequest, SolicitarCredito>();
        CreateMap<GRPCModels.PagarCuotaRequest, PagarCuota>();

        #endregion GRPC Requests To REST Commands

        #region Domain Models To GRPC Responses

        CreateMap<Cliente, GRPCModels.Cliente>();
        CreateMap<Credito, GRPCModels.Credito>()
            .ForMember(
            grpcModel => grpcModel.FechaDeCancelacion,
            config => config.MapFrom((domain, grpc) =>
                {
                    if (domain.FechaDeCancelacion is null) grpc.FechaDeCancelacion = "";
                    else grpc.FechaDeCancelacion = domain.FechaDeCancelacion.ToString();
                    return grpc.FechaDeCancelacion;
                })
            );
        CreateMap<Pago, GRPCModels.Pago>();

        #endregion Domain Models To GRPC Responses
    }
}