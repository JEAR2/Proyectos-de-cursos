using AutoMapper.Data;
using credinet.comun.api;
using DrivenAdapters.Mongo;
using Microsoft.Extensions.DependencyInjection;
using credibank.AppServices.Automapper;
using StackExchange.Redis;
using System;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;
using Domain.UseCases.Clientes;
using DrivenAdapters.Mongo.Adapters;
using FluentValidation;
using EntryPoints.Commons.DTOs.Validaciones;

namespace credibank.AppServices.Extensions
{
    /// <summary>
    /// Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers the cors.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterCors(this IServiceCollection services, string policyName) =>
            services.AddCors(o => o.AddPolicy(policyName, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        /// <summary>
        /// Método para registrar AutoMapper
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(cfg =>
            {
                cfg.AddDataReaderMapping();
            }, typeof(ConfigurationProfile));

        /// <summary>
        /// Método para registrar Mongo
        /// </summary>
        /// <param name="services">services.</param>
        /// <param name="connectionString">connection string.</param>
        /// <param name="db">database.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterMongo(this IServiceCollection services, string connectionString, string db) =>
                                    services.AddSingleton<IContext>(provider => new Context(connectionString, db));

        /// <summary>
        /// Método para registrar los servicios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Services

            services.AddTransient<IObtenerTodosLosClientesUseCase, ObtenerTodosLosClientesUseCase>();
            services.AddTransient<IObtenerClientePorDocumentoDeIdentidadUseCase, ObtenerClientePorDocumentoDeIdentidadUseCase>();
            services.AddTransient<ICrearClienteUseCase, CrearClienteUseCase>();
            services.AddTransient<ISolicitarCreditoUseCase, SolicitarCreditoUseCase>();
            services.AddTransient<IPagarCuotasUseCase, PagarCuotaUseCase>();

            #endregion Services

            #region Adapters

            services.AddScoped<IRepositorioDeClientesGateway, RepositorioDeClientesAdapter>();

            #endregion Adapters

            #region Helpers

            services.AddSingleton<IMensajesHelper, MensajesApiHelper>();

            #endregion Helpers

            return services;
        }

        public static IServiceCollection RegisterValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreacionDeCliente>();
            services.AddValidatorsFromAssemblyContaining<SolicitudDeCredito>();
            services.AddValidatorsFromAssemblyContaining<PagoDeCuota>();

            return services;
        }

        /// <summary>
        ///   Lazies the connection.
        /// </summary>
        /// <param name="connectionString">connection string.</param>
        /// <returns></returns>
        private static Lazy<ConnectionMultiplexer> LazyConnection(string connectionString) =>
            new(() =>
            {
                return ConnectionMultiplexer.Connect(connectionString);
            });
    }
}