using System;
using HotelBookingSystem.Core;

namespace HotelBookingSystem.BookingCreation;

public static class CreateBookingWithBind
{
    public static Either<Problem, ConfirmedBooking> CreateBooking(
        BookingRequest bookingRequest,
        Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking,
        Func<ValidatedBooking, Either<Problem, BookingNumber>> generateBookingNumber,
        Func<ValidatedBooking, Either<Problem, BookingFees>> calculateFees,
        Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement)
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