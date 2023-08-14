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
    public class ProductPriceRepository:IProductPriceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductPriceRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var obj = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);
            var addedObj = _db.ProductPrices.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                _db.ProductPrices.Remove(obj);
                await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            if (id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices.Where(x => x.ProductId == id));

            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices);

            }
        }

        public async Task<ProductPriceDTO> GetById(int id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(obj);
            }
            return new ProductPriceDTO();
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var objFromDb = await _db.ProductPrices.FirstOrDefaultAsync(x => x.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.ProductId = objDTO.ProductId;
                objFromDb.Price = objDTO.Price;
                objFromDb.Publisher = objDTO.Publisher;
                _db.ProductPrices.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(objFromDb);
            }
            return objDTO;
        }

    }
}
