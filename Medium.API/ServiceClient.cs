using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Medium.API.Common;
using Medium.API.Models;
using Newtonsoft.Json;

namespace Medium.API
{
    public class ServiceClient
    {
        private string serverAddress = "medium.com";

        public List<string> PostHistory;
        public int MaxPostHistory = 250;

        public List<string> IgnorePosts = null;
        public string NextTop100Posts = null;

        public ServiceClient(bool debug)
        {
            PostHistory = IsolatedStorageHelper.GetObject<List<string>>("PostHistory");

            if (PostHistory == null)
                PostHistory = new List<string>();

            IgnorePosts = new List<string>();

            if (debug == true)
                serverAddress = "medium-com-l0hj23njywzl.runscope.net";
        }

        public async Task GetTop100Posts(Action<List<Value>> callback)
        {
            NextTop100Posts = "{\"count\": 10}";

            GetNextTop100Posts(callback);
        }

        public async Task GetNextTop100Posts(Action<List<Value>> callback)
        {
            HttpWebRequest request = HttpWebRequest.Create("https://" + serverAddress + "/top-100/" + DateTime.Now.AddMonths(-1).ToString("MMMM-yyyy").ToLower() + "/load-more?format=json") as HttpWebRequest;
            request.Accept = "application/json";

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["X-XSRF-Token"] = "1";

            Stream requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false);
            StreamWriter sw = new StreamWriter(requestStream);

            sw.Write(NextTop100Posts);
            sw.Close();
            
            var response = await request.GetResponseAsync().ConfigureAwait(false);

            Stream stream = response.GetResponseStream();
            stream.Seek(16, SeekOrigin.Begin); // needed to bypass json anti-hijack header
            UTF8Encoding encoding = new UTF8Encoding();
            StreamReader sr = new StreamReader(stream, encoding);
            
            JsonTextReader tr = new JsonTextReader(sr);
            Top100Response top100Response = new JsonSerializer().Deserialize<Top100Response>(tr);

            List<Value> data = top100Response.payload.value;

            tr.Close();
            sr.Dispose();

            stream.Dispose();

            foreach (Value post in data)
            {
                post.homeCollection = top100Response.payload.references.Collection[post.homeCollectionId];
                post.creator = top100Response.payload.references.User[post.creatorId];
            }

            foreach (Value post in data)
            {
                IgnorePosts.Add(post.id);
            }

            NextTop100Posts = "{\"count\": 10, \"ignore\": [";
            
            foreach (string id in IgnorePosts)
            {
                NextTop100Posts += "\"" + id + "\", ";
            }

            NextTop100Posts = NextTop100Posts.Substring(0, NextTop100Posts.Length - 2);
            NextTop100Posts += "]}";

            callback(data);
        }

        public void MarkPostAsRead(string postId)
        {
            while (PostHistory.Count >= MaxPostHistory)
            {
                PostHistory.RemoveAt(MaxPostHistory - 1);
            }

            PostHistory.Insert(0, postId);
        }

        public void SaveData()
        {
            IsolatedStorageHelper.SaveObject<List<string>>("PostHistory", PostHistory);
        }
    }
}
