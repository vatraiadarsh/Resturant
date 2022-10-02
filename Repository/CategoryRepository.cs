using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeatures.Parameters;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateCategory(Category category) => Create(category);


        public void DeleteCategory(Category category) => Delete(category);


        public async Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters,bool trackChanges)
        {
            var categories = await FindAll(trackChanges)
                .Search(categoryParameters.SearchTerm)
                .Sort(categoryParameters.OrderBy)
                .ToListAsync();
            return PagedList<Category>.ToPagedList(categories,categoryParameters.PageNumber,categoryParameters.PageSize);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(categoryId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryCollectionAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public void UpdateCategory(Category category) => Update(category);
        
    }
}
