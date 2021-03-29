using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditScore : IEntity
    {
        public int CreditScoreId { get; set; }
        public int CarId { get; set; }
        public int MinCreditScore { get; set; }
    }
}
