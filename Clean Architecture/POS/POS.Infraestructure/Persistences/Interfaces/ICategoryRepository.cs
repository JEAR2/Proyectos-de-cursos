﻿using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface ICategoryRepository
    {
        Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters);

        Task<IEnumerable<Category>> ListSelectCategories();

        Task<Category> GetCategoryById(int categoryId);

        Task<bool> RegisterCategory(Category category);

        Task<bool> EditCategory(Category category);

        Task<bool> DeleteCategory(int categoryId);
    }
}