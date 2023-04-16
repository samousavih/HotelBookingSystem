using System;
using System.Threading.Tasks;
using LanguageExt;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqAsyncWithEitherFromLibraryWithTypeConverion
{
    public static async Task<Either<Problem, ConfirmedBooking>> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Task<Either<Problem, ValidatedBooking>>> validateBooking,
        Func<ValidatedBooking, Task<Either<Problem, BookingNumber>>> generateBookingNumber,
        Func<ValidatedBooking, BookingFees> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
    {
        return await
            from validatedBooking in validateBooking(bookingRequest).ToAsync()
            from bookingNumber in generateBookingNumber(validatedBooking).ToAsync()
            let bookingFees = calculateFees(validatedBooking)
            from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees).AsTask().ToAsync()
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
    }
}