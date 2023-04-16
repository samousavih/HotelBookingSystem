using System;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinq
{
    public static Either<Problem, ConfirmedBooking> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Either<Problem, BookingNumber>> generateBookingNumber,
        Func<ValidatedBooking, Either<Problem, BookingFees>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {
        return
            from validatedBooking in validateBooking(bookingRequest)
            from bookingNumber in generateBookingNumber(validatedBooking)
            from bookingFees in calculateFees(validatedBooking)
            from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
    }
}