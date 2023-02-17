using System;
using System.Threading.Tasks;

namespace HotelBookingSystem.BookingCreation
{
    public static class CreateBookingWithLinq
    {
        public static Either<Problem, ConfirmedBooking> CreateBooking(BookingRequest bookingRequest)
        {
            return 
                from validatedBooking in ValidateBooking(bookingRequest)
                from bookingNumber in GenerateBookingNumber(bookingRequest)
                from bookingFees in CalculateFees(validatedBooking)
                from bookingAcknowledgement in Either<Problem,BookingAcknowledgement>.From(CreateBookingAcknowledgement(bookingRequest, bookingNumber, bookingFees))
                select new ConfirmedBooking
                {
                    BookingRequest = bookingRequest,
                    BookingNumber = bookingNumber,
                    BookingAcknowledgement = bookingAcknowledgement,
                };
        }
        private static BookingAcknowledgement CreateBookingAcknowledgement(BookingRequest bookingRequest, BookingNumber bookingNumber, BookingFees bookingFees)
        {
            throw new NotImplementedException();
        }
        private static async Task<Either<Problem,BookingNumber>> GenerateBookingNumber(BookingRequest bookingRequest)
        {
            await Task.Delay(1);
            return new BookingNumber();
        }

        private static async Task<Either<Problem,BookingFees>> CalculateFees(ValidatedBooking validatedBooking)
        {
            await Task.Delay(1);
            return new BookingFees();
        }

        private static Either<Problem,ValidatedBooking> ValidateBooking(BookingRequest bookingRequest)
        {
            throw new NotImplementedException();
        }
    }

}
