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
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, EfContext>, ICarImageDal
    {
        public List<CarImageDetailDto> GetCarImageDetail(Expression<Func<CarImage, bool>> filter = null)
        {
            using (EfContext context = new EfContext())
            {
                var result = from c in filter == null ? context.CarImages : context.CarImages.Where(filter)
                             join v in context.Cars on c.CarId equals v.CarId
                             join b in context.Brands on v.BrandId equals b.BrandId
                             join co in context.Colors on v.ColorId equals co.ColorId
                             select new CarImageDetailDto
                             {
                                 CarId = v.CarId,
                                 CarName = v.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = v.ModelYear,
                                 DailyPrice = v.DailyPrice,
                                 Description = v.Description,
                                 ImagePath = c.ImagePath,
                                 ImageDate = c.ImageDate

                             };

                return result.ToList();
            }
        }
    }
}
