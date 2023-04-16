namespace HotelBookingSystem.BookingCreation
{
    public record ConfirmedBooking
    {
        public ValidatedBooking ValidatedBooking { get; set; }
        public BookingNumber BookingNumber { get; set; }
        public BookingAcknowledgement BookingAcknowledgement { get; set; }
    }
}