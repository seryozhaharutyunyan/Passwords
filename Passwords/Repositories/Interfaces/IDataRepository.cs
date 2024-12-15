using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Passwords.Model;

namespace Passwords.Repositories.Interfaces
{
    public interface IDataRepository
    {
        Task<IEnumerable<Data>> RetrieveAllAsync();
        Task<Data?> RetrieveAsync(int id);
        Task<Data?> CreateAsync(Data data);
        Task<Data?> UpdateAsync(int id, Data data);
        Task<bool?> DeleteAsync(int id);
    }
}
