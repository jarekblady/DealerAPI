using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DealerAPI.Entities;
using DealerAPI.Models;
using DealerAPI.Exceptions;

namespace DealerAPI.Services
{
    public interface IDealerService
    {
        IEnumerable<DealerDto> GetAll();
        DealerDto GetById(int id);
        int Create(CreateDealerDto dto);
        void Update(int id, UpdateDealerDto dto);
        void Delete(int id);
    }

    public class DealerService : IDealerService
    {
        private readonly DealerDbContext _dbContext;
        private readonly IMapper _mapper;
        public DealerService(DealerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public DealerDto GetById(int id)
        {
            var dealer = _dbContext
                .Dealers
                .Include(d => d.Address)
                .Include(d => d.Cars)
                .FirstOrDefault(d => d.Id == id);

            if (dealer is null)
                throw new NotFoundException("Dealer not found");

            var dealerDto = _mapper.Map<DealerDto>(dealer);

            return dealerDto;
        }
        public IEnumerable<DealerDto> GetAll()
        {
            var dealers = _dbContext
                .Dealers
                .Include(d => d.Address)
                .Include(d => d.Cars)
                .ToList();

            var dealersDtos = _mapper.Map<List<DealerDto>>(dealers);

            return dealersDtos;

        }

        public int Create(CreateDealerDto dto)
        {
            var dealer = _mapper.Map<Dealer>(dto);
            _dbContext.Dealers.Add(dealer);
            _dbContext.SaveChanges();

            return dealer.Id;
        }

        public void Update(int id, UpdateDealerDto dto)
        {
            var dealer = _dbContext
                .Dealers
                .FirstOrDefault(r => r.Id == id);

            if (dealer is null)
                throw new NotFoundException("Dealer not found");

            dealer.Name = dto.Name;
            dealer.Description = dto.Description;
            dealer.ContactEmail = dto.ContactEmail;
            dealer.ContactNumber = dto.ContactNumber;

            _dbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            var dealer = _dbContext
                .Dealers
                .FirstOrDefault(d => d.Id == id);

            if (dealer is null)
                throw new NotFoundException("Dealer not found");

            _dbContext.Dealers.Remove(dealer);
            _dbContext.SaveChanges();

        }
    }
}
