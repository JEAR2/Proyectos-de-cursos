using AutoMapper.Data;
using credinet.comun.api;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.UseCase.Common;
using Domain.UseCase.Tiendas;
using DrivenAdapter.ServicesBus;
using DrivenAdapter.ServicesBus.Entities;
using DrivenAdapters.Mongo;
using Microsoft.Extensions.DependencyInjection;
using org.reactivecommons.api;
using org.reactivecommons.api.impl;
using StackExchange.Redis;
using System;
using Tiendas.AppServices.Automapper;

namespace Tiendas.AppServices.Extensions
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
        ///   Método para registrar Redis Cache
        /// </summary>
        /// <param name="services">services.</param>
        /// <param name="connectionString">connection string.</param>
        /// <param name="dbNumber">database number.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterRedis(this IServiceCollection services, string connectionString, int dbNumber)
        {
            services.AddSingleton(s => LazyConnection(connectionString).Value.GetDatabase(dbNumber));

            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(connectionString,
                opt => opt.DefaultDatabase = dbNumber);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            return services;
        }

        /// <summary>
        /// Método para registrar los servicios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Helpers

            services.AddSingleton<IMensajesHelper, MensajesApiHelper>();

            #endregion Helpers

            #region Adaptadores

            services.AddScoped<ITiendaRepository, TiendaRepository>();
            services.AddScoped<ITipoRepository, TipoRepository>();
            services.AddScoped<ITiendaEventsRepository, TiendaEventsAdapter>();

            #endregion Adaptadores

            #region UseCases

            services.AddScoped<ITiendaUseCase, TiendaUseCase>();
            services.AddScoped<IManageEventsUseCase, ManageEventsUseCase>();

            #endregion UseCases

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

        /// <summary>
        /// RegisterAsyncGateways
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        public static IServiceCollection RegisterAsyncGateways(this IServiceCollection services,
                string serviceBusConn)
        {
            services.RegisterAsyncGateway<TiendaEntity>(serviceBusConn);
            return services;
        }

        /// <summary>
        /// Register Gateway
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        private static void RegisterAsyncGateway<TEntity>(this IServiceCollection services, string serviceBusConn) =>
                services.AddSingleton<IDirectAsyncGateway<TEntity>>(new DirectAsyncGatewayServiceBus<TEntity>(serviceBusConn));
    }
}