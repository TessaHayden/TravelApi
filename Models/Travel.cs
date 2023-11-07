using System.ComponentModel.DataAnnotations;

namespace TravelApi.Models
{
  public class Travel
  {
    public int TravelId { get; set; }
    [Required]
    [StringLength(200)]
    public string City { get; set; }
    [Required]
    [StringLength(70)]
    public string Country { get; set; }
    [Required]
    [StringLength(50)]
    public string User_Name { get; set; }
  }
}