using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ummyapp0027.Pages
{
    public class BusBookingModel : PageModel
    {
        public static readonly string[] Cities =
        {
            "Bengaluru", "Chennai", "Hyderabad", "Mumbai", "Pune",
            "Goa", "Kochi", "Coimbatore", "Mysuru", "Delhi"
        };

        [BindProperty]
        [Required(ErrorMessage = "Please select a departure city")]
        [Display(Name = "From")]
        public string? From { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a destination city")]
        [Display(Name = "To")]
        public string? To { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please choose a travel date")]
        [DataType(DataType.Date)]
        [Display(Name = "Travel Date")]
        public DateTime? TravelDate { get; set; } = DateTime.Today;

        [BindProperty]
        [Range(1, 6, ErrorMessage = "Passengers must be between 1 and 6")]
        public int Passengers { get; set; } = 1;

        public List<BusResult>? Results { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(From) && From == To)
                ModelState.AddModelError(nameof(To), "Source and destination cannot be the same");

            if (TravelDate.HasValue && TravelDate.Value.Date < DateTime.Today)
                ModelState.AddModelError(nameof(TravelDate), "Travel date cannot be in the past");

            if (!ModelState.IsValid)
                return Page();

            // TODO: replace this sample data with a database / API call
            Results = new List<BusResult>
            {
                new() { Id = 1, Operator = "VRL Travels",  BusType = "Volvo AC Sleeper",    DepartureTime = "21:30", ArrivalTime = "05:45", Duration = "8h 15m", Price = 1150, SeatsAvailable = 14, Rating = 4.4 },
                new() { Id = 2, Operator = "SRS Travels",  BusType = "AC Semi-Sleeper",     DepartureTime = "22:15", ArrivalTime = "06:30", Duration = "8h 15m", Price = 950,  SeatsAvailable = 22, Rating = 4.1 },
                new() { Id = 3, Operator = "KPN Travels",  BusType = "Non-AC Seater",       DepartureTime = "23:00", ArrivalTime = "08:00", Duration = "9h 00m", Price = 620,  SeatsAvailable = 31, Rating = 3.9 },
                new() { Id = 4, Operator = "Orange Tours", BusType = "Multi-Axle AC Volvo", DepartureTime = "20:45", ArrivalTime = "04:30", Duration = "7h 45m", Price = 1380, SeatsAvailable = 6,  Rating = 4.6 },
            };

            return Page();
        }

        // Placeholder for the next step: seat selection
        public IActionResult OnGetSelectSeats(int busId, string from, string to, DateTime date, int passengers)
        {
            return Content($"Seat selection for bus #{busId}: {from} to {to} on {date:dd MMM yyyy}, {passengers} passenger(s)");
        }
    }

    public class BusResult
    {
        public int Id { get; set; }
        public string Operator { get; set; } = "";
        public string BusType { get; set; } = "";
        public string DepartureTime { get; set; } = "";
        public string ArrivalTime { get; set; } = "";
        public string Duration { get; set; } = "";
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
        public double Rating { get; set; }
    }
}
