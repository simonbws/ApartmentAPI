using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Apartment_API.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _db;
        public ApartmentRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Apartment entity)
        {
            await _db.Apartments.AddAsync(entity);
        }

        public async Task<Apartment> GetAsync(Expression<Func<Apartment, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Apartment> query = _db.Apartments;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            // because this is single Get, we use FirstOrDefault instead of ToList.
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Apartment>> GetAllAsync(Expression<Func<Apartment, bool>> filter = null)
        {
            IQueryable<Apartment> query = _db.Apartments;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            // At this point the query will be executed. This is deferred execution. ToList() causes immediate execution.
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Apartment entity)
        {
            _db.Apartments.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Apartment entity)
        {
            _db.Apartments.Update(entity);
            await SaveAsync(); ;
        }
    }
}
