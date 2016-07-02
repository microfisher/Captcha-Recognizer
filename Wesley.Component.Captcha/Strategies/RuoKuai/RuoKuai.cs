using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Wesley.Component.Captcha.Strategies.RuoKuai
{
    public class RuoKuai
    {
        #region Post With Pic
        /// <summary>
        /// HTTP POST方式请求数据(带图片)
        /// </summary>
        /// <param name="url">URL</param>        
        /// <param name="param">POST的数据</param>
        /// <param name="fileByte">图片Byte</param>
        /// <returns></returns>
        public static string Post(string url, IDictionary<object, object> param, byte[] fileByte)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.UserAgent = "RK_C# 1.2";
            wr.Method = "POST";

            //wr.Timeout = 150000;
            //wr.KeepAlive = true;

            //wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Stream rs = null;
            rs = wr.GetRequestStream();
            string responseStr = null;

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in param.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, param[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "image", "i.gif", "image/gif");//image/jpeg
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            rs.Write(fileByte, 0, fileByte.Length);

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            wresp = wr.GetResponse();

            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            responseStr = reader2.ReadToEnd();
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
            }
            wr.Abort();
            wr = null;
            return responseStr;
        }
        #endregion

        #region Post
        public static string Post(string url, Dictionary<object, object> param)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "RK_C# 1.1";
            //request.Timeout = 30000;

            #region POST方法

            //如果需要POST数据  
            if (!(param == null || param.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in param.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, param[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, param[key]);
                    }
                    i++;
                }

                byte[] data = System.Text.Encoding.UTF8.GetBytes(buffer.ToString());
                try
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                catch
                {
                    return "无法连接.请检查网络.";
                }

            }

            #endregion

            WebResponse response = null;
            string responseStr = string.Empty;
            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;

        }
        #endregion
    }
}
