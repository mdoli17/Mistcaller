using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("ObjectsToSave"), SerializeField]
    
    private GameObject[] ObjectsToSave;

    
    public static GameObject player;
    public static GameObject rock;

    [SerializeField] private List<LevelManager> levels;

    public void GenerateLevelGuids()
    {
        foreach (var level in levels) 
        {
            level.SetLevelId(GUID.Generate().ToString());
        }
    }
    public static void ResetLevel()
    {
        SaveObject save = SaveManager.shared.ReadFromFile("Autosave");
        player.transform.position = save.PlayerPosition + new Vector3(5,0,0);
        rock.transform.position = save.RockPosition;
        rock.GetComponentInChildren<RollingBall>().ResetBall();

        //Added this code
        SaveObjectGeneral saveGeneral = SaveManager.shared.ReadSOGFromFile("Autosave2");

        if (saveGeneral.levelStateList.Count != 0)
        {
            foreach (var levelState in saveGeneral.levelStateList)
            {
                levelState.level.EnableLevel(levelState.state);
            }
        }

        if (saveGeneral.objectPositionList.Count != 0)
        {
            foreach (var objectAndPosition in saveGeneral.objectPositionList)
            {
                objectAndPosition._object.transform.position = objectAndPosition.position;
            }
        }
       
    }
    
    private void Start() {
        player = FindObjectOfType<NewPlayer>().gameObject;
        rock = FindObjectOfType<RollingBall>().transform.parent.gameObject;
    }

    public static void SaveGame()
    {
        SaveObject save = new SaveObject(player.transform.position, rock.transform.position);
        SaveManager.shared.WriteToFile(save, "Autosave");


        List<LevelState> list = new List<LevelState>();

        foreach (var level in LevelLibrary.instance.levels)
        {
            list.Add(new LevelState(level, level.state));
        }

        SaveObjectGeneral levelSaveObject = new SaveObjectGeneral(list);
    }

    public static void SaveGame(List<GameObject> objectsToSave)
    {
        SaveObject save = new SaveObject(player.transform.position, rock.transform.position);
        SaveManager.shared.WriteToFile(save, "Autosave");


        List<LevelState> levelStateList = new List<LevelState>();

        foreach (var level in LevelLibrary.instance.levels)
        {
            levelStateList.Add(new LevelState(level, level.state));
        }

        List<ObjectPosition> objectPositionList = new List<ObjectPosition>();

        if (objectsToSave.Count != 0)
        {
            foreach (var _object in objectsToSave)
            {
                objectPositionList.Add(new ObjectPosition(_object, _object.transform.position));
            }
        }

        SaveObjectGeneral levelSaveObject = new SaveObjectGeneral(levelStateList, objectPositionList);
        SaveManager.shared.WriteSOGToFile(levelSaveObject, "Autosave2");
    }

}

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate GUID's"))
        {
            ((GameManager)target).GenerateLevelGuids();
        }
    }
}


