namespace Domain.Model.Entities
{
    /// <summary>
    /// Clase Empleado
    /// </summary>
    public class Empleado
    {
        private int departamentoId;

        /// <summary>
        /// Id
        /// </summary>
        public string id { get; private set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Apellido
        /// </summary>
        public string Apellido { get; private set; }

        /// <summary>
        /// Edad
        /// </summary>
        public int Edad { get; private set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string Correo { get; private set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sexo { get; private set; }

        /// <summary>
        /// Departamento
        /// </summary>
        public Departamento Departamento { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="edad"></param>
        /// <param name="correo"></param>
        /// <param name="sexo"></param>
        /// <param name="departamento"></param>
        public Empleado(string id, string nombre, string apellido, int edad, string correo, string sexo, Departamento departamento)
        {
            this.id = id;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Correo = correo;
            Sexo = sexo;
            Departamento = departamento;
        }

        /// <summary>
        /// Constructor sin Id
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="edad"></param>
        /// <param name="correo"></param>
        /// <param name="sexo"></param>
        /// <param name="departamento"></param>
        public Empleado(string nombre, string apellido, int edad, string correo, string sexo, Departamento departamento)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Correo = correo;
            Sexo = sexo;
            Departamento = departamento;
        }

        public Empleado(string nombre, string apellido, int edad, string correo, string sexo, int departamentoId)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Correo = correo;
            Sexo = sexo;
            Departamento = new Departamento(departamentoId); ;
        }

        /// <summary>
        /// Establece el departamento al que pertenece el empleado
        /// </summary>
        /// <param name="departamento"></param>
        public void EstablecerDepartamento(Departamento departamento) => Departamento = departamento;
    }
}