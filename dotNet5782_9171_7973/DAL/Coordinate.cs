﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Coordinate
    {
        public static Random Random = new Random();
        public double Degrees { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public CoordinatesPosition Position { get; set; }

        public Coordinate() { }

        
        public Coordinate CoordinateCast(double value, CoordinatesPosition position = default)
        {

            //sanity
            if (value < 0 && position == CoordinatesPosition.N)
                position = CoordinatesPosition.S;
            //sanity
            if (value < 0 && position == CoordinatesPosition.E)
                position = CoordinatesPosition.W;
            //sanity
            if (value > 0 && position == CoordinatesPosition.S)
                position = CoordinatesPosition.N;
            //sanity
            if (value > 0 && position == CoordinatesPosition.W)
                position = CoordinatesPosition.E;

            var decimalValue = Convert.ToDecimal(value);

            decimalValue = Math.Abs(decimalValue);

            var degrees = Decimal.Truncate(decimalValue);
            decimalValue = (decimalValue - degrees) * 60;

            var minutes = Decimal.Truncate(decimalValue);
            var seconds = (decimalValue - minutes) * 60;

            Degrees = Convert.ToDouble(degrees);
            Minutes = Convert.ToDouble(minutes);
            Seconds = Convert.ToDouble(seconds);
            Position = position;

            return this;
        }
        public Coordinate(double degrees, double minutes, double seconds, CoordinatesPosition position)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Position = position;
        }
        public double ToDouble()
        {
            var result = (Degrees) + (Minutes) / 60 + (Seconds) / 3600;
            return Position == CoordinatesPosition.W || Position == CoordinatesPosition.S ? -result : result;
        }
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Position;
        }
    }

    public enum CoordinatesPosition
    {
        N, E, S, W
    }
}