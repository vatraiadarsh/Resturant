using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters, bool trackChanges);
        Task<Category> GetCategoryByIdAsync(Guid categoryId, bool trackChanges);
        void CreateCategory(Category category);
        Task<IEnumerable<Category>> GetCategoryCollectionAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);

    }
}
