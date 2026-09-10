using System;
using UnityEngine;

namespace Assets.Script.Audio
{
    [CreateAssetMenu(fileName = "Music", menuName = "MusicWagonEvent")]
    public class WagonSpeedMusic : ScriptableObject
    {

        public event Action SpeedMusic;
        public void SpeedWagonlimit()
        {
            SpeedMusic?.Invoke();
        }
    }
}