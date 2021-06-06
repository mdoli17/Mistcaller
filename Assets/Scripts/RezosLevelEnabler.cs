using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType {Next,Last}

public class RezosLevelEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] last;
    [SerializeField]
    private GameObject[] next;

    [SerializeField]
    private bool disableLevels;

    [SerializeField]
    private LevelType enableLevel;
    private void Start()
    {
        if (disableLevels)
        {
            foreach (var _object in next)
            {
                if (_object && _object.name != "Scene 03.1 Lodi")
                {
                    _object.SetActive(false);
                } 
                else if (_object.name == "Scene 03.1 Lodi")
                {
                    StartCoroutine(disableInTime(_object));
                }
            }
        }
    }

    IEnumerator disableInTime(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch (enableLevel)
            {
                case LevelType.Next:
                    EnableLevels(enableNextLevel: true, enableLastLevel: false);
                    break;
                case LevelType.Last:
                    EnableLevels(enableNextLevel: false, enableLastLevel: true);
                    break;
                default:
                    break;
            }
        }
    }

    private void EnableLevels(bool enableNextLevel, bool enableLastLevel)
    {
        if (next.Length != 0)
        {
            foreach (var _object in next)
            {
                _object.SetActive(enableNextLevel);
                //_object.GetComponent<LevelManager>().EnableLevel(enableNextLevel);
            }
        }

        if (last.Length != 0)
        {
            foreach (var _object in last)
            {
                _object.SetActive(enableLastLevel);
                //_object.GetComponent<LevelManager>().EnableLevel(enableLastLevel);
            }
        }
    }
}
