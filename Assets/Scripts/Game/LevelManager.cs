using UnityEngine;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        public bool state;

        public void EnableLevel(bool enable)
        {
            state = enable;
            //Do some other stuff
            //like, maybe, disable/enable all child objects?
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    transform.GetChild(i).gameObject.SetActive(enable);
            //}
        }
    }
}