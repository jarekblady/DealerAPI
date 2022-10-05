using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DealerAPI.Entities;
using DealerAPI.Exceptions;
using DealerAPI.Models;

namespace DealerAPI.Services
{
    public interface ICarService
    {
        List<CarDto> GetAll(int dealerId);
        CarDto GetById(int dealerId, int carId);
        int Create(int dealerId, CreateCarDto dto);
        void Update(int dealerId, int carId, UpdateCarDto dto);
        void DeleteAll(int dealerId);
        void Delete(int dealerId, int carId);
    }

    public class CarService : ICarService
    {
        private readonly DealerDbContext _context;
        private readonly IMapper _mapper;

        public CarService(DealerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CarDto> GetAll(int dealerId)
        {
            var dealer = GetDealerById(dealerId);

            var carDtos = _mapper.Map<List<CarDto>>(dealer.Cars);

            return carDtos;
        }

        public CarDto GetById(int dealerId, int carId)
        {
            var dealer = GetDealerById(dealerId);

            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
            if (car is null || car.DealerId != dealerId)
            {
                throw new NotFoundException("Car not found");
            }

            var carDto = _mapper.Map<CarDto>(car);
            return carDto;
        }

        private Dealer GetDealerById(int dealerId)
        {
            var dealer = _context
                .Dealers
                .Include(d => d.Cars)
                .FirstOrDefault(d => d.Id == dealerId);

            if (dealer is null)
                throw new NotFoundException("Dealer not found");

            return dealer;
        }

        public int Create(int dealerId, CreateCarDto dto)
        {
            GetDealerById(dealerId);

            var carEntity = _mapper.Map<Car>(dto);

            carEntity.DealerId = dealerId;

            _context.Cars.Add(carEntity);
            _context.SaveChanges();

            return carEntity.Id;
        }

        public void Update(int dealerId, int carId, UpdateCarDto dto)
        {
            var dealer = GetDealerById(dealerId);

            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
            if (car is null || car.DealerId != dealerId)
            {
                throw new NotFoundException("Car not found");
            }

            car.Make = dto.Make;
            car.Model = dto.Model;
            car.Year = dto.Year;
            car.Fuel = dto.Fuel;
            car.EnginePower = dto.EnginePower;
            car.Price = dto.Price;
            car.Description = dto.Description;
            car.DealerId = dealerId;

            _context.SaveChanges();
        }

        public void DeleteAll(int dealerId)
        {
            var dealer = GetDealerById(dealerId);

            _context.RemoveRange(dealer.Cars);
            _context.SaveChanges();

        }
        public void Delete(int dealerId, int carId)
        {
            var dealer = GetDealerById(dealerId);

            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
            if (car is null || car.DealerId != dealerId)
            {
                throw new NotFoundException("Car not found");
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

        }


    }
}
