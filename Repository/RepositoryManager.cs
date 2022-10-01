using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {

        private RepositoryContext _context;
        private ICategoryRepository _categoryRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
        }

        public ICategoryRepository Category
        {
            // repository initialization with db context
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
            
        }

        public void Dispose() => _context.Dispose();

        public Task saveAsync() => _context.SaveChangesAsync();
        
    }
}
