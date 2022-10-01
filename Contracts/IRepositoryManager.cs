using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager : IDisposable
    {
        public ICategoryRepository Category { get; }
        Task saveAsync();
    }
}
