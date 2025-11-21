using Apartment_API;
using Apartment_API.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using Apartment_API.Models.DTO;
using FluentAssertions;
using Apartment_API.Models;

namespace Apartment_Tests
{
    public class ApartmentControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _jwtSecret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT";

        public ApartmentControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // replace sql Dbcontext InMemory
                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("TestApartmentDb"));
                });
            });
            _client = _factory.CreateClient();
        }
        private string GenerateJwtToken(string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.UtcNow.AddHours(1),
               signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [Fact]
        public async Task Admin_CanCreateApartment_ReturnsCreated()
        {
            var token = GenerateJwtToken("admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var model = new ApartmentCreateDTO
            {
                Name = "Test Apartment",
                Details = "Test details",
                Rate = 100,
                Sqft = 50,
                Occupancy = 2,
                ImageUrl = "test.jpg",
                Amenity = "None"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/apartmentApi", model);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        [Fact]
        public async Task Customer_CannotCreateApartment_ReturnsForbidden()
        {
            var token = GenerateJwtToken("customer");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var model = new ApartmentCreateDTO
            {
                Name = "Test Apartment 2",
                Details = "Test details",
                Rate = 100,
                Sqft = 50,
                Occupancy = 2,
                ImageUrl = "test2.jpg",
                Amenity = "None"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/apartmentApi", model);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [Fact]
        public async Task NoToken_ReturnsUnauthorized()
        {
            _client.DefaultRequestHeaders.Authorization = null;

            var model = new ApartmentCreateDTO
            {
                Name = "Test Apartment 3",
                Details = "Test details",
                Rate = 100,
                Sqft = 50,
                Occupancy = 2,
                ImageUrl = "test3.jpg",
                Amenity = "None"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/apartmentApi", model);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        //[Fact]
        //public async Task Admin_CanDeleteApartment_ReturnsNoContent()
        //{
        //    var token = GenerateJwtToken("admin");
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    // Tworzymy apartament do usunięcia
        //    var createModel = new ApartmentCreateDTO
        //    {
        //        Name = "Delete Apartment",
        //        Details = "To delete",
        //        Rate = 100,
        //        Sqft = 50,
        //        Occupancy = 2,
        //        ImageUrl = "delete.jpg",
        //        Amenity = "None"
        //    };

        //    var createResponse = await _client.PostAsJsonAsync("/api/v1/apartmentApi", createModel);
        //    var createdContent = await createResponse.Content.ReadFromJsonAsync<APIResponse>();
        //    var id = ((System.Text.Json.JsonElement)createdContent.Result).GetProperty("id").GetInt32();

        //    // Usuwamy apartament
        //    var response = await _client.DeleteAsync($"/api/v1/apartmentApi/{id}");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
        //[Fact]
        //public async Task Admin_CanGetApartment_ReturnsOk()
        //{
        //    var token = GenerateJwtToken("admin");
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    // Tworzymy apartament
        //    var createModel = new ApartmentCreateDTO
        //    {
        //        Name = "Get Apartment",
        //        Details = "For get test",
        //        Rate = 100,
        //        Sqft = 50,
        //        Occupancy = 2,
        //        ImageUrl = "get.jpg",
        //        Amenity = "None"
        //    };

        //    var createResponse = await _client.PostAsJsonAsync("/api/v1/apartmentApi", createModel);
        //    var createdContent = await createResponse.Content.ReadFromJsonAsync<APIResponse>();
        //    var id = ((System.Text.Json.JsonElement)createdContent.Result).GetProperty("id").GetInt32();

        //    // Pobieramy apartament
        //    var getResponse = await _client.GetAsync($"/api/v1/apartmentApi/{id}");
        //    getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
        //[Fact]
        //public async Task Admin_CanUpdateApartment_ReturnsNoContent()
        //{
        //    var token = GenerateJwtToken("admin");
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    // Tworzymy apartament
        //    var createModel = new ApartmentCreateDTO
        //    {
        //        Name = "Update Apartment",
        //        Details = "Before update",
        //        Rate = 100,
        //        Sqft = 50,
        //        Occupancy = 2,
        //        ImageUrl = "update.jpg",
        //        Amenity = "None"
        //    };

        //    var createResponse = await _client.PostAsJsonAsync("/api/v1/apartmentApi", createModel);
        //    var createdContent = await createResponse.Content.ReadFromJsonAsync<APIResponse>();
        //    var id = ((System.Text.Json.JsonElement)createdContent.Result).GetProperty("id").GetInt32();

        //    // Aktualizacja
        //    var updateModel = new ApartmentUpdateDTO
        //    {
        //        Id = id,
        //        Name = "Updated Apartment",
        //        Details = "After update",
        //        Rate = 150,
        //        Sqft = 60,
        //        Occupancy = 3,
        //        ImageUrl = "updated.jpg",
        //        Amenity = "Pool"
        //    };

        //    var response = await _client.PutAsJsonAsync($"/api/v1/apartmentApi/{id}", updateModel);
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
    }
}
