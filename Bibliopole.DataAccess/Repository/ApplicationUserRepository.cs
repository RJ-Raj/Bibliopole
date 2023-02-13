using System;
using System.Linq.Expressions;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository
{
    public class ApplicationUserRepository :Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}

