﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Admin> _adminRepository { get; set; }
        public IGenericRepository<Student> _studentRepository { get; set; }
        public UnitOfWork(IGenericRepository<Admin> adminRepository, IGenericRepository<Student> studentRepository)
        {
            _adminRepository = adminRepository;
            _studentRepository = studentRepository;
        }
    }
}
