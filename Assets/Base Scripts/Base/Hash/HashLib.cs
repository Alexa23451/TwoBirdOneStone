using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

public static class HashLib
{
    private static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
    private static TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    private const string _stringEnCode = "HGY&U^^^V%$V%^*^BV%$%&*V^";

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string GetHashString(string plainText)
    {
        var data = Encoding.Default.GetBytes(plainText);
        var hash = md5.ComputeHash(data);
        return BytesToString(hash);
    }

    private static string BytesToString(byte[] bytes)
    {
        StringBuilder stringVal = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            stringVal.Append(bytes[i]);
        }

        return stringVal.ToString();
    }

    public static string GetHashStringAndDeviceID(string plainText)
    {
        return GetHashString(OptimizeComponent.GetStringOptimize(_stringEnCode, plainText));
        //return GetHashString(OptimizeComponent.GetStringOptimize(SystemInfo.deviceUniqueIdentifier, plainText));
    }

    public static string EncryptAndDeviceID(string text)
    {
        //tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(SystemInfo.deviceUniqueIdentifier));
        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_stringEnCode));
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        using (var transform = tdes.CreateEncryptor())
        {
            byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }

    public static string DecryptAndDeviceID(string cipher)
    {
        //tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(SystemInfo.deviceUniqueIdentifier));
        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_stringEnCode));
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        using (var transform = tdes.CreateDecryptor())
        {
            byte[] cipherBytes = Convert.FromBase64String(cipher);
            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return UTF8Encoding.UTF8.GetString(bytes);
        }
    }


    public static bool VerifyMd5Hash(string input, string hash)
    {
        // Hash the input.
        string hashOfInput = GetHashString(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}