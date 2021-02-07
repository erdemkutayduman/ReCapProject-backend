using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand entity)
        {
            if (entity.BrandName.Length > 2)
            {
                _brandDal.Add(entity);
                Console.WriteLine("The brand is successfully added.");
            }
            else
            {
                Console.WriteLine($"Enter the length of the brand name more than 2 characters. The brand name you entered: {entity.BrandName}");
            }
        }

        public void Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            Console.WriteLine("The brand is successfully deleted.");

        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public Brand GetById(int id)
        {
            return _brandDal.Get(c => c.BrandId == id);
        }

        public void Update(Brand entity)
        {
            if (entity.BrandName.Length >= 2)
            {
                _brandDal.Update(entity);
                Console.WriteLine("The brand is successfully updated.");
            }
            else
            {
                Console.WriteLine($"Enter the length of the brand name more than 2 characters. The brand name you entered: : {entity.BrandName}");
            }

        }
    }
}
