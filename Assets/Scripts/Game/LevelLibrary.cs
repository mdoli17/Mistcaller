using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "LevelLibrary", menuName = "LevelLibrary", order = 0)]
    public class LevelLibrary : ScriptableSingleton<LevelLibrary>
    {
        public List<LevelManager> levels;
    }
}