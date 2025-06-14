using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagement.Application.DTO;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entites;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyDto> GetByIdAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
                throw new Exception("Company not found.");

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> CreateAsync(CompanyCreateDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            company.Id = Guid.NewGuid();

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
    }
}
