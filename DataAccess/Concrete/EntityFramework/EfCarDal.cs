using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, EfContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsDetail()
        {
            using (EfContext context = new EfContext())
            {
                var result = from v in context.Cars
                             join c in context.Colors
                             on v.ColorId equals c.ColorId
                             join b in context.Brands
                             on v.BrandId equals b.BrandId
                             join o in context.Orders
                             on v.OrderId equals o.OrderId
                             select new CarDetailDto
                             {
                                 CarId = v.CarId,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 DailyPrice = v.DailyPrice,
                                 Description = v.Description,
                                 ModelYear = v.ModelYear,

                             };
                return result.ToList();
            }
        }
    }
}