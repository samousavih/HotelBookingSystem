using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using HotelBookingSystem.BookingCreation;

namespace HotelBookingSystem.BookingCreationWithResultAsync;

public static class CreateBookingWithLinqButWithResultAsync
{
    public static async Task<Result<ConfirmedBooking, Problem>> CreateBooking(BookingRequest bookingRequest,
        Func<BookingRequest, Result<ValidatedBooking, Problem>> validateBooking,
        Func<ValidatedBooking, Task<Result<BookingNumber, Problem>>> generateBookingNumber,
        Func<ValidatedBooking, Task<Result<BookingFees, Problem>>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Result<BookingAcknowledgement, Problem>> createBookingAcknowledgement)
    {
        return await
            from validatedBooking in validateBooking(bookingRequest)
            from bookingNumber in generateBookingNumber(validatedBooking)
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