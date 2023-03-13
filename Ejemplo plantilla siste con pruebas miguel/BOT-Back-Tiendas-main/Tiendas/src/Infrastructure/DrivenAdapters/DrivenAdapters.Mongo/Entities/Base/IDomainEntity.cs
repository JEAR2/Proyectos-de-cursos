namespace DrivenAdapters.Mongo.Entities.Base
{
    /// <summary>
    /// IDomainEntity interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainEntity<out T> where T : class
    {
        /// <summary>
        /// Convierte una entidad a una entidad de dominio
        /// </summary>
        /// <returns></returns>
        T AsEntity();
    }
}