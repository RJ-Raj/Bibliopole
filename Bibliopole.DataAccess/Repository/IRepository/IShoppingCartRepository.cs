using System;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository :IRepository<ShoppingCart> 
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}

