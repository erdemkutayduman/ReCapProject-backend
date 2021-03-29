using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService : IBaseService<Customer>
    {
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    }
}
