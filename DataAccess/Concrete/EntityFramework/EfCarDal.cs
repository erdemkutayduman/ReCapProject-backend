using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, EfContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsDetail(Expression<Func<Car, bool>> filter = null)
        {
            using (EfContext context = new EfContext())
            {
                var result = from v in filter == null ? context.Cars : context.Cars.Where(filter)
                             join c in context.Colors
                             on v.ColorId equals c.ColorId
                             join b in context.Brands
                             on v.BrandId equals b.BrandId
                             select new CarDetailDto
                             {
                                 CarId = v.CarId,
                                 ColorId = c.ColorId,
                                 BrandId = b.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 DailyPrice = v.DailyPrice,
                                 Description = v.Description,
                                 ModelYear = v.ModelYear
                             };
                return result.ToList();
            }
        }
    }
}