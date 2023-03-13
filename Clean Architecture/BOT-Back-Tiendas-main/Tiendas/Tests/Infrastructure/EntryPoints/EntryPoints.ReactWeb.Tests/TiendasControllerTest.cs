using credinet.comun.models.Vendors;
using credinet.exception.middleware.enums;
using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Tests;
using Domain.UseCase.Common;
using Domain.UseCase.Tiendas;
using EntryPoints.ReactiveWeb.Controllers;
using EntryPoints.ReactiveWeb.Entity;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;

using Xunit;

namespace EntryPoints.ReactWeb.Tests
{
    public class TiendasControllerTest
    {
        private readonly Mock<ITiendaUseCase> _tiendaUseCaseMock;
        private readonly Mock<IManageEventsUseCase> _manageEventsUseCase;
        private readonly Mock<IOptions<ConfiguradorAppSettings>> _appSettings;

        private readonly TiendasController _tiendasController;

        public TiendasControllerTest()
        {
            _appSettings = new();
            _manageEventsUseCase = new();
            _tiendaUseCaseMock = new();

            _appSettings.Setup(settings => settings.Value)
                .Returns(new ConfiguradorAppSettings
                {
                    DefaultCountry = "co",
                    DomainName = "Tiendas"
                });

            _tiendasController = new(_tiendaUseCaseMock.Object, _manageEventsUseCase.Object, _appSettings.Object);

            _tiendasController.ControllerContext.HttpContext = new DefaultHttpContext();
            _tiendasController.ControllerContext.HttpContext.Request.Headers["Location"] = "1,1";
            _tiendasController.ControllerContext.RouteData = new RouteData();
            _tiendasController.ControllerContext.RouteData.Values.Add("controller", "Tiendas");
        }

        [Theory]
        [InlineData("Tienda 1234", 1, "Físico")]
        [InlineData("Tienda 1234", 2, "Virtual")]
        public async Task CrearAlmacenes_Con_Status200(string nombreTienda, int idTipo, string nombreTipo)
        {
            TiendaRequest tiendaRequest = ObtenerTiendaRequestTest(idTipo: 1);

            _tiendaUseCaseMock
                .Setup(useCase => useCase.CrearTienda(It.IsAny<Tienda>()))
                .ReturnsAsync(ObtenerTiendaTest(nombreTienda, idTipo, nombreTipo));

            _tiendasController.ControllerContext.RouteData.Values.Add("action", "CrearAlmacenes");

            var result = await _tiendasController.CrearAlmacenes(tiendaRequest);

            var okObjectResult = result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObtenerAlmacenes_Con_Status200()
        {
            _tiendaUseCaseMock
                .Setup(useCase => useCase.ObtenerTiendas())
                .ReturnsAsync(ObtenerTiendasTest);

            _tiendasController.ControllerContext.RouteData.Values.Add("action", "ObtenerTiendas");

            var result = await _tiendasController.ObtenerTiendas();

            var okObjectResult = result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CrearAlmacenes_Con_Excepcion_No_Controlada()
        {
            TiendaRequest tiendaRequest = new();

            _tiendaUseCaseMock
                .Setup(useCase => useCase.CrearTienda(It.IsAny<Tienda>()))
                .ReturnsAsync(ObtenerTiendaTest(string.Empty, 0, string.Empty));

            _tiendasController.ControllerContext.RouteData.Values.Add("action", "CrearAlmacenes");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _tiendasController.CrearAlmacenes(tiendaRequest));

            Assert.Equal((int)TipoExcepcionNegocio.ExceptionNoControlada, exception.code);
        }

        [Fact]
        public async Task CrearAlmacenes_Con_Excepcion_De_Negocio()
        {
            TiendaRequest tiendaRequest = ObtenerTiendaRequestTest(idTipo: 0);

            _tiendaUseCaseMock
                .Setup(useCase => useCase.CrearTienda(It.IsAny<Tienda>()))
                .Throws(new BusinessException(nameof(TipoExcepcionNegocio.TipoInvalido), (int)TipoExcepcionNegocio.TipoInvalido));

            _tiendasController.ControllerContext.RouteData.Values.Add("action", "CrearAlmacenes");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _tiendasController.CrearAlmacenes(tiendaRequest));

            Assert.Equal((int)TipoExcepcionNegocio.TipoInvalido, exception.code);
        }

        #region Private Methods

        /// <summary>
        /// ObtenerTiendaRequestTest
        /// </summary>
        /// <param name="idTipo"></param>
        /// <returns></returns>
        private TiendaRequest ObtenerTiendaRequestTest(int idTipo) => new TiendaRequest()
        {
            TipoId = idTipo,
            Celular = "3001111111",
            Correo = "mail@mail.com",
            Direccion = "Calle 13",
            Nombre = "Tienda 13",
            Ubicacion = new() { Latitud = 0.1M, Longitud = 0.1M }
        };

        /// <summary>
        /// ObtenerTiendaTest
        /// </summary>
        /// <param name="nombreTienda"></param>
        /// <param name="idTipo"></param>
        /// <param name="nombreTipo"></param>
        /// <returns></returns>
        private Tienda ObtenerTiendaTest(string nombreTienda, int idTipo, string nombreTipo) => new TiendaBuilderTest()
            .ConId("1234")
            .ConNombre(nombreTienda)
            .ConTipo(new TipoBuilderTest().ConId(idTipo).ConNombre(nombreTipo).Build())
            .ConUbicacion(new UbicacionBuilderTest().Build())
        .Build();

        /// <summary>
        /// ObtenerTiendasTest
        /// </summary>
        /// <returns></returns>
        private List<Tienda> ObtenerTiendasTest() => new()
            {
                new TiendaBuilderTest()
                    .ConId("1234")
                    .ConNombre("Tienda 1234")
                    .ConTipo(new TipoBuilderTest().ConId(1).ConNombre("Físico").Build())
                    .ConUbicacion(new UbicacionBuilderTest().Build())
                .Build(),
                new TiendaBuilderTest()
                    .ConId(id: "1111")
                    .ConNombre(nombre: "Tienda 1111")
                    .ConTipo(tipo: new TipoBuilderTest().ConId(id: 2).ConNombre(nombre: "Virtual").Build())
                    .ConUbicacion(ubicacion: new UbicacionBuilderTest().Build())
                .Build()
            };

        #endregion Private Methods
    }
}