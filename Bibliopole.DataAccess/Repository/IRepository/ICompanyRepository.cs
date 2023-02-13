using System;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company> 
    {
        void Update(Company obj);
    }
}

