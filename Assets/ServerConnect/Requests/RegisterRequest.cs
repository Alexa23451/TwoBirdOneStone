using UnityEngine;

namespace ServerConnect.Requests
{
    public class RegisterRequest : RequestBase
    {
        public RegisterRequest(RequestType requestType) : base(requestType)
        {
        }

        public void PostAccountToServer(string userName, string passWord)
        {
            string sign = SHA256(userName + SHA256(passWord) + GetAPIVersion() +
                                 GetSecretKey());

            WWWForm form = new WWWForm();

            form.AddField("username", userName);
            form.AddField("password", SHA256(passWord));
            form.AddField("sign", sign);
            ServerController.Instance.StartCoroutine(SendPostCoroutine(GetURLConnection(), form,
                GetAPIVersion()));
        }
    }
}