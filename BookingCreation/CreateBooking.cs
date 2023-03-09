
using System;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingPlain
    {
        public static ConfirmedBooking CreateBooking(BookingRequest bookingRequest)
        {
            var validatedBooking = ValidateBooking(bookingRequest);
            var bookingNumber = GenerateBookingNumber(validatedBooking);
            var bookingFees = CalculateFees(validatedBooking);
            var bookingAcknowledgement = CreateBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees);
            
            return new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
        }
        private static BookingAcknowledgement CreateBookingAcknowledgement(ValidatedBooking validatedBooking, BookingNumber bookingNumber, BookingFees bookingFees)
        {
            throw new NotImplementedException();
        }
        private static BookingNumber GenerateBookingNumber(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static BookingFees CalculateFees(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static ValidatedBooking ValidateBooking(BookingRequest bookingRequest)
        {
            throw new NotImplementedException();
        }
    }
}
