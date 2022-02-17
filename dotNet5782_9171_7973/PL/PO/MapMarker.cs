using StringUtilities;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace PO
{
    class MapMarker
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Color { get; set; }

        public string Label { get; set; }

        private static Dictionary<Type, Color> ColorsDictionary { get; set; } = new()
        {
            [typeof(Drone)] = Colors.LightBlue,
            [typeof(DroneForList)] = Colors.LightBlue,
            [typeof(Customer)] = Colors.LightPink,
            [typeof(Parcel)] = Colors.DarkSeaGreen,
            [typeof(BaseStation)] = Colors.MediumPurple,
        };

        public static MapMarker FromType(ILocalable localable)
        {
            return FromTypeAndName(localable, localable.GetType().Name.Replace("ForList", "").CamelCaseToReadable());
        }

        public static MapMarker FromTypeAndName(ILocalable localable, string name)
        {
            return new()
            {
                Longitude = (double)localable.Location.Longitude,
                Latitude = (double)localable.Location.Latitude,
                Color = ColorsDictionary[localable.GetType()].ToString(),
                Label = name,
            };
        }
    }
}
