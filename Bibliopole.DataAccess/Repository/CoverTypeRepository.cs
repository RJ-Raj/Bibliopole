using System;
using System.Linq.Expressions;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository
{
    public class CoverTypeRepository :Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}

