namespace HotelBookingSystem.BookingRegistry {
    public abstract class Specification {
        public abstract bool IsSatisfiedBy(BookingRecord b);
    }
}