using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.CrossCutting.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly MyContext _context;

        private DbSet<T> _dataset;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                    return false;

                _dataset.Remove(result);

                await _context.SaveChangesAsync();

            }
            catch (System.Exception)
            {
                throw;
            }

            return true;
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                _dataset.Add(item);

                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return item;
        }

        //Procura um ID 
        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));

                if (result == null)
                    throw new RecordIsNullException("Record is null");

                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                if (result == null)
                    throw new RecordIsNullException("Record is null");


                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = result.CreateAt;

                //Atualizando os valores do objeto
                _context.Entry(result).CurrentValues.SetValues(item);

                await _context.SaveChangesAsync();

            }
            catch (System.Exception)
            {

                throw;
            }

            return item;
        }
    }
}