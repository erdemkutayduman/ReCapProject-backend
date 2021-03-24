using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService : IBaseService<Car>
    {
        IDataResult<List<Car>> GetAllByColor(int id);
        IDataResult<List<Car>> GetAllByBrand(int id);        
        IDataResult<List<Car>> GetCarsByBrand(int brandId);
        IDataResult<List<Car>> GetCarsByColor(int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<Car, bool>> filter = null);
        IDataResult<List<CarDetailDto>> GetCarDetailsByCar(int id);
        IDataResult<List<CarDetailDto>> GetCarDetailsByBrand(int id);
        IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int id);
        IDataResult<List<CarDetailDto>> GetCarDetailsByFilter(int brandId, int colorId);
        IResult AddTransactionalTest(Car car);
        
    }
}
