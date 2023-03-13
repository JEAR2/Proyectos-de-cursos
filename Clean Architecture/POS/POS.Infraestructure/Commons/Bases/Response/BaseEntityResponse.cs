namespace POS.Infraestructure.Commons.Bases.Response
{
    //para devolver los registros de la base de datos
    public class BaseEntityResponse<T>
    {
        public int TotalRecords { get; set; }
        public List<T>? Items { get; set; }
    }
}