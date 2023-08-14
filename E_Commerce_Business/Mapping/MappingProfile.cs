using AutoMapper;
using E_Commerce_DataAccess;
using E_Commerce_DataAccess.Models;
using E_Commerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();

        }
    }
}
