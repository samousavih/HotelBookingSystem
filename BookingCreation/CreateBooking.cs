//
// using System;
//
// namespace HotelBookingSystem.BookingCreation
// {
//     public static class CreateBooking
//     {
//         public static Either<Problem, ConfirmedBooking> CreateBooking(BookingRequest bookingRequest)
//         {
//             var validatedBooking = ValidateBooking(bookingRequest);
//             if (validatedBooking.)
//             {
//                 return validatedBooking;
//             }
//             var bookingNumber = GenerateBookingNumber(bookingRequest);
//             var bookingFees = CalculateFees(validatedBooking);
//             var bookingAcknowledgement = CreateBookingAcknowledgement(bookingRequest, bookingNumber, bookingFees);
//             
//             return new ConfirmedBooking
//             {
//                 BookingRequest = bookingRequest,
//                 BookingNumber = bookingNumber,
//                 BookingAcknowledgement = bookingAcknowledgement,
//             };
//         }
//         private static BookingAcknowledgement CreateBookingAcknowledgement(BookingRequest bookingRequest, BookingNumber bookingNumber, BookingFees bookingFees)
//         {
//             throw new NotImplementedException();
//         }
//         private static Either<Problem,BookingNumber> GenerateBookingNumber(BookingRequest bookingRequest)
//         {
//             throw new NotImplementedException();
//         }
//
//         private static Either<Problem,BookingFees> CalculateFees(ValidatedBooking validatedBooking)
//         {
//             throw new NotImplementedException();
//         }
//
//         private static Either<Problem,ValidatedBooking> ValidateBooking(BookingRequest bookingRequest)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }
