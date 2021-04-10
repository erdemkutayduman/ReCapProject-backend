using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CreditScoreDto : IDto
    {
        public int CustomerCreditScore { get; set; }
        public int MinCreditScore { get; set; }
    }
}
