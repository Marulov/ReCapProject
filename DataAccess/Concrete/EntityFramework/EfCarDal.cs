﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentCarContext>, ICarDal
    {

        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from c in filter is null ? context.Cars : context.Cars.Where(filter)
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join cl in context.Colors
                             on c.ColorId equals cl.ColorId
                             join i in context.CarImages
                             on c.CarId equals i.CarId
                             select new CarDetailDto { CarId = c.CarId, CarName = c.CarName, BrandName = b.BrandName, ColorName = cl.ColorName, DailyPrice = c.DailyPrice , Descriptions = c.Description, ModelYear = c.ModelYear, ImagePath =i.ImagePath};
                return result.ToList();
            }
        }
    }
}
