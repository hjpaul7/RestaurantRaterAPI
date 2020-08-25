using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant Entity (class that gets stored in database)
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Rating { get; set; }

        public bool isRecommended => Rating > 3.5;
    }
}