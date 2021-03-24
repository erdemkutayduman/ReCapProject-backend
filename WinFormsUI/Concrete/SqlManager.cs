using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsUI.Abstract;

namespace WinFormsUI.Concrete
{
    public class SqlManager
    {
        
        public SqlConnection Connection()
        {            
                SqlConnection con = new SqlConnection(@"Server = (localdb)\MSSQLLocalDB; Database=RentalCars; Trusted_Connection=true");
                con.Open();
                return con;            
        }

      
    }
}
