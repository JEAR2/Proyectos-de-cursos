namespace Domain.Model.Entities
{
    /// <summary>
    /// Tipo
    /// </summary>
    public class Tipo
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        public Tipo(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        public Tipo(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; private set; }
    }
}