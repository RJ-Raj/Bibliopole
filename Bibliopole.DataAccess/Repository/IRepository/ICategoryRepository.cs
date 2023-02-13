using System;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository :IRepository<Category> 
    {
        void Update(Category obj);
    }
}

