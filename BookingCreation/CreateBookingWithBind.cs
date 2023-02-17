using System;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingWithBind
    {
        public static Either<Problem, ConfirmedBooking> CreateBooking(BookingRequest bookingRequest)
        {
            return ValidateBooking(bookingRequest)
                .Bind(validatedBooking =>
            {
                return GenerateBookingNumber(bookingRequest)
                    .Bind(bookingNumber =>
                {
                    return CalculateFees(validatedBooking)
                        .Bind<ConfirmedBooking>(bookingFees =>
                        {
                            var bookingAcknowledgement = CreateBookingAcknowledgement(bookingRequest, bookingNumber, bookingFees);
                            return new ConfirmedBooking
                            {
                                BookingRequest = bookingRequest,
                                BookingNumber = bookingNumber,
                                BookingAcknowledgement = bookingAcknowledgement,
                            };
                        });
                });
            });
        }
        private static BookingAcknowledgement CreateBookingAcknowledgement(BookingRequest bookingRequest, BookingNumber bookingNumber, BookingFees bookingFees)
        {
            throw new NotImplementedException();
        }
        private static Either<Problem,BookingNumber> GenerateBookingNumber(BookingRequest bookingRequest)
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
