using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class PasswordManager
    {
        /// <summary>
        /// 用于暴力破解的字符数组
        /// </summary>
        public char[] PasswordChars { get; private set; }

        /// <summary>
        /// 用于记录密码破解进度的主键值
        /// </summary>
        public long PasswordIndex { get; private set; }

        /// <summary>
        /// 密码生成控制器构造函数
        /// </summary>
        /// <param name="passwordChars">用于暴力破解的字符数组</param>
        public PasswordManager(char[] passwordChars)
        {
            PasswordChars = passwordChars;
        }

        /// <summary>
        /// 密码生成控制器构造函数
        /// </summary>
        /// <param name="passwordChars">用于暴力破解的字符数组</param>
        /// <param name="password">起始密码</param>
        public PasswordManager(char[] passwordChars, string password) : this(passwordChars)
        {
            PasswordIndex = CalculatePasswordIndex(PasswordChars, password);
        }

        /// <summary>
        /// 密码生成控制器构造函数
        /// </summary>
        /// <param name="passwordChars">用于暴力破解的字符数组</param>
        /// <param name="passwordIndex">密码破解进度的主键值</param>
        public PasswordManager(char[] passwordChars, long passwordIndex) : this(passwordChars)
        {
            PasswordIndex = passwordIndex;
        }

        /// <summary>
        /// 密码生成控制器构造函数
        /// </summary>
        /// <param name="passwordCharList">用于暴力破解的字符数组串</param>
        public PasswordManager(string passwordCharList)
        {
            PasswordChars = passwordCharList.ToCharArray();
        }

        /// <summary>
        /// 密码生成控制器构造函数
        /// </summary>
        /// <param name="passwordCharList">用于暴力破解的字符数组串</param>
        /// <param name="password">起始密码</param>
        public PasswordManager(string passwordCharList, string password) : this(passwordCharList)
        {
            PasswordIndex = CalculatePasswordIndex(PasswordChars, password);
        }

        /// <summary>
        /// 密码生成控制器构造函数
        /// </summary>
        /// <param name="passwordCharList">用于暴力破解的字符数组串</param>
        /// <param name="passwordIndex">密码破解进度的主键值</param>
        public PasswordManager(string passwordCharList, long passwordIndex) : this(passwordCharList)
        {
            PasswordIndex = passwordIndex;
        }

        /// <summary>
        /// 将密码转换为对应字符组的主键值
        /// </summary>
        /// <param name="passwordChars">用于暴力破解的字符数组</param>
        /// <param name="password">需要转换的密码</param>
        /// <returns>密码对应的主键值</returns>
        public static long CalculatePasswordIndex(char[] passwordChars, string password)
        {
            
            var index = 0L;
            for (var i = 0; i < password.Length; ++i)
            {
                var charIndex = -1;
                for (var j = 0; j < passwordChars.Length; ++j)
                {
                    if (passwordChars[j] == password[i])
                    {
                        charIndex = j;
                        break;
                    }
                }
                if (charIndex < 0)
                {
                    throw new ArgumentException();
                }

                index += ++charIndex * Math.Pow(passwordChars.Length, password.Length - i - 1).ToLongWithEx();
            }
            return index;
        }


        private string CalculatePassword(long index)
        {
            var password = string.Empty;
            while (true)
            {
                password = PasswordChars[(index % PasswordChars.Length).ToInt()] + password;
                if (index >= PasswordChars.Length)
                {
                    index /= PasswordChars.Length;
                    --index;
                }
                else break;
            }

            return password;
        }

        public string ReadPassword()
        {
            var index = 0L;
            lock (this)
            {
                //先获取当前再自增 防止 index:0 拿不到
                index = PasswordIndex++;
            }

            return CalculatePassword(index);
        }

    }
}
