using AutoMapper;
using Domain.Model.Entities;

namespace CrudPlantillaSiste.AppServices.Automapper
{
    /// <summary>
    /// EntityProfile
    /// </summary>
    public class ConfigurationProfile : Profile
    {
        /// <summary>
        /// ConfigurationProfile
        /// </summary>
        public ConfigurationProfile()
        {
            // CreateMap<Entity, DrivenAdapters.Mongo.Entities.EntityBase>();
            //CreateMap<DrivenAdapters.Mongo.Entities.EntityBase, Entity>();
        }
    }
}