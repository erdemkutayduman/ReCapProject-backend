using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public IResult Add(Order entity)
        {
            if (entity.ShipCity.Length >= 2)
            {
                _orderDal.Add(entity);
                return new SuccessDataResult<List<Order>>(Messages.OrderAdded);
            }
            else
            {
                return new ErrorDataResult<List<Order>>(Messages.InvalidOrder);
            }
        }

        public IResult Delete(Order entity)
        {
            _orderDal.Delete(entity);
            return new SuccessDataResult<List<Order>>(Messages.OrderDeleted);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll());
        }

        public IDataResult<Order> GetById(int id)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(c => c.OrderId == id));
        }

        public IResult Update(Order entity)
        {
            if (entity.ShipCity.Length >= 2)
            {
                _orderDal.Update(entity);
                return new SuccessDataResult<List<Order>>(Messages.OrderUpdated);
            }
            else
            {
                return new ErrorDataResult<List<Order>>(Messages.InvalidOrder);
            }
        }
    }
}
