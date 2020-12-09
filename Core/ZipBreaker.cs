using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ZipBreaker
    {
        private PasswordManager pm { get; set; }

        public string FilePath { get; private set; }

        public long MaxPasswordIndex { get; private set; }


        private ZipBreaker(string filePath)
        {
            FilePath = filePath;
        }

        public ZipBreaker(string filePath, string passwordCharList) : this(filePath)
        {
            pm = new PasswordManager(passwordCharList);
        }

        public ZipBreaker(string filePath, string passwordCharList, string startPassword, string endPassword = null) : this(filePath)
        {
            pm = new PasswordManager(passwordCharList, startPassword);
            if (string.IsNullOrEmpty(endPassword))
            {
                MaxPasswordIndex = -1L;
            }
            else
            {
                MaxPasswordIndex = PasswordManager.CalculatePasswordIndex(passwordCharList.ToArray(), endPassword);
            }
        }

    }
}
