using Domain.Model.Entities;

namespace Domain.Model.Tests
{
    public class DepartamentoBuilderTest
    {
        private int _id = 0;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;

        public Departamento Build() => new(_id, _nombre, _descripcion);

        public DepartamentoBuilderTest ConId(int id)
        {
            _id = id;
            return this;
        }

        public DepartamentoBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public DepartamentoBuilderTest ConDescripcion(string descripcion)
        {
            _descripcion = descripcion;
            return this;
        }
    }
}