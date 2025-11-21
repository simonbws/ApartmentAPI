using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using Apartment_API.Repository;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Apartment_API;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Apartment_Tests
{
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Delete existing db context
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    // Add InMemory db
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("TestApartmentDb"));
                    // add required password configuration
                    services.Configure<IdentityOptions>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 3;
                    });
                    //  Add AutoMapper, IConfiguration, UserRepository
                    services.AddAutoMapper(typeof(MappingConfig));

                    var config = new ConfigurationBuilder()
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            { "ApiSettings:Secret", "TEST_SECRET_1234567890" }
                        })
                        .Build();
                    services.AddSingleton<IConfiguration>(config);
                    services.AddScoped<IUserRepository, UserRepository>();
                });
            }).CreateClient();
        }
        [Fact]
        public async Task RegisterUser_ShouldReturnOk_ForValidData()
        {
            // arrange
            var request = new RegisterationRequestDTO()
            {
                UserName = "test1@test.com",
                Name = "Test User",
                Password = "abc123",
                Role = "admin"
            };
            // act
            var response = await _client.PostAsJsonAsync("/api/v1/UsersAuth/register", request);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task RegisterUser_ShouldReturnBadRequest_WhenUserAlreadyExists()
        {
            var request = new RegisterationRequestDTO()
            {
                UserName = "existing@test.com",
                Name = "User",
                Password = "abc123",
                Role = "admin"
            };
            await _client.PostAsJsonAsync("/api/v1/UsersAuth/register", request);
            var secondResponse = await _client.PostAsJsonAsync("/api/v1/UsersAuth/register", request);

            secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Login_ShouldReturnBadRequest_ForInvalidCredentials()
        {
            var loginRequest = new LoginRequestDTO
            {
                UserName = "nonexistent@test.com",
                Password = "wrongpass"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/UsersAuth/login", loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = await response.Content.ReadFromJsonAsync<APIResponse>();
            content.IsSuccess.Should().BeFalse();
        }
    }
}