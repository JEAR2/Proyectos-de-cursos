using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //para hacer el uso de transacciones
        private readonly PosContext _context;

        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(PosContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }

        public void Dispose()
        {
            //para liberar los objetos o recursos en memoria
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}