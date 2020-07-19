using HotelBookingSystem.BookingRegistry;

namespace HotelBookingSystem.BookingAmendments {
    public class FinishedBookingSpecification : Specification {
        public override bool IsSatisfiedBy(BookingRecord bookingRecord) {
            return bookingRecord.Status == BookingRecordStatus.Finished;
        }
    }
}