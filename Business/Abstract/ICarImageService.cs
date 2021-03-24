using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService : IBaseService<CarImage>
    {
        IDataResult<CarImage> Get(int id);
        IDataResult<List<CarImage>> GetImagesByCarId(int id);
        IDataResult<List<CarImage>> GetAllByCarId(int carId);

    }
}
