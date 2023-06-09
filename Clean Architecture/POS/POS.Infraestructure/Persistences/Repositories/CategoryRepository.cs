﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.Security.Cryptography.X509Certificates;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly PosContext _context;

        public CategoryRepository(PosContext context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Category>();
            //uso de Linq
            var categories = (from c in _context.Categories
                              where c.AuditDeleteUser == null && c.AuditDeleteDate == null
                              select c).AsNoTracking().AsQueryable();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        categories = categories.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;

                    case 2:
                        categories = categories.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(filters.StateFilter));
            }
            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categories = categories.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }
            if (filters.Sort is not null)
            {
                filters.Sort = "CategoryId";
            }
            response.TotalRecords = await categories.CountAsync();
            response.Items = await Ordering(filters, categories, !(bool)filters.Download!).ToListAsync();
            return response;
        }

        public async Task<IEnumerable<Category>> ListSelectCategories()
        {
            var categories = await _context.Categories.Where(x => x.State.Equals((int)StateTypes.Active) && x.AuditDeleteUser == null && x.AuditDeleteDate == null).AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories!.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId.Equals(categoryId));
            return category;
        }

        public async Task<bool> RegisterCategory(Category category)
        {
            category.AuditCreateUser = 1;
            category.AuditCreateDate = DateTime.Now;
            await _context.AddAsync(category);
            var recorsAffected = await _context.SaveChangesAsync();
            return recorsAffected > 0;
        }

        public async Task<bool> EditCategory(Category category)
        {
            category.AuditUpdateUser = 1;
            category.AuditUpdateDate = DateTime.Now;

            _context.Update(category);
            _context.Entry(category).Property(x => x.AuditCreateUser).IsModified = false;
            _context.Entry(category).Property(x => x.AuditCreateDate).IsModified = false;

            var recorsAffected = await _context.SaveChangesAsync();
            return recorsAffected > 0;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories!.AsNoTracking().SingleOrDefaultAsync(x => x.CategoryId.Equals(categoryId));
            category.AuditDeleteUser = 1;
            category.AuditDeleteDate = DateTime.Now;
            _context.Update(category);
            var recorsAffected = await _context.SaveChangesAsync();
            return recorsAffected > 0;
        }
    }
}