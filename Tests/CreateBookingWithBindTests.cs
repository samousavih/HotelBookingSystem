using System;
using FluentAssertions;
using HotelBookingSystem.BookingCreation;
using HotelBookingSystem.Core;
using Xunit;

namespace HotelBookingSystem.Tests
{
    public class CreateBookingWithBindTests
    {
        [Fact]
        public void CreateBookingWithBind_WithValidRequest_ReturnsConfirmedBooking()
        {
            // Arrange
            var bookingRequest = new BookingRequest();
            Either<Problem, ValidatedBooking> ValidateBooking(BookingRequest request) => new ValidatedBooking();
            Either<Problem, BookingNumber> GenerateBookingNumber(ValidatedBooking booking) => new BookingNumber();
            Either<Problem, BookingFees> CalculateFees(ValidatedBooking booking) => new BookingFees();
            Either<Problem, BookingAcknowledgement> CreateBookingAcknowledgement(ValidatedBooking booking, BookingNumber number, BookingFees fees) => new BookingAcknowledgement();

            // Act
            var result = CreateBookingWithBind.CreateBooking(
                bookingRequest,
                ValidateBooking,
                GenerateBookingNumber,
                CalculateFees,
                CreateBookingAcknowledgement);
            
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
            Either<Problem, ValidatedBooking> ValidateBooking(BookingRequest request) => new Problem();
            Either<Problem, BookingNumber> GenerateBookingNumber(ValidatedBooking booking) => new BookingNumber();
            Either<Problem, BookingFees> CalculateFees(ValidatedBooking booking) => new BookingFees();
            Func<ValidatedBooking, BookingNumber, BookingFees, Either<Problem, BookingAcknowledgement>> createBookingAcknowledgement =
                (booking, number, fees) => new BookingAcknowledgement();

            // Act
            var result = CreateBookingWithLinq.CreateBooking(
                bookingRequest,
                ValidateBooking,
                GenerateBookingNumber,
                CalculateFees,
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
