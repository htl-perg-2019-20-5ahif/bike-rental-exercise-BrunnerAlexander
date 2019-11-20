using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Service
{
    public class Calculation : ICalculation
    {
        public decimal CalculateCost(DateTime rentalBegin, DateTime rentalEnd, decimal rentalPriceFirstHour, decimal rentalPricePerAdditionalHour)
        {
            var duration = rentalEnd - rentalBegin;
            
            if(duration <= TimeSpan.FromMinutes(15))
            {
                return 0;
            }

            decimal totalCost = rentalPriceFirstHour;
            int hours = (int)Math.Ceiling(duration.TotalHours - 1);
            totalCost += hours * rentalPricePerAdditionalHour;

            return totalCost;
        }
    }
}
