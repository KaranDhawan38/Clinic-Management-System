using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class Encryption
    {

        public static string Encrypt(string password)
        {
            StringBuilder newPassword = new StringBuilder(password);
            bool changeFlag = true;
            int passwordLength = newPassword.Length;
            for (int i = 0; i < passwordLength; i++)
            {
                int number = (i + 1) * 10 % 3;
                if (changeFlag == true)
                {
                    int count = 0;
                    while (count != number)
                    {
                        newPassword[i]++;
                        count++;
                    }
                    changeFlag = false;
                }
                else
                {
                    int count = 0;
                    while (count != number)
                    {
                        newPassword[i]--;
                        count++;
                    }
                    changeFlag = true;
                }
            }
            return newPassword.ToString();
        }

    }
}
