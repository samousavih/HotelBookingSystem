using System.Collections.Generic;

namespace HotelBookingSystem.BookingRegistry {
    public interface IBookingRegisteryQueryHandler {
        IEnumerable<BookingRecord> GetBookingRecordsBySpecification(Specification spedification);
    }
}