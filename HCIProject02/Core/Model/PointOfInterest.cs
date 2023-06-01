using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public abstract class PointOfInterest : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private string? _description;
        public string? Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private double? _latitude;
        public double? Latitude { get => _latitude; set => OnPropertyChanged(ref _latitude, value);}

        private double? _longitude;
        public double? Longitude { get => _longitude; set => OnPropertyChanged(ref _longitude, value); }

        private string? _address;
        public string? Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private string? _imagePath;
        public string? ImagePath { get => _imagePath; set => OnPropertyChanged(ref _imagePath, value); }

        public PointOfInterest() { }
        protected PointOfInterest(PointOfInterest other) : base(other)
        {
            Name = other.Name;
            Description = other.Description;
            Latitude = other.Latitude;
            Longitude = other.Longitude;
            Address = other.Address;
            ImagePath = other.ImagePath;
        }

        public override string ToString()
        {
            return $"Name ({Name})\n" +
                $"Description ({Description})\n" +
                $"Latitude ({Latitude})\n" +
                $"Longitude ({Longitude})\n" +
                $"Address ({Address})\n" +
                $"ImagePath ({ImagePath})\n";
        }
    }
}
