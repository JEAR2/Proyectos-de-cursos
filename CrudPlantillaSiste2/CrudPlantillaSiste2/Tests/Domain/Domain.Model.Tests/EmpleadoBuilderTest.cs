using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Tests
{
    public class EmpleadoBuilderTest
    {
        private static string _id = string.Empty;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private int _edad = 0;
        private string _correo = string.Empty;
        private string _sexo = string.Empty;
        private Departamento _departamento;

        public EmpleadoBuilderTest()
        {
        }

        public Empleado Build() => new(_id, _nombre, _apellido, _edad, _correo, _sexo, _departamento);

        public EmpleadoBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public EmpleadoBuilderTest ConNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public EmpleadoBuilderTest ConApellido(string apellido)
        {
            _apellido = apellido;
            return this;
        }

        public EmpleadoBuilderTest ConEdad(int edad)
        {
            _edad = edad;
            return this;
        }

        public EmpleadoBuilderTest ConCorreo(string correo)
        {
            _correo = correo;
            return this;
        }

        public EmpleadoBuilderTest ConSexo(string sexo)
        {
            _sexo = sexo;
            return this;
        }

        public EmpleadoBuilderTest ConDepartamento(Departamento departamento)
        {
            _departamento = departamento;
            return this;
        }
    }
}