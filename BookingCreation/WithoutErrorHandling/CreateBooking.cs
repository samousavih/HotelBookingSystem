
using System;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingPlain
    {
        public static ConfirmedBooking CreateBooking(BookingRequest bookingRequest,
            Func<BookingRequest, ValidatedBooking> validateBooking,
            Func<ValidatedBooking, BookingNumber> generateBookingNumber,
            Func<ValidatedBooking, BookingFees> calculateFees,
            Func<ValidatedBooking, BookingNumber, BookingFees, BookingAcknowledgement> createBookingAcknowledgement)
        {
            var validatedBooking = validateBooking(bookingRequest);
            var bookingNumber = generateBookingNumber(validatedBooking);
            var bookingFees = calculateFees(validatedBooking);
            var bookingAcknowledgement = createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees);
            
            return new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
        }
    }
}
