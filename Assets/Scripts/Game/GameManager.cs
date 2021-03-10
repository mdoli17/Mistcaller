using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("ObjectsToSave"), SerializeField]
    
    private GameObject[] ObjectsToSave;

    
    public static GameObject player;
    public static GameObject rock;


    public static void ResetLevel()
    {
        SaveObject save = SaveManager.shared.ReadFromFile("Autosave");
        player.transform.position = save.PlayerPosition + new Vector3(5,0,0);
        rock.transform.position = save.RockPosition;
        rock.GetComponentInChildren<RollingBall>().ResetBall();
    }
    
    private void Start() {
        player = FindObjectOfType<Player>().gameObject;
        rock = FindObjectOfType<RollingBall>().transform.parent.gameObject;
        Debug.Log(rock == null);
    }

    public static void SaveGame()
    {
        SaveObject save = new SaveObject(player.transform.position, rock.transform.position);
        SaveManager.shared.WriteToFile(save, "Autosave");
    }
}


