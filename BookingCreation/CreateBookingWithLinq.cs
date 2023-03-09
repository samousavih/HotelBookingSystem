using System;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinq
{
    public static Either<Problem, ConfirmedBooking> CreateBooking(BookingRequest bookingRequest)
    {
        return 
            from validatedBooking in ValidateBooking(bookingRequest)
            from bookingNumber in GenerateBookingNumber(validatedBooking)
            from bookingFees in CalculateFees(validatedBooking)
            from bookingAcknowledgement in CreateBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
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