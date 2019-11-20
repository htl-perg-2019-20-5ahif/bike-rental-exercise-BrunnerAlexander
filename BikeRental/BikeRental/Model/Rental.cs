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

        private DateTime rentalEnd = DateTime.MaxValue;
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
        public decimal RentalCost { get; set; }


        private bool paid = false;
        public bool Paid
        {
            get { return paid; }
            set
            {
                if(rentalEnd < DateTime.Now || RentalCost == default)
                {
                    throw new ArgumentException("Can only be paid when the rental already ended");
                }
                paid = value;
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
