using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Passwords.Model;
using Passwords.Repositories.Interfaces;

namespace Passwords.Repositories
{
    public class CodeRepository : ICodeRepository
    {
        private readonly PasswordsDb _db;

        public CodeRepository (PasswordsDb db)
        {
            _db = db;
        }

        public async Task<Code?> CreateAsync(Code data)
        {
            Code code = (await _db.Codes.AddAsync(data)).Entity;
            return await _db.SaveChangesAsync() == 1 ? code : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Code? code = _db.Codes.Find(id);

            if (code is null)
            {
                return null;
            }

            _db.Codes.Remove(code);
            return await _db.SaveChangesAsync() == 1;
        }

        public List<Code?>? RetrieveAll()
        {
            List<Code?> code = _db.Codes.ToList();
            return code.Count==0 ? null : code;
        }

        public async Task<IEnumerable<Code?>> RetrieveAllAsync()
        {
            IEnumerable<Code> code = _db.Codes.ToList();
            return await Task.FromResult<IEnumerable<Code>>(code);
        }

        public async Task<Code?> UpdateAsync(int id, Code data)
        {
            try
            {
                data.Id = id;
                Code code = (_db.Codes.Update(data)).Entity;

                return await _db.SaveChangesAsync() == 1 ? code : null;
            }
            catch
            {
                return null;
            }
        }

    }
}
