using System;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithSelectMany
{
    public static Either<Problem, ConfirmedBooking> CreateBooking(BookingRequest bookingRequest)
    {
        
        return ValidateBooking(bookingRequest)
            .SelectMany(validatedBooking => GenerateBookingNumber(validatedBooking),
                (validatedBooking, bookingNumber) => new { validatedBooking, bookingNumber })
            .SelectMany(context => CalculateFees(context.validatedBooking),
                (context, bookingFees) => new { context.validatedBooking, context.bookingNumber, bookingFees })
            .SelectMany(context => CreateBookingAcknowledgement(context.validatedBooking, context.bookingNumber, context.bookingFees),
                (context, bookingAcknowledgement) => new ConfirmedBooking
                {
                    ValidatedBooking = context.validatedBooking,
                    BookingNumber = context.bookingNumber,
                    BookingAcknowledgement = bookingAcknowledgement,
                });
    }
    private static Either<Problem,BookingAcknowledgement> CreateBookingAcknowledgement(ValidatedBooking validatedBooking, BookingNumber bookingNumber, BookingFees bookingFees)
    {
        return new BookingAcknowledgement();
    }
    private static Either<Problem,BookingNumber> GenerateBookingNumber(ValidatedBooking validatedBooking)
    {
        return new BookingNumber();
    }

    private static Either<Problem,BookingFees> CalculateFees(ValidatedBooking validatedBooking)
    {
        return new BookingFees();
    }

    private static Either<Problem,ValidatedBooking> ValidateBooking(BookingRequest bookingRequest)
    {
        return new ValidatedBooking();
    }
}