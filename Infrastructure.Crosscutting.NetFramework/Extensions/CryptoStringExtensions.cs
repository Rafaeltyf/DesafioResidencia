using Infrastructure.Crosscutting.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.NetFramework.Extensions
{
    public sealed class CryptoStringExtensions : IStringExtensions
    {
        public string ConvertToMD5(string value)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            StringBuilder builder = new StringBuilder();
            foreach (var item in md5Bytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }

        public string ConvertToCrypt(string S)
        {
            byte num = 0;
            int length;
            string str = "";
            if (S != null)
            {
                length = S.Length;
            }
            else
            {
                length = 0;
            }
            byte num1 = (byte)length;
            byte num2 = num1;
            byte num3 = 1;
            if (num2 >= num3)
            {
                num2++;
                do
                {
                    byte s = (byte)S[num3 - 1];
                    if (s >= 32 && s <= 126)
                    {
                        s = 0xff;
                    }
                    else
                    {
                        s = 127;
                    }
                    char chr = S[num3 - 1];
                    do
                    {
                        chr = (char)(short)((short)chr & s ^ num3);
                        num = (byte)chr;
                    }
                    while (num <= 31 || num >= 127);


                    str = string.Concat(str, chr.ToString());
                    num3++;
                }
                while (num3 != num2);
            }
            return str;
        }
    }
}
