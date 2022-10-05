using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DealerAPI.Entities;

namespace DealerAPI
{
    public class DealerSeeder
    {
        private readonly DealerDbContext _dbContext;

        public DealerSeeder(DealerDbContext dbContext)//wstrzyknięcie kontekstu bazy danych przez konstruktor
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                
                if (!_dbContext.Dealers.Any())
                {
                    var dealers = GetDealers();
                    _dbContext.Dealers.AddRange(dealers);
                    _dbContext.SaveChanges();
                }
            }
        }       
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        }
        
        private IEnumerable<Dealer> GetDealers()
        {
            var dealers = new List<Dealer>()
            {
                new Dealer()
                {
                    Name = "AAACars",
                    Description = "Description1",
                    ContactEmail = "aaacars@email.com",
                    ContactNumber = "999789654",
                    Cars = new List<Car>()
                    {
                        new Car()
                        {
                            Make = "BMW",
                            Model = "X5",
                            Year = 2020,
                            Fuel = "Gasoline",
                            EnginePower = 200,
                            Price = 100000.00M,
                        },

                        new Car()
                        {
                            Make = "Audi",
                            Model = "A6",
                            Year = 2022,
                            Fuel = "Diesel",
                            EnginePower = 220,
                            Price = 150000.00M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        PostalCode = "28-400",
                        Street = "Mariacka",
                        HouseNumber = "120",

                    }
                },
                new Dealer()
                {
                    Name = "TTTCars",
                    Description = "Description2",
                    ContactEmail = "tttcars@email.com",
                    ContactNumber = "777888999",
                    Cars = new List<Car>()
                    {
                        new Car()
                        {
                            Make = "BMW",
                            Model = "X3",
                            Year = 2018,
                            Fuel = "Gasoline",
                            EnginePower = 150,
                            Price = 80000.00M,
                        },

                        new Car()
                        {
                            Make = "Audi",
                            Model = "Q5",
                            Year = 2021,
                            Fuel = "Diesel",
                            EnginePower = 180,
                            Price = 160000.00M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kielce",
                        PostalCode = "41-200",
                        Street = "Opolska",
                        HouseNumber = "50",

                    }
                },
            };

            return dealers;
        }
    }
}

