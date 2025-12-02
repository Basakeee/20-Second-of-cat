using System;
using UnityEngine;
using Zlipacket.Managers;
using Zlipacket.Tools;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private AudioSource levelMusic;
        
        private void Start()
        {
            if (!MusicManager.Instance.CheckIsSongPlaying(levelMusic.name))
            {
                MusicManager.Instance.StopAllMusic();
                MusicManager.Instance.PlayMusicWithCallback(levelMusic.clip, levelMusic.name, transform);
            }
        }
    }
}