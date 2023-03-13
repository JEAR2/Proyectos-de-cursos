using DrivenAdapters.Mongo.DbModels;

using MongoDB.Driver;

namespace DrivenAdapters.Mongo;

/// <summary>
/// Interfaz Mongo context contract.
/// </summary>
public interface IContext
{
    /// <summary>
    /// Colleccion de Tipo Contrato
    /// </summary>
    public IMongoCollection<DocumentoCliente> Clientes { get; }
}