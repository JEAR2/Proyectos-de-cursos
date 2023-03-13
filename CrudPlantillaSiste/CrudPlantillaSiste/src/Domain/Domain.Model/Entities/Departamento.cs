namespace Domain.Model.Entities
{
    /// <summary>
    /// Clase Departamento
    /// </summary>
    public class Departamento
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        public Departamento(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        /// <summary>
        /// Constructor con solo id
        /// </summary>
        /// <param name="id"></param>
        public Departamento(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Constructor sin Id
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        public Departamento(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}