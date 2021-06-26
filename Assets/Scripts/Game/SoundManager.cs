using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace Game
{

    public static class SoundManager
    {
        private static float playerMoveTime;
        public static void PlaySound(string sound)
        {
            var soundGameObject = new GameObject(sound);
            var audioSource = soundGameObject.AddComponent<SoundHandler>();
            if (sound == "PlayerStepSoundOne" || sound == "PlayerStepSoundTwo")
            {
                if (CanPlaySound())
                {
                    audioSource.HandleAudioClip(SoundLibrary.instance.sounds.Find(x => x.clipName == sound).audioClip);
                }
            }
            else
            {
                audioSource.HandleAudioClip(SoundLibrary.instance.sounds.Find(x => x.clipName == sound).audioClip);
            }
        }

        private static bool CanPlaySound()
        {
            var lastTimePlayed = playerMoveTime;
            var playerMoveTimerMax = 0.5f;
            if (lastTimePlayed + playerMoveTimerMax < Time.time)
            {
                playerMoveTime = Time.time;
                return true;
            }
            return false;
        }
        
        
    }
}