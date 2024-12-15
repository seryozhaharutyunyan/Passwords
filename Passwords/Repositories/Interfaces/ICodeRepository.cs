using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Passwords.Model;

namespace Passwords.Repositories.Interfaces
{
    public interface ICodeRepository
    {
        List<Code?> RetrieveAll();
        Task<IEnumerable<Code?>> RetrieveAllAsync();
        Task<Code?> CreateAsync(Code data);
        Task<Code?> UpdateAsync(int id, Code data);
        Task<bool?> DeleteAsync(int id);
    }
}
