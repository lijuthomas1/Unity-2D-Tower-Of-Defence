using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Level
{
    [CreateAssetMenu]
    public class EnemySpawnInfo : ScriptableObject
    {
       public List<WaveInfo> waveInfoList = new List<WaveInfo>();
    }

    [System.Serializable]
    public struct WaveInfo
    {
        public float spawnTimeInSecond;
        public int maxEnemyCount;
    }
}
