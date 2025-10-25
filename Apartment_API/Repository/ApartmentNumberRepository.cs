using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Repository.IRepository;

namespace Apartment_API.Repository
{
    public class ApartmentNumberRepository : Repository<ApartmentNumber>, IApartmentNumberRepository
    {
        private readonly AppDbContext _db;
        public ApartmentNumberRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ApartmentNumber> UpdateAsync(ApartmentNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.ApartmentNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
