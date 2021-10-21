//using System;

//namespace ConsuleUI
//{
//    partial class Program
//    {
//        public static Random Rand => new Random();

//        public static string RandomName()
//        {
//            const int MIN_NAME_LENGTH = 4;
//            const int MAX_NAME_LENGTH = 7;

//            int length = Rand.Next(MIN_NAME_LENGTH, MAX_NAME_LENGTH);

//            string name = ((char)Rand.Next('A', 'Z' + 1)).ToString();

//            for (int i = 0; i < length - 1; i++)
//            {
//                name += (char)Rand.Next('a', 'z' + 1);
//            }

//            return name;
//        }

//        public static string RandomFullName()
//        {
//            return $"{RandomName()} {RandomName()}";
//        }
//    }
//}