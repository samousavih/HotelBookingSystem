using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.BookingRegistry {
    public class BookingRecordContext {
        public DbSet<BookingRecord> BookingRecords { get; set; }
    }
}