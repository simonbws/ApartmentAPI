using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Apartment_API.Repository
{
    public class ApartmentRepository : Repository<Apartment>, IApartmentRepository
    {
        private readonly AppDbContext _db;
        public ApartmentRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Apartment> UpdateAsync(Apartment entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Apartments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
