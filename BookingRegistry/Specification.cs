using System;
using System.Linq.Expressions;

namespace HotelBookingSystem.BookingRegistry {
    public abstract class Specification {
        public Expression<Func<BookingRecord, bool>> Expression { get; set; }        
    }
}