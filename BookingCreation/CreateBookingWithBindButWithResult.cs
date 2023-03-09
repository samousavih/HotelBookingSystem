using System;
using CSharpFunctionalExtensions;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingWithBindButWithResult
    {
        public static Result<ConfirmedBooking, Problem> CreateBooking(BookingRequest bookingRequest)
        {
            return ValidateBooking(bookingRequest)
                .Bind(validatedBooking =>
                {
                    return GenerateBookingNumber(validatedBooking)
                        .Bind(bookingNumber =>
                        {
                            return CalculateFees(validatedBooking)
                                .Bind(bookingFees =>
                                {
                                    return CreateBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
                                        .Map(bookingAcknowledgement => new ConfirmedBooking
                                        {
                                            ValidatedBooking = validatedBooking,
                                            BookingNumber = bookingNumber,
                                            BookingAcknowledgement = bookingAcknowledgement,
                                        });
                                });
                        });
                });
        }
        private static Result<BookingAcknowledgement,Problem> CreateBookingAcknowledgement(ValidatedBooking validatedBooking, BookingNumber bookingNumber, BookingFees bookingFees)
        {
            throw new NotImplementedException();
        }
        private static Result<BookingNumber,Problem> GenerateBookingNumber(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static Result<BookingFees,Problem> CalculateFees(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static Result<ValidatedBooking,Problem> ValidateBooking(BookingRequest bookingRequest)
        {
            throw new NotImplementedException();
        }
    }

}
