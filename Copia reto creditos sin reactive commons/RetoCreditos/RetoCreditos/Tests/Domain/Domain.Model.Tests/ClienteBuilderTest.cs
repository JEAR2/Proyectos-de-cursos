using Domain.Model.Entities;

namespace Domain.Model.Tests
{
    public class ClienteBuilderTest
    {
        private string _id = string.Empty;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private string _correo = string.Empty;
        private string _pais = string.Empty;
        private List<Credito> _creditos = new List<Credito>();

        public ClienteBuilderTest()
        {
        }

        public Cliente Build() => new(_id, _nombre, _apellido, _correo, _pais, _creditos);

        public ClienteBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public ClienteBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public ClienteBuilderTest ConApellido(string apellido)
        {
            _apellido = apellido;
            return this;
        }

        public ClienteBuilderTest ConCorreo(string correo)
        {
            _correo = correo;
            return this;
        }

        public ClienteBuilderTest ConPais(string pais)
        {
            _pais = pais;
            return this;
        }

        public ClienteBuilderTest ConCreditos(List<Credito> creditos)
        {
            _creditos = creditos;
            return this;
        }
    }
}