using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int brandid);
        IDataResult<List<Car>> GetCarsByColorId (int colorid);
        IDataResult<List<Car>> GetCarsByOrderId(int orderid);
        IDataResult<List<CarDetailDto>> GetCarsDetail();
        IDataResult<Car> GetById(int carId);
        IResult Add(Car car);

    }
}
