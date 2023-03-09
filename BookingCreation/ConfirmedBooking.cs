namespace HotelBookingSystem.BookingCreation
{
    public class ConfirmedBooking
    {
        public ValidatedBooking ValidatedBooking { get; set; }
        public BookingNumber BookingNumber { get; set; }
        public BookingAcknowledgement BookingAcknowledgement { get; set; }
    }
}