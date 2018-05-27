using Newtonsoft.Json;
using System;
using System.Net;

namespace Strafe {
    public class JSONResponse {
        public dynamic JSON;
        public HttpStatusCode HTTPStatus;

        internal JSONResponse() { }

        public static JSONResponse Get(string url, int attempt = 0) {
            HttpWebRequest webRequest = (HttpWebRequest) HttpWebRequest.Create(url);
            webRequest.UserAgent = Properties.Settings.Default.useragent.Replace("{version}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            try {
                HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
                return new JSONResponse() {
                    JSON = JsonConvert.DeserializeObject(new System.IO.StreamReader(webResponse.GetResponseStream()).ReadToEnd()),
                    HTTPStatus = webResponse.StatusCode
                };

            } catch (WebException webexc) {
                // timeout error; slow down and retry
                if (((HttpWebResponse) webexc.Response).StatusCode == (HttpStatusCode) 429) {
                    StrafeForm.Log("TVMaze responded with 429 - sleeping 2000 ms");
                    System.Threading.Thread.Sleep(2000);
                    if (attempt > 5) return new JSONResponse() { JSON = null, HTTPStatus = (HttpStatusCode) 429 };
                    return JSONResponse.Get(url, attempt + 1);
                }

                return new JSONResponse() { JSON = null, HTTPStatus = ((HttpWebResponse) webexc.Response).StatusCode };

            } catch (Exception exc) {
                StrafeForm.Log("Unknown exception in JSONResponse: " + exc.Message);
                return new JSONResponse() { JSON = null, HTTPStatus = HttpStatusCode.Ambiguous };
            }
        }
    }
}
