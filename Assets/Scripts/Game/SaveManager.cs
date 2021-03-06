﻿using System;
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
using Game;

public class SaveManager
{
    private static string savePath;
    private static readonly string username = Environment.UserDomainName;

    byte[] key = Convert.FromBase64String(Encryption.cryptoKey);
    private static SaveManager instance = new SaveManager();
    

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
        File.WriteAllText(savePath + filename + ".txt", json);
    }

    public void WriteSOGToFile(SaveObjectGeneral save, string filename)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(savePath + filename + ".txt", json);
    }

    public SaveObject ReadFromFile(string filename)
    {
        SaveObject save = null;
        string json = File.ReadAllText(savePath + filename + ".txt");
        save = JsonUtility.FromJson<SaveObject>(json);
        return save;
    }

    public SaveObjectGeneral ReadSOGFromFile(string filename)
    {
        SaveObjectGeneral save = null;
        string json = File.ReadAllText(savePath + filename + ".txt");
        save = JsonUtility.FromJson<SaveObjectGeneral>(json);
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
    public SaveObject()
    {

    }

    public SaveObject(Vector3 PlayerPosition, Vector3 RockPosition)
    {
        this.PlayerPosition = PlayerPosition;
        this.RockPosition = RockPosition;
    }

    public Vector3 PlayerPosition;

    public Vector3 RockPosition;
    
}

[Serializable]
public class SaveObjectGeneral
{
    public SaveObjectGeneral()
    {

    }

    public SaveObjectGeneral(List<LevelState> levelStateList, List<GameObject> objects, List<Vector3> positions)
    {
        this.levelStateList = levelStateList;

        for (int i = 0; i < objects.Count; i++)
        {
            objectPositionList.Add(new ObjectPosition(objects[i], positions[i]));
        }
    }

    public SaveObjectGeneral(List<LevelState> levelStateList, List<ObjectPosition> objectPositionList)
    {
        this.levelStateList = levelStateList;
        this.objectPositionList = objectPositionList;
    }

    public SaveObjectGeneral(List<LevelState> levelStateList)
    { 
        this.levelStateList = levelStateList;
    }

    public List<LevelState> levelStateList;

    public List<ObjectPosition> objectPositionList;
}

[Serializable]
public class LevelState
{
    public LevelState(LevelManager level, bool state)
    {
        this.level = level;
        this.state = state;
    }

    public LevelManager level;
    public bool state;
}

[Serializable]
public class ObjectPosition
{
    public ObjectPosition(GameObject _object, Vector3 position)
    {
        this._object = _object;
        this.position = position;
    }

    public GameObject _object;
    public Vector3 position;
}