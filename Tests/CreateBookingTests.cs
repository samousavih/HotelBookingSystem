using System;
using FluentAssertions;
using HotelBookingSystem.BookingCreation;
using Xunit;

namespace HotelBookingSystem.Tests
{
    public class CreateBookingTests
    {
        [Fact]
        public void CreateBooking_WithValidRequest_ReturnsConfirmedBooking()
        {
            // Arrange
            var bookingRequest = new BookingRequest();
            Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking =
                request => new ValidatedBooking();
            Func<ValidatedBooking, Either<Problem, BookingNumber>> generateBookingNumber =
                booking => new BookingNumber();
            Func<ValidatedBooking, Either<Problem, BookingFees>> calculateFees =
                booking => new BookingFees();
            Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement =
                (booking, number, fees) => new BookingAcknowledgement();

            // Act
            var result = CreateBookingWithLinq.CreateBooking(
                bookingRequest,
                validateBooking,
                generateBookingNumber,
                calculateFees,
                createBookingAcknowledgement);
            
            var rightCaseExecuted = false;
            // Assert
            result.Switch(
                p => rightCaseExecuted = false,
                i => rightCaseExecuted = true
                );
            rightCaseExecuted.Should().BeTrue();
        }

        [Fact]
        public void CreateBooking_WithInvalidRequest_ReturnsProblem()
        {
            // Arrange
            var bookingRequest = new BookingRequest();
            Func<BookingRequest, Either<Problem, ValidatedBooking>> validateBooking =
                request => new Problem();
            Func<ValidatedBooking, Either<Problem, BookingNumber>> generateBookingNumber =
                booking => new BookingNumber();
            Func<ValidatedBooking, Either<Problem, BookingFees>> calculateFees =
                booking => new BookingFees();
            Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement =
                (booking, number, fees) => new BookingAcknowledgement();

            // Act
            var result = CreateBookingWithLinq.CreateBooking(
                bookingRequest,
                validateBooking,
                generateBookingNumber,
                calculateFees,
                createBookingAcknowledgement);
            var leftCaseExecuted = false;
            // Assert
            result.Switch(
                p => leftCaseExecuted = true,
                i => leftCaseExecuted = false
                );
            leftCaseExecuted.Should().BeTrue();
        }
    }
}
