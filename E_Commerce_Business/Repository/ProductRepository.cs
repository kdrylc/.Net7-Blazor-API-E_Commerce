using AutoMapper;
using E_Commerce_Business.Repository.IRepository;
using E_Commerce_DataAccess.Data;
using E_Commerce_DataAccess;
using E_Commerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Business.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var obj = _mapper.Map<ProductDTO, Product>(objDTO);
            var addedObj = _db.Products.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                _db.Products.Remove(obj);
                await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(x => x.Category).Include(x => x.ProductPrices));
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var obj = await _db.Products.Include(x => x.Category).Include(x => x.ProductPrices).FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<List<ProductDTO>> GetProductByCategoryId(int id)
        {
            var result = await _db.Products.Where(x => x.CategoryId == id).ToListAsync();
            var objProd = _mapper.Map<List<Product>, List<ProductDTO>>(result);
            if (objProd.Count > 0)
            {
                return objProd;
            }
            return new List<ProductDTO>();
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var objFromDb = await _db.Products.FirstOrDefaultAsync(x => x.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.CategoryId = objDTO.CategoryId;
                objFromDb.Author = objDTO.Author;
                objFromDb.ShopFavourites = objDTO.ShopFavourites;
                objFromDb.CustomerFavourites = objDTO.CustomerFavourites;
                _db.Products.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}
