using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Passwords.Model;
using Passwords.Repositories.Interfaces;

namespace Passwords.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly PasswordsDb _db;

        public DataRepository(PasswordsDb db)
        {
            _db = db;
        }
        public async Task<Data?> CreateAsync(Data d)
        {
            Data data = (await _db.Data.AddAsync(d)).Entity;
            return await _db.SaveChangesAsync() == 1 ? data : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Data? data = _db.Data.Find(id);

            if (data is null)
            {
                return null;
            }

            _db.Data.Remove(data);
            return await _db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Data>> RetrieveAllAsync()
        {
            IEnumerable<Data> data = _db.Data.ToList();
            return await Task.FromResult<IEnumerable<Data>>(data);
        }

        public async Task<Data?> RetrieveAsync(int id)
        {
            Data? data = await _db.Data.FindAsync(id);
            return data;
        }

        public async Task<Data?> UpdateAsync(int id, Data d)
        {
            try
            {
                d.Id = id;
                Data data = (_db.Data.Update(d)).Entity;

                return await _db.SaveChangesAsync() == 1 ? data : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
