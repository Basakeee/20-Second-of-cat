using System;
using BaseZlipacket.Managers;
using BaseZlipacket.Tools;
using UnityEngine;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private AudioClip levelMusic;
        
        private void Start()
        {
            if (levelMusic == null)
            {
                MusicManager.Instance.StopAllMusic();
                return;
            }
            
            if (!MusicManager.Instance.CheckIsSongPlaying(levelMusic.name))
            {
                MusicManager.Instance.StopAllMusic();
                MusicManager.Instance.PlayMusicWithCallback(levelMusic, levelMusic.name, transform);
            }
        }
    }
}