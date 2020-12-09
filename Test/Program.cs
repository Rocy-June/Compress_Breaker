using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static List<char> PasswordChar = "0123456789abcdef".ToCharArray().ToList();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mode select:");
                Console.WriteLine("1. PasswordToIndex");
                Console.WriteLine("2. IndexToPassword");
                switch (ToLong(Console.ReadLine()))
                {
                    case 1L:
                        PasswordToIndex();
                        break;
                    case 2L:
                        IndexToPassword();
                        break;
                    default:
                        break;
                }
            }
        }

        static void PasswordToIndex()
        {
            while (true)
            {
                Console.Write("Insert a password and convert to index or insert \"break\" to exit: ");
                var value = Console.ReadLine();
                if (value == "break")
                {
                    return;
                }

                Console.WriteLine("Password Index: " + CalculatePasswordIndex(value));
            }
        }

        static void IndexToPassword()
        {
            while (true)
            {
                Console.Write("Insert an index and convert to password or insert \"break\" to exit: ");
                var value = Console.ReadLine();
                if (value == "break")
                {
                    return;
                }

                Console.WriteLine("Password: " + CalculatePassword(ToLong(value)));
            }
        }

        private static long CalculatePasswordIndex(string password)
        {
            var index = 0L;
            for (var i = 0; i < password.Length; ++i)
            {
                var charIndex = PasswordChar.FindIndex(e => e == password[i]);
                if (charIndex < 0)
                {
                    throw new ArgumentException();
                }

                index += ToLong(++charIndex * Math.Pow(PasswordChar.Count, password.Length - i - 1));
            }
            return index;
        }

        private static string CalculatePassword(long index)
        {
            var password = string.Empty;
            while (true)
            {
                password = PasswordChar[ToInt(index % PasswordChar.Count)] + password;
                if (index >= PasswordChar.Count)
                {
                    index /= PasswordChar.Count;
                    --index;
                }
                else break;
            }

            return password;
        }

        private static int ToInt(object obj)
        {
            try { return Convert.ToInt32(obj); } catch { return 0; }
        }

        private static long ToLong(object obj)
        {
            try { return Convert.ToInt64(obj); } catch { return 0; }
        }
    }
}
