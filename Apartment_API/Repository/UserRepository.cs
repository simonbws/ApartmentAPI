using Apartment_API.Models.DTO;
using Apartment_API.Models;
using Apartment_API.Data;

namespace Apartment_API.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
}
