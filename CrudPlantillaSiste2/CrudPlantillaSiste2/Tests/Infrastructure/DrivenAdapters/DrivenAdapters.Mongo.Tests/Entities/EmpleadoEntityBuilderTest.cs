using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities;

namespace DrivenAdapters.Mongo.Tests.Entities
{
    public class EmpleadoEntityBuilderTest
    {
        private static string _id = string.Empty;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private int _edad = 0;
        private string _correo = string.Empty;
        private string _sexo = string.Empty;
        private DepartamentoEntity _departamento = new DepartamentoEntityBuilderTest().ConId(id: 0).Build();

        public EmpleadoEntityBuilderTest()
        {
        }

        public EmpleadoEntity Build() => new(_id, _nombre, _apellido, _edad, _correo, _sexo, _departamento);

        public EmpleadoEntityBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public EmpleadoEntityBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public EmpleadoEntityBuilderTest ConApellido(string apellido)
        {
            _apellido = apellido;
            return this;
        }

        public EmpleadoEntityBuilderTest ConEdad(int edad)
        {
            _edad = edad;
            return this;
        }

        public EmpleadoEntityBuilderTest ConCorreo(string correo)
        {
            _correo = correo;
            return this;
        }

        public EmpleadoEntityBuilderTest ConSexo(string sexo)
        {
            _sexo = sexo;
            return this;
        }

        public EmpleadoEntityBuilderTest ConDepartamento(DepartamentoEntity departamento)
        {
            _departamento = departamento;
            return this;
        }
    }
}