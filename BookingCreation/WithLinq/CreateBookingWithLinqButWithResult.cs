using System;
using CSharpFunctionalExtensions;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqButWithResult
{
    public static Result<ConfirmedBooking, Problem> CreateBooking(BookingRequest bookingRequest,
        Func<BookingRequest, Result<ValidatedBooking, Problem>> validateBooking,
        Func<ValidatedBooking, Result<BookingNumber, Problem>> generateBookingNumber,
        Func<ValidatedBooking, Result<BookingFees, Problem>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Result<BookingAcknowledgement, Problem>> createBookingAcknowledgement)
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


