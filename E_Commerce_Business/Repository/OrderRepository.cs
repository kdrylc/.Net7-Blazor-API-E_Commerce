using AutoMapper;
using E_Commerce_Business.Repository.IRepository;
using E_Commerce_Common;
using E_Commerce_DataAccess;
using E_Commerce_DataAccess.Data;
using E_Commerce_DataAccess.Models;
using E_Commerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public OrderRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(orderDTO);
                _context.OrderHeaders.Add(obj.OrderHeader);
                await _context.SaveChangesAsync();
                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                _context.OrderDetails.AddRange(obj.OrderDetails);
                await _context.SaveChangesAsync();
                return new OrderDTO()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList(),

                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderDTO;
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (objHeader != null)
            {
                IEnumerable<OrderDetail> objDetail = _context.OrderDetails.Where(x => x.OrderHeaderId == id);
                _context.OrderDetails.RemoveRange(objDetail);
                _context.OrderHeaders.Remove(objHeader);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<OrderDTO> Get(int id)
        {
            Order order = new Order
            {
                OrderHeader = _context.OrderHeaders.FirstOrDefault(x => x.Id == id),
                OrderDetails = _context.OrderDetails.Where(x => x.OrderHeaderId == id)
            };
           if (order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
           return new OrderDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            List<Order> orderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _context.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _context.OrderDetails;
            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(x => x.OrderHeaderId == header.Id),
                };
                orderFromDb.Add(order);
            }
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orderFromDb);

        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccess(int id)
        {
            var data = await _context.OrderHeaders.FindAsync(id);
            if (data == null)
            {
               return new OrderHeaderDTO();
            }
            if (data.Status == Keys.Pending)
            {
                data.Status = Keys.Status_Confirmed;
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO orderHeaderDTO)
        {
            if (orderHeaderDTO != null)
            {
                var orderHeaderFromDb = _context.OrderHeaders.FirstOrDefault(x => x.Id == orderHeaderDTO.Id);
                orderHeaderFromDb.FirstName = orderHeaderDTO.FirstName;
                orderHeaderFromDb.LastName = orderHeaderDTO.LastName;
                orderHeaderFromDb.Address = orderHeaderDTO.Address;
                orderHeaderFromDb.AdditionalInformation = orderHeaderDTO.AdditionalInformation;
                orderHeaderFromDb.PostalCode = orderHeaderDTO.PostalCode;
                orderHeaderFromDb.Phone = orderHeaderDTO.Phone;
                orderHeaderFromDb.Status = orderHeaderDTO.Status;
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeaderFromDb);

            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var result = await _context.OrderHeaders.FindAsync(orderId);
            if (result == null)
            {
                return false;
            }
            result.Status = status;
            if (status == Keys.Status_Shipped)
            {
                result.ShippingDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
