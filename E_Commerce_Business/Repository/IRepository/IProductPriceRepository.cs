using E_Commerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Business.Repository.IRepository
{
    public interface IProductPriceRepository
    {
        public Task<ProductPriceDTO> Create(ProductPriceDTO objDTO);
        public Task<ProductPriceDTO> Update(ProductPriceDTO objDTO);
        public Task<int> Delete(int id);
        public Task<ProductPriceDTO> GetById(int id);
        public Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null);
    }
}
