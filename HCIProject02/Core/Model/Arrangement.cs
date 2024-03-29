﻿using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Arrangement : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private Hotel _hotel;
        public virtual Hotel Hotel { get => _hotel; set => OnPropertyChanged(ref _hotel, value);}

        private double _price;
        public double Price { get => _price; set => OnPropertyChanged(ref _price, value);}

        private DateTime _departureTime;
        public DateTime DepartureTime { get => _departureTime; set => OnPropertyChanged(ref _departureTime, value); }

        private DateTime _returnTime;
        public DateTime ReturnTime { get => _returnTime; set => OnPropertyChanged(ref _returnTime, value); }

        private IList<Attraction> _attractions;
        public virtual IList<Attraction> Attractions { get => _attractions; set => OnPropertyChanged(ref _attractions, value); }

        private string _imagePath;
        public string ImagePath { get => _imagePath; set => OnPropertyChanged(ref _imagePath, value); }

        private string _tripPlan;
        public string TripPlan { get => _tripPlan; set => OnPropertyChanged(ref _tripPlan, value);}

        private bool _isAvailable;  // if arrangement is not available it is not shown to the clients, it should still be shown in agent tables, do not confuse with isActive property
        public bool IsAvailable { get => _isAvailable; set => OnPropertyChanged(ref _isAvailable, value); }

        public Arrangement()
        {

        }
    }
}
