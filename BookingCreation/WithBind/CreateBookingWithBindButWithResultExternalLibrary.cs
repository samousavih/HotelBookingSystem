using System;
using CSharpFunctionalExtensions;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithBindButWithResult
{
    public static Result<ConfirmedBooking, Problem> CreateBooking(BookingRequest bookingRequest,
        Func<BookingRequest, Result<ValidatedBooking, Problem>> validateBooking,
        Func<ValidatedBooking, Result<BookingNumber, Problem>> generateBookingNumber,
        Func<ValidatedBooking, Result<BookingFees, Problem>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Result<BookingAcknowledgement, Problem>> createBookingAcknowledgement)
    {
        return validateBooking(bookingRequest)
            .Bind(validatedBooking =>
            {
                return generateBookingNumber(validatedBooking)
                    .Bind(bookingNumber =>
                    {
                        return calculateFees(validatedBooking)
                            .Bind(bookingFees =>
                            {
                                return createBookingAcknowledgement(validatedBooking, bookingNumber, bookingFees)
                                    .Map(bookingAcknowledgement => new ConfirmedBooking
                                    {
                                        ValidatedBooking = validatedBooking,
                                        BookingNumber = bookingNumber,
                                        BookingAcknowledgement = bookingAcknowledgement,
                                    });
                            });
                    });
            });
    }
}