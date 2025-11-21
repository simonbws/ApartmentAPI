using Apartment_API;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Tests
{
    public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly List<Type> _controllerTypes;
        private readonly WebApplicationFactory<Program> _factory;


        public ProgramTests(WebApplicationFactory<Program> factory)
        {
            // Pobieramy wszystkie kontrolery z Twojej aplikacji
            _controllerTypes = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            // Tworzymy fabrykę z możliwością nadpisania serwisów (opcjonalnie)
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Rejestrujemy kontrolery, aby DI je znalazło
                    _controllerTypes.ForEach(c => services.AddScoped(c));
                });
            });
        }
        [Fact]
        public void ConfigureServices_ForControllers_RegistersAllDependencies()
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            // we check if controllers are initialized correctly
            _controllerTypes.ForEach(controllerType =>
            {
                var controller = scope.ServiceProvider.GetService(controllerType);
                controller.Should().NotBeNull($"Controller {controllerType.Name} should be registered in DI");
            });
        }
    }
}
