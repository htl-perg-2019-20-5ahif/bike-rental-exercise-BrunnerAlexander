using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Model
{
    public enum Category
    {
        StandardBike,
        Mountainbike,
        TrackingBike,
        RacingBike
    }

    public class Bike
    {
        public int BikeId { get; set; }

        [Required]
        [MaxLength(25)]
        public String Brand { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public String Notes { get; set; }

        public DateTime DateOfLastService { get; set; }

        [Required]
        [Range(0.00, Double.MaxValue)]
        [RegularExpression(@"\d*.\d{1,2}", ErrorMessage = "Wrong format")]
        public decimal RentalPriceFirstHour {get; set;}

        [Required]
        [Range(0.00, Double.MaxValue)]
        [RegularExpression(@"\d*.\d{1,2}", ErrorMessage = "Wrong format")]
        public decimal RentalPriceAdditionalHour { get; set; }

        public Category BikeCategory { get; set; }

        public bool IsRented { get; set; }

        public List<Rental> Rentals { get; set; }
    }
}
