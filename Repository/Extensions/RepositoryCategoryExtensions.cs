using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryCategoryExtensions
    {
        public static IQueryable<Category> Search(this IQueryable<Category> categories, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return categories;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return categories.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Category> Sort(this IQueryable<Category> categories, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return categories.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Category>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return categories.OrderBy(e => e.Name);

            return categories.OrderBy(orderQuery);
        }


    }
}
