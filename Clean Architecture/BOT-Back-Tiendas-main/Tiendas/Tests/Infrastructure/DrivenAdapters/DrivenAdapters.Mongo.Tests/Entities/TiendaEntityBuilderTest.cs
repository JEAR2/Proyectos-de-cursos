using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Tests.Entities;

namespace Domain.Model.Tests
{
    public class TiendaEntityBuilderTest
    {
        private string _id = string.Empty;
        private string _nombre = string.Empty;
        private TipoEntity _tipo = new TipoEntityBuilderTest().ConId(0).Build();
        private string _direccion = string.Empty;
        private UbicacionEntity _ubicacion = new(0, 0);
        private string _celular = string.Empty;
        private string _correo = string.Empty;
        private DateTime _fechaCreacion;

        public TiendaEntityBuilderTest()
        { }

        public TiendaEntity Build() => new(_id, _nombre, _tipo, _direccion,
                _ubicacion, _celular, _correo, _fechaCreacion);

        public TiendaEntityBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public TiendaEntityBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public TiendaEntityBuilderTest ConTipo(TipoEntity tipo)
        {
            _tipo = tipo;
            return this;
        }

        public TiendaEntityBuilderTest ConDireccion(string direccion)
        {
            _direccion = direccion;
            return this;
        }

        public TiendaEntityBuilderTest ConUbicacion(UbicacionEntity ubicacion)
        {
            _ubicacion = ubicacion;
            return this;
        }

        public TiendaEntityBuilderTest ConCelular(string celular)
        {
            _celular = celular;
            return this;
        }

        public TiendaEntityBuilderTest ConCorreo(string correo)
        {
            _correo = correo;
            return this;
        }

        public TiendaEntityBuilderTest ConFechaCreacion(DateTime fechaCreacion)
        {
            _fechaCreacion = fechaCreacion;
            return this;
        }
    }
}