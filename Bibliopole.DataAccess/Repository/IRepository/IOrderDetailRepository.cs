using System;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail> 
    {
        void Update(OrderDetail obj);
    }
}

