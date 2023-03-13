using DrivenAdapters.Mongo.Entities;

namespace DrivenAdapters.Mongo.Tests.Entities
{
    public class TipoEntityBuilderTest
    {
        private int _id = 0;
        private string _nombre = string.Empty;

        public TipoEntity Build() => new(_id, _nombre);

        public TipoEntityBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public TipoEntityBuilderTest ConId(int? id)
        {
            if (id is not null)
                _id = id.Value;
            return this;
        }
    }
}