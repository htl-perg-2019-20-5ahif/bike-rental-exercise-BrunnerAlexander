using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Service
{
    public interface ICalculation
    {
        public decimal CalculateCost(DateTime rentalBegin, DateTime rentalEnd, decimal rentalPriceFirstHour, decimal rentalPricePerAdditionalHour);
    }
}
