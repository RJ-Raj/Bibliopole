using System;
using System.Linq.Expressions;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository
{
    public class OrderHeaderRepository :Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == Id);
            if(orderFromDb!=null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int Id, string sessionId, string paymentIntentId )
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == Id);


            orderFromDb.PaymentDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentIntentId;
        }

        
    }
}

