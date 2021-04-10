using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditScore : IEntity
    {
        public int CreditScoreId { get; set; }
        public int CustomerId { get; set; }
        public string NationalIdentity { get; set; }
        public int MinCreditScore { get; set; }
    }
}
