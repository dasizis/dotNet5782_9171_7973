namespace BL
 {
    static class Validation
    {
        static bool IsValidPhone(string phone)
        {
            foreach (char ch in phone)
            {
              if (ch == '-') continue;
              if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }
        static bool IsValidLongitude(int long)
        {
          return long >= -180 && long <= 180;
        }
        static bool IsValidLatitude(int lat)
        {
          return lat >= -90 && lat <= 90;
        }
        static bool IsValidName(string name)
        {
            if (name.Split(' ') != 2) return false;
            
            foreach (char ch in name)
            {
              if (ch == ' ') continue;
              if (!ch.IsLetter()) return false;
            }
            return true;
        }
         
    }
 }
