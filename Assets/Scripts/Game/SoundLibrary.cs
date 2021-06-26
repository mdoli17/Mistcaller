using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(order = 0, fileName = "SoundLibrary", menuName = "SoundLibrary")]
    public class SoundLibrary : ScriptableSingleton<SoundLibrary>
    {
        public List<SoundClass> sounds;
    }
    
    [System.Serializable]
    public class SoundClass
    {
        public string clipName;
        public AudioClip audioClip;
    }

    public struct SoundNames
    {
        public struct Player
        {
            public const string JumpSound = "PlayerJumpSound";
            public const string StepSoundOne = "PlayerStepSoundOne";
            public const string StepSoundTwo = "PlayerStepSoundTwo";
        }

        public struct Environment
        {
            public const string ButtonPressSound = "ButtonPressSound";
            public const string PlaygroundSound = "PlaygroundSound";
            public const string RopeSound = "RopeSound";
            
            public const string BranchStepSound = "BranchStepSound";
            public const string BranchFallSound = "BranchFallSound";
            
            public const string MovableBoxSound = "MovableBoxSound";
            
            public const string DrumLowSound = "DrumLowSound";
            public const string DrumMidSound = "DrumMidSound";
            public const string DrumHighSound = "DrumHighSound";

            
            public const string SpikeFallSound = "SpikeFallSound";
            
            public const string PotIdleSound = "PotIdleSound";
            public const string PotSplashSound = "PotSplashSound";

            public const string OwlSound = "OwlSound";
            
            public const string DoorOpenSound = "DoorOpenSound";
            public const string DoorCloseSound = "DoorCloseSound";

            public const string TotemSound = "TotemSound";
            
            public const string WheelSound = "WheelSound";

            public const string SafeTurnSound = "SafeTurnSound";

            public const string FishBiteSound = "FishBiteSound";
            public const string FishPullUpFromWaterSound = "FishFromWaterSound";
            public const string FishGateFallSound = "FishGateFallSound";
            

            public const string BerryPickOne = "BerryPickOne";
            public const string BerryPickTwo = "BerryPickTwo";
            public const string BerryPickThree = "BerryPickThree";

            public const string SpikeBallImpact = "SpikeBallImpactSound";

            public const string ElevatorBackgroundMusic = "ElevatorBackgroundMusic";
            public const string ElevatorMoveSound = "ElevatorMoveSound";
        }
        
        

        public struct Game
        {
            public const string LevelBackgroundSound = "LevelBackground";
            public const string CaveBackgroundSound = "CaveBackgroundSound";
            public const string SpikeDeathSound = "SpikeTouchSound";
        }
    }
}