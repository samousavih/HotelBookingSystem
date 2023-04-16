using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqButWithResultAsync
{
    public static async Task<Result<ConfirmedBooking, Problem>> CreateBooking(BookingRequest bookingRequest,
        Func<BookingRequest, Task<Result<ValidatedBooking, Problem>>> validateBooking,
        Func<ValidatedBooking, Task<Result<BookingNumber,Problem>>> generateBookingNumber,
        Func<ValidatedBooking, BookingFees> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Result<BookingAcknowledgement, Problem>> createBookingAcknowledgement)
    {
        return await
            from validatedBooking in validateBooking(bookingRequest)
            from bookingNumber in generateBookingNumber(validatedBooking)
            from bookingFees in calculateFees(validatedBooking).ToResultAsync()
            from bookingAcknowledgement in createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
            select new ConfirmedBooking
            {
                ValidatedBooking = validatedBooking,
                BookingNumber = bookingNumber,
                BookingAcknowledgement = bookingAcknowledgement,
            };
    }
}
public static class ResultExtensions
{
    public static Task<Result<T, Problem>> ToResultAsync<T>(this T value)
    {
        return Task.FromResult(Result.Success<T,Problem>(value));
    }
}