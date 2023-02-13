using System;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product> 
    {
        void Update(Product obj);
    }
}

