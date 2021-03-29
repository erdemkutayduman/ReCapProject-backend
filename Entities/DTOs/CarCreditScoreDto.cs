using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarCreditScoreDto : IDto
    {
        public int CustomerCarCreditScore { get; set; }
        public int CarMinCarCreditScore { get; set; }
    }
}
