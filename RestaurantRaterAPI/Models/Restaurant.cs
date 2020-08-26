using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant Entity (class that gets stored in database)
    public class Restaurant
    {
        // Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Rating
        {
            get
            {
                double totalAverageRating = 0;

                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                return (Ratings.Count > 0) ? Math.Round(totalAverageRating / Ratings.Count, 2) : 0;
            }
        }

        // Average Food Rating
        public double FoodRating
        {
            get
            {
                double averageFoodScore = 0;

                foreach (var rating in Ratings)
                {
                    averageFoodScore += rating.FoodScore;
                }

                return (Ratings.Count > 0) ? Math.Round(averageFoodScore / Ratings.Count, 2) : 0;
            }
        }

        // Average Environment Rating
        public double EnvironmentRating
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore);

                double totalEnvironmentScore = scores.Sum();

                return (Ratings.Count > 0) ? Math.Round(totalEnvironmentScore / Ratings.Count, 2) : 0;
            }
        }

        // Average Cleanliness Rating
        public double CleanlinessRating
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return (Ratings.Count > 0) ? Math.Round(totalScore / Ratings.Count, 2) : 0;
            }
        }

        public bool isRecommended => Rating > 8.5;

        // All of the associated rating objects from the database

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}