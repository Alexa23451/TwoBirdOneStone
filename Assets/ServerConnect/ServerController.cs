using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ServerConnect
{
    public enum RequestType
    {
        Register,
    }

    internal class URLConnection
    {
        public static readonly Dictionary<RequestType, string> urlDict = new Dictionary<RequestType, string>()
        {
            {RequestType.Register, "http://34.124.240.110:8100/auth/register"},
        };
    }

    public class RequestBase : ConnectionBase<RequestType>
    {
        public RequestBase(RequestType requestType)
        {
            this.requestType = requestType;
            urlConnection = URLConnection.urlDict[requestType];
        }
    }

    public class ConnectionBase<T> where T : Enum
    {
        private readonly string apiVersion = "0.0.1";
        private readonly string secretKey = "e43b6f51cbe811d799ba8f248616262c733846d824021aee437f048630a82e7a";
        protected T requestType;
        protected string urlConnection;

        protected string GetAPIVersion()
        {
            return apiVersion;
        }

        protected string GetURLConnection()
        {
            return urlConnection;
        }

        protected string GetSecretKey()
        {
            return secretKey;
        }

        protected string SHA256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

        protected IEnumerator SendPostCoroutine(string url, WWWForm form, string apiVersion)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
                www.SetRequestHeader("api_version", apiVersion);
                yield return www.Send();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("POST successful!");
                    StringBuilder sb = new StringBuilder();
                    foreach (KeyValuePair<string, string> dict in www.GetResponseHeaders())
                    {
                        sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                    }


                    // Print Body : result from server
                    Debug.Log(www.downloadHandler.text);
                    Debug.Log("obj server return: " + Zipper.DecompressString(www.downloadHandler.text));
                }
            }
        }
    }


    public class ServerController : MonoBehaviour
    {
        public static ServerController Instance;

        private RequestBase request;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else Destroy(this);
        }

        public T GetRequest<T>(RequestType requestType) where T : RequestBase
        {
            return (T) Activator.CreateInstance(typeof(T), requestType);
        }
    }
}