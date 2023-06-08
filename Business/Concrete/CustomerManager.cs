﻿using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstarct;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IDataResult<List<Customer>> GetList()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetList().ToList());
        }
    }
}
