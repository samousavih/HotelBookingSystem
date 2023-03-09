using System;
using HotelBookingSystem.BookingCreation;

namespace HotelBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
           var result = CreateBookingWithLinq.CreateBooking(new BookingRequest());
        }
    }
}
