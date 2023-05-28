﻿using HCIProject02.Core.Exceptions;
using HCIProject02.Core.Model;
using HCIProject02.Core.Repository.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public Booking BookArrangement(Arrangement arrangement, Client passenger)
        {
            List<Booking> bookings = _bookingRepository.GetBookingsForPassenger(passenger);
            if(bookings.FirstOrDefault(booking => booking.Arrangement.Id == arrangement.Id) == null)
            {
                Booking booking = new Booking { Arrangement = arrangement, Passenger = passenger, Price = arrangement.Price};
                return _bookingRepository.Create(booking);
            }
            throw new ArrangementAlreadyBookedException();
            
        }
    }
}
