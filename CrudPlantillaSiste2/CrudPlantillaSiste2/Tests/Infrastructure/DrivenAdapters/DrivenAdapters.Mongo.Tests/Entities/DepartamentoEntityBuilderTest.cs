using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Tests.Entities
{
    public class DepartamentoEntityBuilderTest
    {
        private int _id = 0;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;

        public DepartamentoEntity Build() => new(_id, _nombre, _descripcion);

        public DepartamentoEntityBuilderTest ConId(int id)
        {
            _id = id;
            return this;
        }

        public DepartamentoEntityBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public DepartamentoEntityBuilderTest ConDescripcion(string descripcion)
        {
            _descripcion = descripcion;
            return this;
        }
    }
}