using Domain.Model.Entities;

namespace Domain.Model.Tests
{
    public class TipoBuilderTest
    {
        private int _id = 0;
        private string _nombre = string.Empty;

        public Tipo Build() => new(_id, _nombre);

        public TipoBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public TipoBuilderTest ConId(int id)
        {
            _id = id;
            return this;
        }
    }
}