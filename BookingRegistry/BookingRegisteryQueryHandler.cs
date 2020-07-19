using System.Collections.Generic;
using System.Linq;

namespace HotelBookingSystem.BookingRegistry {
    public class BookingRegisteryQueryHandler : IBookingRegisteryQueryHandler {
        private readonly BookingRecordContext context;

        public BookingRegisteryQueryHandler(BookingRecordContext context) {
            this.context = context;
        }
        public IEnumerable<BookingRecord> GetBookingRecordsBySpecification(Specification spedification) {
             return context.BookingRecords
                .Where(b => spedification.IsSatisfiedBy(b))
                .ToList();
        }
    }
}