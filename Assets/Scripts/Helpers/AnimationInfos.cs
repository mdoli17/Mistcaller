using UnityEngine;

namespace Helpers
{
    public static class AnimationInfos
    {
        private static int Convert(string name) => Animator.StringToHash(name);
        public struct RotatingDisk
        {
            public static int LeftAnimationTrigger => Convert("OnLeftClick");
            public static int RightAnimationTrigger => Convert("OnRightClick");
            public static int IncorrectAnimationTrigger => Convert("OnIncorrectGuess");
            
        }

        public readonly struct Trigger
        {
            public static int ClickAnimationTrigger => Convert("OnTriggerClick");
        }

        public readonly struct Door
        {
            public static int OpenAnimationTrigger => Convert("OpenDoor");
            public static int CloseAnimationTrigger => Convert("CloseDoor");
        }

        public readonly struct Spike
        {
            public static int SpikeFallTrigger => Convert("SpikeFall");
        }
    }
}