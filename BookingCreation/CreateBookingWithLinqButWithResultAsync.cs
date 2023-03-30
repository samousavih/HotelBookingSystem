using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqButWithResultAsync
{
    public static async Task<Result<ConfirmedBooking, Problem>> CreateBooking(BookingRequest bookingRequest,
        Func<BookingRequest, Result<ValidatedBooking, Problem>> validateBooking,
        Func<ValidatedBooking, Task<BookingNumber>> generateBookingNumber,
        Func<ValidatedBooking, Task<Result<BookingFees, Problem>>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Result<BookingAcknowledgement, Problem>> createBookingAcknowledgement)
    {
        return await
            from validatedBooking in validateBooking(bookingRequest)
            from bookingNumber in generateBookingNumber(validatedBooking).ToResultAsync()
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
public static class ResultExtensions
{
    public static async Task<Result<T,Problem>> ToResultAsync<T>(this Task<T> task)
    {
        return Result.Success<T,Problem>(await task);
    }
    
    public static Task<Result<T, Problem>> ToResultAsync<T>(this T value)
    {
        return Task.FromResult(Result.Success<T,Problem>(value));
    }
}