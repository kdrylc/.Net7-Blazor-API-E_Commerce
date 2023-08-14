using AutoMapper;
using E_Commerce_Business.Repository.IRepository;
using E_Commerce_DataAccess;
using E_Commerce_DataAccess.Data;
using E_Commerce_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Business.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DiscountRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DiscountDTO> CampaignCode(DiscountDTO discountDTO)
        {
            var result = _mapper.Map<DiscountDTO,Discount>(discountDTO);
          var addedObj =  _context.Discounts.Add(result);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<Discount, DiscountDTO>(addedObj.Entity);
            return response;
        }

        public async Task<ServiceResponse<DiscountDTO>> ImplementCode(string code)
        {
            var result = await _context.Discounts.FirstOrDefaultAsync(x => x.DiscountCode.ToLower().Equals(code.ToLower()));
            var response = _mapper.Map<Discount, DiscountDTO>(result);
            if (result == null)
            {
                return new ServiceResponse<DiscountDTO>
                {
                    Success = false,
                    Message = "Invalid Code",
                    
                };
            }
            return new ServiceResponse<DiscountDTO>
            {
                Success = true,
                Message = $"Discount  Code is found %{result.DiscountAmount} Implemented",
                Data = response,
               
            };
        }
    }
}
