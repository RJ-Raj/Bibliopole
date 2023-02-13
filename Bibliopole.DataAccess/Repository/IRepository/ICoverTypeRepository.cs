using System;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType> 
    {
        void Update(CoverType obj);
    }
}

