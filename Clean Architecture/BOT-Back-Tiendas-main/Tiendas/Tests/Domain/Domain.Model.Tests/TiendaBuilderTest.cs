using Domain.Model.Entities;

namespace Domain.Model.Tests
{
    public class TiendaBuilderTest
    {
        private static string _id = string.Empty;
        private string _nombre = string.Empty;
        private Tipo _tipo;
        private string _direccion = string.Empty;
        private Ubicacion _ubicacion = new(0, 0);
        private string _celular = string.Empty;
        private string _correo = string.Empty;
        private DateTime _fechaCreacion;

        public TiendaBuilderTest()
        { }

        public Tienda Build() => new(_id, _nombre, _tipo, _direccion,
                _ubicacion, _celular, _correo, _fechaCreacion);

        public TiendaBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public TiendaBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public TiendaBuilderTest ConTipo(Tipo tipo)
        {
            _tipo = tipo;
            return this;
        }

        public TiendaBuilderTest ConDireccion(string direccion)
        {
            _direccion = direccion;
            return this;
        }

        public TiendaBuilderTest ConUbicacion(Ubicacion ubicacion)
        {
            _ubicacion = ubicacion;
            return this;
        }

        public TiendaBuilderTest ConCelular(string celular)
        {
            _celular = celular;
            return this;
        }

        public TiendaBuilderTest ConCorreo(string correo)
        {
            _correo = correo;
            return this;
        }

        public TiendaBuilderTest ConFechaCreacion(DateTime fechaCreacion)
        {
            _fechaCreacion = fechaCreacion;
            return this;
        }
    }
}