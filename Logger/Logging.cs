using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logging
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
               );
       
        public static void loggException(string excp)
        {
            log.Fatal(excp);
        }

        public static void loggInfo(string info)
        {
            string date = DateTime.Now.Date.ToString();
            string time = DateTime.Now.TimeOfDay.ToString();
            log.Info(info);
        }

        public static void loggError(string err)
        {
            log.Error(err);
        }
    }
}
