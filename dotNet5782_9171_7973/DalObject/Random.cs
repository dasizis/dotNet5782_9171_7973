using DO;
using System.Collections.Generic;

namespace Dal
{
    static class RandomManager
    {
        internal static System.Random Rand => new();

        /// <summary>
        /// Returns a double random number in a given range
        /// </summary>
        /// <param name="start">The range start</param>
        /// <param name="end">The range end</param>
        /// <returns>A double random number between start to end</returns>
        internal static double RandomDouble(double start, double end)
        {
            return Rand.NextDouble() * (end - start) + start;
        }

        /// <summary>
        /// Randomize a longitude in Israel area
        /// </summary>
        /// <returns>A random longitude in Israel area</returns>
        internal static double RandomLongitude()
        {
            const double ISRAEL_LONG_START = 34.2;
            const double ISRAEL_LONG_END = 42.8;

            return RandomDouble(ISRAEL_LONG_START, ISRAEL_LONG_END);
        }

        /// <summary>
        /// Randomize a latitude in Israel area
        /// </summary>
        /// <returns>A random latitude in Israel area</returns>
        internal static double RandomLatitude()
        {
            const double ISRAEL_LAT_START = 33.5;
            const double ISRAEL_LAT_END = 36.15;

            return RandomDouble(ISRAEL_LAT_START, ISRAEL_LAT_END);
        }

        /// <summary>
        /// Creates a ranodm name in a random length (4-7 letters).
        /// </summary>
        /// <returns>the random name</returns>
        internal static string RandomName()
        {
            const int MIN_NAME_LENGTH = 4;
            const int MAX_NAME_LENGTH = 8;

            int length = Rand.Next(MIN_NAME_LENGTH, MAX_NAME_LENGTH);

            string name = ((char)Rand.Next('A', 'Z' + 1)).ToString();

            for (int i = 0; i < length - 1; i++)
            {
                name += (char)Rand.Next('a', 'z' + 1);
            }

            return name;
        }

        /// <summary>
        /// Creates a ranodm full name
        /// </summary>
        /// <returns>the random full name</returns>
        internal static string RandomFullName()
        {
            return $"{RandomName()} {RandomName()}";
        }

        /// <summary>
        /// Creates a random phone number
        /// </summary>
        /// <returns>the random phone number</returns>
        internal static string RandomPhone()
        {
            string phone = "05";
            const int PHONE_LENGTH = 10;

            phone += Rand.Next(
                (int)System.Math.Pow(10, PHONE_LENGTH - 3),
                (int)System.Math.Pow(10, PHONE_LENGTH - 2)
            ).ToString();

            return phone;
        }

        /// <summary>
        /// Creates a random date
        /// </summary>
        /// <returns>the random date</returns>
        internal static System.DateTime RandomDate()
        {
            int year = Rand.Next(1950, 2021);
            int month = Rand.Next(1, 12);
            int day = Rand.Next(1, 28);
            int hour = Rand.Next(23);
            int minute = Rand.Next(59);
            int second = Rand.Next(59);

            return new System.DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// choose a random enum value
        /// </summary>
        /// <param name="enumType">the enum type</param>
        /// <returns>the random value (int value)</returns>
        internal static int RandomEnumOption(System.Type enumType)
        {
            System.Array enumValues = enumType.GetEnumValues();
            return Rand.Next(enumValues.Length);
        }

        /// <summary>
        /// Creates a random Parcel instance
        /// </summary>
        /// <param name="id">the instance id</param>
        /// <returns>the created Parcel instance</returns>
        internal static Parcel RandomParcel(int id, List<Customer> customers)
        {
            int random = Rand.Next();
            return new Parcel()
            {
                Id = id,
                Requested = RandomDate(),
                Weight = (WeightCategory)RandomEnumOption(typeof(WeightCategory)),
                Priority = (Priority)RandomEnumOption(typeof(Priority)),
                SenderId = customers[random % customers.Count].Id,
                TargetId = customers[(random + 7) % customers.Count].Id,
            };
        }

        /// <summary>
        /// Creates a random Drone instance
        /// </summary>
        /// <param name="id">the instance id</param>
        /// <returns>the created Drone instance</returns>
        internal static Drone RandomDrone(int id)
        {
            return new Drone()
            {
                Id = id,
                Model = RandomName(),
                MaxWeight = (WeightCategory)RandomEnumOption(typeof(WeightCategory)),
            };
        }

        /// <summary>
        /// Creates a random Customer instance
        /// </summary>
        /// <param name="id">the instance id</param>
        /// <returns>the created Customer instance</returns>
        internal static Customer RandomCustomer(int id)
        {
            return new Customer()
            {
                Id = id,
                Name = RandomFullName(),
                Longitude = RandomLongitude(),
                Latitude = RandomLatitude(),
                Phone = RandomPhone(),
            };
        }

        /// <summary>
        /// Creates a random BaseStation instance
        /// </summary>
        /// <param name="id">the instance id</param>
        /// <returns>the created BaseStation instance</returns>
        internal static BaseStation RandomBaseStation(int id)
        {
            return new BaseStation()
            {
                Id = id,
                Name = RandomName(),
                Longitude = RandomLongitude(),
                Latitude = RandomLatitude(),
                ChargeSlots = Rand.Next(1, 10),
            };
        }
    }
}