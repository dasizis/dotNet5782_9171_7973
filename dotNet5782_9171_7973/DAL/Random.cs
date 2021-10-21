
namespace DAL
{
    class RandomManager
    {
        public static System.Random Rand => new System.Random();

        public static string RandomName()
        {
            const int MIN_NAME_LENGTH = 4;
            const int MAX_NAME_LENGTH = 7;

            int length = Rand.Next(MIN_NAME_LENGTH, MAX_NAME_LENGTH);

            string name = ((char)Rand.Next('A', 'Z' + 1)).ToString();

            for (int i = 0; i < length - 1; i++)
            {
                name += (char)Rand.Next('a', 'z' + 1);
            }

            return name;
        }

        public static string RandomFullName()
        {
            return $"{RandomName()} {RandomName()}";
        }

        public static string RandomPhone()
        {
            /*const int LENGTH = 10;

            string phone = "";

            for (int i = 0; i < LENGTH; i++)
            {
                phone += (char)Rand.Next();
            }

            return phone;*/

            string phone ="05";

            phone += Rand.Next(1000000, 10000000 ).ToString();

            return phone;
        }

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
        
        public static int RandomEnumOption(System.Type enumType)
        {
            System.Array enumValues = enumType.GetEnumValues();
            return (int)enumValues.GetValue(Rand.Next(enumValues.Length));
        }
        

    }
}