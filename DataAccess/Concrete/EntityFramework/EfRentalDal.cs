using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, EfContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (EfContext context = new EfContext())
            {
                var result = from r in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join v in context.Cars
                             on r.CarId equals v.CarId
                             join c in context.Customers
                             on r.CustomerId equals c.CustomerId
                             join b in context.Brands
                             on v.BrandId equals b.BrandId
                             join u in context.Users
                             on c.UserId equals u.UserId
                             select new RentalDetailDto
                             {
                                 Id = r.Id,
                                 CarName = b.BrandName,
                                 CustomerName = c.CustomerName,
                                 UserName = u.FirstName + " " + u.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
