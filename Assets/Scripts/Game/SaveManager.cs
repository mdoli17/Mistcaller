using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;
using System.Threading;
using UnityEditor;
using System.Security.AccessControl;

public class SaveManager
{
    private static string savePath;
    private static readonly string username = Environment.UserDomainName;

    byte[] key = Convert.FromBase64String(Encryption.cryptoKey);
    private static SaveManager instance = new SaveManager();
    
    private readonly object pLock = new object();
    public static SaveManager shared
    {
        get { return instance; }
    }

    public SaveManager()
    {
        savePath = Application.dataPath + "/Saves/";
    }


    public void WriteToFile(SaveObject save, string filename)
    {


        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(savePath + filename, json);
    }

    public SaveObject ReadFromFile(string filename)
    {
        SaveObject save = null;
        string json = File.ReadAllText(savePath + filename);
        save = JsonUtility.FromJson<SaveObject>(json);
        return save;
    }
}

public static class Encryption
{
    public const string cryptoKey = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";
    private const int keySize = 256;
    private const int ivSize = 16;

    public static void WriteObjectToStream(string path, object obj)
    {
        if (object.ReferenceEquals(null, obj))
            return;

        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(path, json);

    }

    public static SaveObject ReadObjectFromStream(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveObject>(json);
    }


    public static CryptoStream CreateEncryptionStream(byte[] key, Stream outputStream)
    {
        byte[] iv = new byte[ivSize];

        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetNonZeroBytes(iv);
        }

        outputStream.Write(iv, 0, iv.Length);

        Rijndael rijndael = new RijndaelManaged();
        rijndael.KeySize = keySize;

        CryptoStream encryptor = new CryptoStream(
            outputStream,
            rijndael.CreateEncryptor(key, iv),
            CryptoStreamMode.Write);
        return encryptor;
    }

    public static CryptoStream CreateDecryptionStream(byte[] key, Stream inputStream)
    {
        byte[] iv = new byte[ivSize];

        if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
        {
            throw new ApplicationException("Failed to read IV from stream.");
        }

        Rijndael rijndael = new RijndaelManaged();
        rijndael.KeySize = keySize;

        CryptoStream decryptor = new CryptoStream(
            inputStream,
            rijndael.CreateDecryptor(key, iv),
            CryptoStreamMode.Read);
        return decryptor;
    }
}

[Serializable]
public class SaveObject
{
    public Vector3 PlayerPosition;
    
}
