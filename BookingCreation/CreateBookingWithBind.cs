using System;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingWithBind
    {
        public static Either<Problem, ConfirmedBooking> CreateBooking(BookingRequest bookingRequest)
        {
            return ValidateBooking(bookingRequest)
                .Bind(validatedBooking => {
                return GenerateBookingNumber(validatedBooking)
                    .Bind(bookingNumber => {
                    return CalculateFees(validatedBooking)
                        .Bind(bookingFees => {
                            return CreateBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
                                .Bind<ConfirmedBooking>(bookingAcknowledgement => new ConfirmedBooking {
                                    ValidatedBooking = validatedBooking,
                                    BookingNumber = bookingNumber,
                                    BookingAcknowledgement = bookingAcknowledgement,
                                });
                        });
                });
            });
        }
        private static Either<Problem,BookingAcknowledgement> CreateBookingAcknowledgement(ValidatedBooking validatedBooking, BookingNumber bookingNumber, BookingFees bookingFees)
        {
            throw new NotImplementedException();
        }
        private static Either<Problem,BookingNumber> GenerateBookingNumber(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static Either<Problem,BookingFees> CalculateFees(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static Either<Problem,ValidatedBooking> ValidateBooking(BookingRequest bookingRequest)
        {
            throw new NotImplementedException();
        }
    }

}
