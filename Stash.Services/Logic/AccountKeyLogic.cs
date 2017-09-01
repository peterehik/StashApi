using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stash.Services.RequestModels.AccountKey;

namespace Stash.Services.Logic
{
    public class AccountKeyLogic
    {
        private const string Log = "C:\\Users\\petere\\Documents\\stuff.txt";
        private const string AccountKeyUrl = "https://account-key-service.herokuapp.com/v1/account";
        private const int NumTrials = 10;

        public AccountKeyResponse GetAccountKey(AccountKeyRequest request)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonData = JsonConvert.SerializeObject(new {email = request.Email, key = request.Key});
                bool error;
                string result;
                int numTrials = 0;
                var resultException = new WebException();
                do
                {
                    try
                    {
                        result = webClient.UploadString(new Uri(AccountKeyUrl), jsonData);
                        error = false;
                    }
                    catch (WebException ex) //web api down.
                    {
                        error = true;
                        result = null;
                        resultException = ex;
                        Thread.Sleep(10*1000);
                    }
                    finally
                    {
                        numTrials++;
                    }
                } while (error && numTrials <= NumTrials);

                if (error)
                    throw resultException;

                dynamic serializedResponse = JsonConvert.DeserializeObject(result);
                var response = new AccountKeyResponse()
                {
                    AccountKey = serializedResponse.account_key,
                    Email = request.Email
                };

                return response;

            }
        }
    }
}
