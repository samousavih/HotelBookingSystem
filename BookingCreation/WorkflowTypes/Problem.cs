namespace HotelBookingSystem.BookingCreation
{
    public record Problem
    {
        public string Title { get; set; }
        public int Code { get; set; }
        public string Detail { get; set; }
    }
    public static class Problems {
        public static Problem Nofee  = new Problem ();
    }
}

