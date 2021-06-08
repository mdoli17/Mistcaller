using UnityEngine;
using Sirenix.OdinInspector;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {

        [SerializeField][ReadOnly] private string levelId;

        public bool state;

        public void EnableLevel(bool enable)
        {
            state = enable;
        }

        public void SetLevelId(string id) => levelId = id;
        public string GetLevelId() => levelId;

    }

}