using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using DealerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection;
using DealerAPI.Models;
using System.Text;
using Newtonsoft.Json;
using DealerAPI.Tests.Helpers;

namespace DealerAPI.Tests

{
    public class DealerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private WebApplicationFactory<Startup> _factory;
        public DealerControllerTests(WebApplicationFactory<Startup> factory)
        {
            //var factory = new WebApplicationFactory<Startup>();
            //_client = factory.CreateClient();

            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DealerDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));


                        services
                            .AddDbContext<DealerDbContext>(options => options.UseInMemoryDatabase("DealerDb"));

                    });
                 });

            _client = _factory.CreateClient();
        }

        private void SeedDealer(Dealer dealer)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DealerDbContext>();

            _dbContext.Dealers.Add(dealer);
            _dbContext.SaveChanges();
        }


        [Fact]
        public async Task Delete_ForExistingDealer_ReturnsNoContent()
        {
            // arrange

            var dealer = new Dealer()
            {
                Name = "Test"
            };

            SeedDealer(dealer);

            // act
            var response = await _client.DeleteAsync("/api/dealer/" + dealer.Id);


            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task Delete_ForNonExistingDealer_ReturnsNotFound()
        {
            // act

            var response = await _client.DeleteAsync("/api/dealer/567");

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }


        [Fact]
        public async Task CreateDealer_WithValidModel_ReturnsCreatedStatus()
        {
            // arrange
            var model = new CreateDealerDto()
            {
                Name = "BBBCars",
                City = "Warszawa",
                Street = "Mickiewicza",
                HouseNumber = "5"
            };

            var httpContent = model.ToJsonHttpContent();

            //var json = JsonConvert.SerializeObject(model);

            //var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // act
            var response = await _client.PostAsync("/api/dealer", httpContent);

            // arrange 

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateDealer_WithInvalidModel_ReturnsBadRequest()
        {
            // arrange
            var model = new CreateDealerDto()
            {
                Name = "BBBCars",
                City = "Warszawa"
            };

            var httpContent = model.ToJsonHttpContent();

            // act
            var response = await _client.PostAsync("/api/dealer", httpContent);

            // arrange

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task GetAll_GetDealers_ReturnsOkResult()
        {
            //arrange

            //var factory = new WebApplicationFactory<Startup>();
            //var client = factory.CreateClient();

            // act
           
            var response = await _client.GetAsync("/api/dealer");
            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
