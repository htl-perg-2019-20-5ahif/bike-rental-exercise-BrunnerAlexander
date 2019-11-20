using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Model
{
    public class Rental
    { 
        public int RentalId { get; set; }

        public DateTime RentalBegin { get; set; }

        public DateTime rentalEnd = DateTime.MaxValue;
        public DateTime RentalEnd
        {
            get { return rentalEnd;  }
            set
            {
                if(value < RentalBegin)
                {
                    throw new ArgumentException("RentalEnd must be later than RentalBegin");
                }
                rentalEnd = value;
            }
        }

        [Range(0.00, Double.MaxValue)]
        [RegularExpression(@"\d*.\d{1,2}", ErrorMessage = "Wrong format")]
        public Decimal RentalCost { get; set; }

        public Boolean Paid
        {
            get { return Paid; }
            set
            {
                if(rentalEnd < DateTime.Now || RentalCost == default)
                {
                    throw new ArgumentException("Can only be paid when the rental already ended");
                }
                Paid = value;
            }
        }


        // Keys

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public int BikeId { get; set; }
        
        public Bike Bike { get; set; }
    }
}
