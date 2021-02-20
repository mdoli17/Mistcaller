using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("ObjectsToSave"), SerializeField]
    
    private GameObject[] ObjectsToSave;

    private static GameManager instance = new GameManager();
   
    
    public static GameManager shared
    {
        get { return shared; }
    }

    public void ResetLevel()
    {
        SaveObject save = SaveManager.shared.ReadFromFile("CheckpointSave.txt");
        
    }
    
}


