using System;
using System.Threading.Tasks;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqAsync
{
    public static async Task<Either<Problem, ConfirmedBooking>> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Task<Either<Problem, BookingNumber>>> generateBookingNumber,
        Func<ValidatedBooking, Task<Either<Problem, BookingFees>>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {
        return await 
            from validatedBooking in validateBooking(bookingRequest).ToEitherAsync()
            from bookingNumber in generateBookingNumber(validatedBooking)
            from bookingFees in calculateFees(validatedBooking)
            from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees).ToEitherAsync()
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
    }
}
