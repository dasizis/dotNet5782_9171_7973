
namespace DAL
{
    class RandomManager
    {
        public static System.Random Rand => new System.Random();

        /// <summary>
        /// creates a ranodm name in a random length (4-7 letters).
        /// </summary>
        /// <returns>the random name</returns>
        public static string RandomName()
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
        /// creates a ranodm full name
        /// </summary>
        /// <returns>the random full name</returns>
        public static string RandomFullName()
        {
            return $"{RandomName()} {RandomName()}";
        }

        /// <summary>
        /// creates a random phone number
        /// </summary>
        /// <returns>the random phone number</returns>
        public static string RandomPhone()
        {
            string phone ="05";
            const int PHONE_LENGTH = 10;

            phone += Rand.Next(
                (int)System.Math.Pow(10, PHONE_LENGTH - 3),
                (int)System.Math.Pow(10, PHONE_LENGTH - 2)
            ).ToString();

            return phone;
        }

        /// <summary>
        /// creates a random date
        /// </summary>
        /// <returns>the random date</returns>
        public static System.DateTime RandomDate()
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
        public static int RandomEnumOption(System.Type enumType)
        {
            System.Array enumValues = enumType.GetEnumValues();
            return Rand.Next(enumValues.Length);
        }
    }
}