using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace WebRequester
{
    class ShowWebRequest : IRequest<WebRequest>, IResponse<WebResponse,WebRequest>, IWrite<WebResponse>
    {
        public WebRequest Requester(string link)
        {
            //create request from url
            WebRequest webRequest = WebRequest.Create(link);

            return webRequest;
            // If required by the server, set the credentials.
            //webRequest.Credentials = CredentialCache.DefaultCredentials;
        }

        public WebResponse Response(WebRequest request)
        {
                WebResponse webResponse = request.GetResponse();
                return webResponse;
        }

        public void WriteToConsole(WebResponse response)
        {
            //status
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            //use stream to read the response
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            //read response
            string responseFromServer = reader.ReadToEnd();

            //write resonse to console
            Console.WriteLine(responseFromServer);

            //Close steam
            dataStream.Close();

            //Close response
            response.Close();
        }

        
    }
}
