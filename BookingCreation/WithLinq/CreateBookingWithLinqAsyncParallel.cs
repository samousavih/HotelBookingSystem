using System;
using System.Threading.Tasks;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqAsyncParallel
{
    public static async Task<Either<Problem, ConfirmedBooking>> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Task<Either<Problem, BookingNumber>>> generateBookingNumber,
        Func<ValidatedBooking, Task<Either<Problem, BookingFees>>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {
        var task1 = validateBooking(bookingRequest).ToEitherAsync();

        var task2 = from validatedBooking in task1
                    from bookingNumber in generateBookingNumber(validatedBooking)
                    select bookingNumber;

        var task3 = from validatedBooking in task1
                    from bookingFees in calculateFees(validatedBooking)
                    select bookingFees;

        return await from validatedBooking in task1
                     from bookingNumber in task2
                     from bookingFees in task3
                     from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees).ToEitherAsync()
                     select new ConfirmedBooking
                     {
                         ValidatedBooking = validatedBooking,
                         BookingNumber = bookingNumber,
                         BookingAcknowledgement = bookingAcknowledgement,
                     };

    }
}
