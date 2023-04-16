using System;
using System.Threading.Tasks;
using LanguageExt;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqAsyncWithEitherFromLibrary
{
    public static async Task<Either<Problem, ConfirmedBooking>> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Task<Either<Problem, ValidatedBooking>>> validateBooking,
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
}
public static class TaskExtension
{
    public static async Task<Either<L, R2>> Select<R2, L, R>(this Task<Either<L, R>> first, Func<R, R2> map) => (await first).Map(map);
    public static async Task<Either<L, R3>> SelectMany<R2, R3, L, R>(this Task<Either<L, R>> first, Func<R, Task<Either<L, R2>>> getSecond, Func<R, R2, R3> project)
    {
        return await first.BindAsync(async a => (await getSecond(a)).Map(b => project(a, b)));
    }

    public static async Task<Either<L, R3>> SelectMany<R2, R3, L, R>(this Either<L, R> first, Func<R, Task<Either<L, R2>>> getSecond, Func<R, R2, R3> project)
    {
        return await first.BindAsync(async a => (await getSecond(a)).Map(b => project(a, b)));
    }

    public static async Task<Either<L, R3>> SelectMany<R2, R3, L, R>(this Task<Either<L, R>> first, Func<R, Either<L, R2>> getSecond, Func<R, R2, R3> project)
    {
        return (await first).Bind(a => getSecond(a).Map(b => project(a, b)));
    }
}