using System;
using System.Collections;
using System.Collections.Generic;
using TowerOfDefence.Level;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnInfo waveInfo;
        [SerializeField]
        private GameObject enemyObject;
        private WaveInfo currentWaveInfo; 
        private int currentWaveIndex = 0;
        private int currentEnemyIndex = 0;

        private void Start()
        {
            StartWave();
        }
        private void StartWave()
        {
            currentWaveInfo = waveInfo.waveInfoList[currentWaveIndex];
            StartCoroutine(WaitForWave());
        }

        private IEnumerator WaitForWave()
        {
            while (currentEnemyIndex < currentWaveInfo.maxEnemyCount)
            {
                yield return new WaitForSeconds(currentWaveInfo.spawnTimeInSecond);
                CreateEnemy();
                currentEnemyIndex++;
                print("Here");
            }
            yield return null;
        }

        private void CreateEnemy()
        {
            Instantiate(enemyObject, LevelManager.Instance.GetStartPoint.position, Quaternion.identity);
        } 
        
    }
}
