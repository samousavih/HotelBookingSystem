using System;
using CSharpFunctionalExtensions;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithLinqButWithResult
{
    public static Result<ConfirmedBooking, Problem> CreateBooking(BookingRequest bookingRequest,
        Func<BookingRequest, Result<ValidatedBooking, Problem>> validateBooking,
        Func<ValidatedBooking, Result<BookingNumber, Problem>> generateBookingNumber,
        Func<ValidatedBooking, Result<BookingFees, Problem>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Result<BookingAcknowledgement, Problem>> createBookingAcknowledgement)
    {
        return
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

public static class ResultExtensions
{
    public static Result<V, E> SelectMany<U, V, E, T>(this Result<T, E> first, Func<T, Result<U, E>> getSecond, Func<T, U, V> project)
    {
        return first.Bind(a => getSecond(a).Map(b => project(a, b)));
    }
    public static Result<T, E> Select<T,R,E>(this Result<R,E> first, Func<R, T> map) => first.Map(map);
}


