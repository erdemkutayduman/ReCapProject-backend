using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsUI.Abstract
{
    public interface ISqlService
    {
        IDataResult<List<Car>> GetAll();
        SqlConnection Connection();
    }
}
