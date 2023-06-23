using System;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingWithBindImproved
    {
        public static Either<Problem, ConfirmedBooking> CreateBooking(
            BookingRequest bookingRequest,
            Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
            Func<ValidatedBooking, Either<Problem, BookingNumber>> generateBookingNumber,
            Func<ValidatedBooking, Either<Problem, BookingFees>> calculateFees,
            Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
        {
            return validateBooking(bookingRequest)
                .Bind(validatedBooking => generateBookingNumber(validatedBooking)
                    .Bind(bookingNumber => calculateFees(validatedBooking)
                        .Bind(bookingFees => createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
                            .Map(bookingAcknowledgement => new ConfirmedBooking
                            {
                                ValidatedBooking = validatedBooking,
                                BookingNumber = bookingNumber,
                                BookingAcknowledgement = bookingAcknowledgement,
                            }))));
        }
    }
}
