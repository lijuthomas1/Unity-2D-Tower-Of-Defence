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
        private IEnumerator nextWaveAutoCouroutine;
        private IEnumerator currentWaveCouroutine;

        private void Start()
        {
            StartWave();
        }
        private void StartWave()
        {
            if (currentWaveIndex >= waveInfo.waveInfoList.Count) return;
            currentWaveInfo = waveInfo.waveInfoList[currentWaveIndex];
            currentWaveCouroutine = WaitForWave();
            StartCoroutine(currentWaveCouroutine);
        }

        private void StartNextWave()
        {
            StopCoroutine(nextWaveAutoCouroutine);
            if (currentWaveIndex < waveInfo.waveInfoList.Count)
            {
                currentWaveIndex++;
                StartWave();
            }
        }

        private void OnWaveEnd()
        {
            print("OnWaveEnd");

            if (nextWaveAutoCouroutine != null) StopCoroutine(nextWaveAutoCouroutine);
            if (currentWaveIndex >= waveInfo.waveInfoList.Count) return;
            nextWaveAutoCouroutine = WaitForNextWave(waveInfo.nextWaveTime);
            StartCoroutine(nextWaveAutoCouroutine);
        }

        private IEnumerator WaitForNextWave(float waitTime)
        {
            if (currentWaveIndex < waveInfo.waveInfoList.Count)
            {
                yield return new WaitForSeconds(waitTime);
                StartNextWave();
            }


        }

        private IEnumerator WaitForWave()
        {
            currentEnemyIndex = 0;
            while (currentEnemyIndex < currentWaveInfo.maxEnemyCount)
            {
                yield return new WaitForSeconds(currentWaveInfo.spawnTimeInSecond);
                CreateEnemy();
                currentEnemyIndex++;
                print("Here");
            }
            yield return null;
            OnWaveEnd();
        }

        private void CreateEnemy()
        {
            Instantiate(enemyObject, LevelManager.Instance.GetStartPoint.position, Quaternion.identity);
        }

    }
}
