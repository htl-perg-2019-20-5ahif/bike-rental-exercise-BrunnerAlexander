using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Model
{
    public enum GenderType
    {
        Male,
        Female, 
        Unknown
    }

    public class Customer
    {
        public int CustomerId { get; set; }

        public GenderType Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public String LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(75)]
        public String Street { get; set; }

        [Required]
        [MaxLength(10)]
        public String HouseNumber { get; set; }

        [Required]
        [MaxLength(75)]
        public String ZipCode { get; set; }

        [Required]
        [MaxLength(75)]
        public String Town { get; set; }

        public List<Rental> Rentals { get; set; }
    }
}
