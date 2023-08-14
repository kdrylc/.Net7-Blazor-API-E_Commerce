using E_Commerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Business.Repository.IRepository
{
    public interface IDiscountRepository
    {
        public Task<DiscountDTO> CampaignCode(DiscountDTO discountDTO);
        public Task<ServiceResponse<DiscountDTO>> ImplementCode(string code);

    }
}
