using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities;

namespace DrivenAdapters.Mongo.Tests.Entities
{
    public class ClienteEntityBuilderTest
    {
        private string _id = string.Empty;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private string _correo = string.Empty;
        private string _pais = string.Empty;
        private List<Credito> _creditos = new List<Credito>();

        public ClienteEntity Build() => new(_id, _nombre, _apellido, _correo, _pais, _creditos);

        public ClienteEntityBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public ClienteEntityBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public ClienteEntityBuilderTest ConApellido(string apellido)
        {
            _apellido = apellido;
            return this;
        }

        public ClienteEntityBuilderTest ConCorreo(string correo)
        {
            _correo = correo;
            return this;
        }

        public ClienteEntityBuilderTest ConPais(string pais)
        {
            _pais = pais;
            return this;
        }

        public ClienteEntityBuilderTest ConCreditos(List<Credito> creditos)
        {
            _creditos = creditos;
            return this;
        }
    }
}