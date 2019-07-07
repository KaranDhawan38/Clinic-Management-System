using Rest;
using RestSharp;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ApteanClinic.BusinessLayer
{
    public static class Verify
    {
        public static int GetOTP(string toNumber)
        {
            try
            {
                int otpValue = new Random().Next(100000, 999999);
                var client = new RestSharp.RestClient("https://www.fast2sms.com/dev/bulk");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("authorization", "hCNAdyIp2EUQOeqo5vVWjzG1PstwJ37fmrDKR4lTHY9Li68bgZEW82iCwTjB9kYgHV4PGx36tybJOUsL");
                request.AddParameter("sender_id", "FSTSMS");
                request.AddParameter("language", "english");
                request.AddParameter("route", "qt");
                request.AddParameter("numbers", toNumber);
                request.AddParameter("message", "11222");
                request.AddParameter("variables", "{#BB#}");
                request.AddParameter("variables_values", otpValue.ToString());

                IRestResponse response = client.Execute(request);
                return otpValue;
            }
            catch (Exception e)
            {
                Database.ExceptionHandler.PrintException(e, new System.Diagnostics.StackTrace(true));
                throw e;
            }
        }

    }
}