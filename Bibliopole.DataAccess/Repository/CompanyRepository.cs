﻿using System;
using System.Linq.Expressions;
using Bibliopole.DataAccess.Repository.IRepository;
using Bibliopole.Models;

namespace Bibliopole.DataAccess.Repository
{
    public class CompanyRepository :Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
