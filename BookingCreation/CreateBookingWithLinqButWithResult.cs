using System;
using CSharpFunctionalExtensions;

namespace HotelBookingSystem.BookingCreation;
public static class CreateBookingWithLinqButWithResult
    {
        public static Result<ConfirmedBooking, Problem> CreateBooking(BookingRequest bookingRequest)
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
        private static Result<BookingAcknowledgement,Problem> CreateBookingAcknowledgement(ValidatedBooking validatedBooking, BookingNumber bookingNumber, BookingFees bookingFees)
        {
            throw new NotImplementedException();
        }
        private static Result<BookingNumber,Problem> GenerateBookingNumber(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static Result<BookingFees,Problem> CalculateFees(ValidatedBooking validatedBooking)
        {
            throw new NotImplementedException();
        }

        private static Result<ValidatedBooking,Problem> ValidateBooking(BookingRequest bookingRequest)
        {
            throw new NotImplementedException();
        }
    }

public static class ResultExtensions
{
    public static Result<V, E> SelectMany<U, V, E, T>(this Result<T, E> first, Func<T, Result<U, E>> getSecond, Func<T, U, V> project)
    {
        return first.Bind(a => getSecond(a).Map(b => project(a, b)));
    }
}


