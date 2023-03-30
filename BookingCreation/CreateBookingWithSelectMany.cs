using System;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithSelectMany
{
    public static Either<Problem, ConfirmedBooking> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Either<Problem, BookingNumber>> generateBookingNumber,
        Func<ValidatedBooking, Either<Problem, BookingFees>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {

        return validateBooking(bookingRequest)
            .SelectMany(validatedBooking => generateBookingNumber(validatedBooking),
                (validatedBooking, bookingNumber) => new { validatedBooking, bookingNumber })
            .SelectMany(context => calculateFees(context.validatedBooking),
                (context, bookingFees) => new { context.validatedBooking, context.bookingNumber, bookingFees })
            .SelectMany(context => createBookingAcknowledgement(context.validatedBooking, context.bookingNumber, context.bookingFees),
                (context, bookingAcknowledgement) => new ConfirmedBooking
                {
                    ValidatedBooking = context.validatedBooking,
                    BookingNumber = context.bookingNumber,
                    BookingAcknowledgement = bookingAcknowledgement,
                });
    }
}