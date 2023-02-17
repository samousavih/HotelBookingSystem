namespace HotelBookingSystem.BookingCreation
{
    public class ConfirmedBooking
    {
        public BookingRequest BookingRequest { get; set; }
        public BookingNumber BookingNumber { get; set; }
        public BookingAcknowledgement BookingAcknowledgement { get; set; }
    }
}