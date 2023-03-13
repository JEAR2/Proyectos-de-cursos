using AutoMapper.Data;
using credinet.comun.api;
using Domain.Model.Entities.Gateway;
using Domain.UseCase.Common;
using DrivenAdapters.Mongo;
using DrivenAdapters.Mongo.Entities;
using Microsoft.Extensions.DependencyInjection;
using Creditos.AppServices.Automapper;
using StackExchange.Redis;
using System;
using Domain.UseCase.Users;
using DrivenAdapter.ServiceBus;
using org.reactivecommons.api;
using org.reactivecommons.api.impl;
using DrivenAdapter.ServiceBus.Entities;
using Domain.UseCase.Commands;
using EntryPoints.ServiceBus.Users;
using Domain.Model.Request;

namespace Creditos.AppServices.Extensions
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
        /// Registro del blobstorage
        /// </summary>
        /// <param name="services">Contenedor de servicios</param>
        /// <param name="connectionString">cadena de conexion del storage</param>
        /// <param name="containerName">nombre del contenedor del storage</param>
        /// <returns></returns>
        //public static IServiceCollection RegisterBlobstorage(this IServiceCollection services, string connectionString, string containerName)
        //{
        //    //Blob storage
        //    //TODO: Buscar si existe mejor implementacion de la DI
        //    services.AddSingleton<IBlobStorage>(provider => new BlobStorage(containerName, connectionString));
        //    return services;
        //}

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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICreditRepository, CreditRepository>();
            services.AddScoped<IUserEventRepository, UserEventsAdapter>();

            #endregion Adaptadores

            #region UseCases
            services.AddTransient<IManageEventsUseCase, ManageEventsUseCase>();
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddTransient<IUserCommandUseCase, UserCommandUseCase>();
            services.AddTransient<IUserCommandSubscription, UserCommandSubscription>();


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
        /// Regiter gateway
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAsyncGateways(this IServiceCollection services, string serviceBusConn) {
            services.RegisterAsyncGateway<UserEntityBus>(serviceBusConn);
            return services;
        }
        /// <summary>
        /// Este metodo sería innecesario si hubiese trabajado con la misma entidad de dominio (UserRequest) en ambos casos
        /// Pero antes no creé UserRequest en el dominio sino que utilicé UserEntityBus que está dentro de los DrivenAdpaters
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAsyncGatewaysUserRequest(this IServiceCollection services, string serviceBusConn)
        {
            services.RegisterAsyncGateway<UserRequest>(serviceBusConn);
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBusConn"></param>
        /// <returns></returns>
        private static void RegisterAsyncGateway<TEntity>(this IServiceCollection services, string serviceBusConn)
        {
            services.AddSingleton<IDirectAsyncGateway<TEntity>>(new DirectAsyncGatewayServiceBus<TEntity>(serviceBusConn));
        }
    }
}