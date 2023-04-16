using System;
using System.Threading.Tasks;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqAsyncAndNonMonadic
{
    public static async Task<Either<Problem, ConfirmedBooking>> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Task<Either<Problem, BookingNumber>>> generateBookingNumber,
        Func<ValidatedBooking, BookingFees> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {
        return await
            from validatedBooking in validateBooking(bookingRequest)
            from bookingNumber in generateBookingNumber(validatedBooking)
            let bookingFees = calculateFees(validatedBooking)
            from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
    }

    public static async Task<Either<Problem, ConfirmedBooking>> CreateBooking2(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Task<Either<Problem, BookingNumber>>> generateBookingNumber,
        Func<ValidatedBooking, BookingFees> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {
        return await
            from validatedBooking in validateBooking(bookingRequest).ToEitherAsync()
            from bookingNumber in generateBookingNumber(validatedBooking)
            from bookingFees in calculateFees(validatedBooking).ToEitherAsync<Problem, BookingFees>()
            from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees).ToEitherAsync()
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
    }
}
