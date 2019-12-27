using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography.Core;

namespace DuoAuthenticator.Model
{
    public class OneTimePasswordGenerator
    {
        private readonly static int[] DigitsPower = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000 };
        private readonly Int32 _codeDigits = 6;

        public String GetNext(String secret, UInt64 counter)
        {
            // Original code of Duo developers or obfuscation mechanisms.

            var numArray = Encoding.UTF8.GetBytes(secret);
            byte[] numArray1 = new byte[8];
            long num2 = (long)counter;
            for (int i = (int)numArray1.Length - 1; i >= 0; i--)
            {
                numArray1[i] = (byte)(num2 & (long)255);
                num2 >>= 8;
            }

            CryptographicHash cryptographicHash = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1).CreateHash(numArray.AsBuffer());
            cryptographicHash.Append(numArray1.AsBuffer());
            byte[] numArray2 = cryptographicHash.GetValueAndReset().ToArray();

            int num3 = numArray2[(int)numArray2.Length - 1] & 15;
            int num4 = (numArray2[num3] & 127) << 24 | (numArray2[num3 + 1] & 255) << 16 | (numArray2[num3 + 2] & 255) << 8 | numArray2[num3 + 3] & 255;
            int digitsPower = num4 % DigitsPower[this._codeDigits];
            string str1 = digitsPower.ToString();
            while (str1.Length < this._codeDigits)
            {
                str1 = string.Concat("0", str1);
            }
            return str1;
        }
    }
}
