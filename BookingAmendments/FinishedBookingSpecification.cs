using HotelBookingSystem.BookingRegistry;

namespace HotelBookingSystem.BookingAmendments {
    public class FinishedBookingSpecification : Specification {
        public FinishedBookingSpecification() {
            this.Expression = bookingRecord => bookingRecord.Status == BookingRecordStatus.Finished;
        }
    }
}