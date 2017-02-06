using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Util
    {
        public static bool ElementExistsInEnumeration(Type enumType, object value)
        {
            bool exists = Enum.IsDefined(enumType, value);
            if (!exists)
            {
                throw new ArgumentException("Niste uneli adekvatan parametar. Mora pripadati " + enumType +
                                            " enumeraciji!");
            }
            return exists;
        }
    
        public static bool CheckDate(int day, int month, int year)
        {
            if (day <= 0)
            {
                Console.WriteLine("Dan moze biti najmanje 1.\n");
                return false;
            }
            if (month <= 0 || month > 12)
            {
                Console.WriteLine("Mesec mora biti broj izmedju 1 i 12.\n");
                return false;
            }
            if (year < 0)
            {
                Console.WriteLine("Godina mora biti veca od 0.\n");
                return false;
            }

            bool isLeap = year % 4 == 0 && year % 100 != 0 || year % 400 == 0;

            if (month == 2 && isLeap && day > 29)
            {
                Console.WriteLine("U prestupnoj godini februar ima najvise 29 dana.\n");
                return false;
            }
            else if (month == 2 && !isLeap && day > 29)
            {
                Console.WriteLine("Kada nije prestupna godina februar ima najvise 28 dana.\n");
                return false;
            }
            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) &&
                     day > 31)
            {
                Console.WriteLine(month + ". mesec moze imati maksimalno 31 dan.\n");
                return false;
            }
            else if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)
            {
                Console.WriteLine(month + ". mesec moze imati najvise 30 dana.\n");
                return false;
            }
            return true;
        }
  }
}
