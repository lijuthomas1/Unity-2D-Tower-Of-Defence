using System;
using System.Collections;
using TowerOfDefence.Level;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnInfo waveInfo;
        [SerializeField]
        private string enemyObjectTag;
        private WaveInfo currentWaveInfo;
        private int currentWaveIndex = 0;
        private int currentEnemyIndex = 0;
        private IEnumerator nextWaveAutoCouroutine;
        private IEnumerator currentWaveCouroutine;
        

        private float remainingEnemies = 0;
        private void Start()
        {            
            LevelManager.OnEnemyDead += EnemyDead;
            LevelManager.OnGameStateChange += OnGameStateChange;
        }
        private void OnDestroy()
        {
            LevelManager.OnEnemyDead -= EnemyDead;
            LevelManager.OnGameStateChange -= OnGameStateChange;
        }



        private void OnGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.GameStarted:
                    ResetGame();
                    StartWave();
                    break;
                case GameState.GameOver:
                    if (nextWaveAutoCouroutine != null) StopCoroutine(nextWaveAutoCouroutine);
                    break;
            }
        }

        private void ResetGame()
        {
            currentWaveIndex = 0;
        }
        private void StartWave()
        {
            if (currentWaveIndex >= waveInfo.waveInfoList.Count) return;
            currentWaveInfo = waveInfo.waveInfoList[currentWaveIndex];
            remainingEnemies += currentWaveInfo.maxEnemyCount;
            currentWaveCouroutine = WaitForWave();
            StartCoroutine(currentWaveCouroutine);
        }

        private void StartNextWave()
        {
            if(nextWaveAutoCouroutine !=null) StopCoroutine(nextWaveAutoCouroutine);
            print("currentWaveIndex "+ currentWaveIndex +" Max wave " + waveInfo.waveInfoList.Count);
            if (currentWaveIndex < waveInfo.waveInfoList.Count-1)
            {
                currentWaveIndex++;
                StartWave();
            }
            else
            {
                print("Level Over");
                LevelManager.Instance.GameOverRequest();
            }
        }

        private void OnWaveEnd()
        {
            if (nextWaveAutoCouroutine != null) StopCoroutine(nextWaveAutoCouroutine);
            if (currentWaveIndex >= waveInfo.waveInfoList.Count) return;
            float nextWaveTime = (currentWaveInfo.maxEnemyCount * currentWaveInfo.spawnTimeInSecond) + waveInfo.nextWaveTime;
            nextWaveAutoCouroutine = WaitForNextWave(nextWaveTime);
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
                
                CreateEnemy();
                currentEnemyIndex++;
                yield return new WaitForSeconds(currentWaveInfo.spawnTimeInSecond);
                // print("Here");
            }
            yield return null;
            OnWaveEnd();
        }

        private void CreateEnemy()
        {
            GameObject enemy = ObjectPoolManager.Instance.SpawnObjectFromPool(enemyObjectTag, LevelManager.Instance.GetStartPoint.position, Quaternion.identity);
            if(enemy == null) return;
            enemy.GetComponent<EnemyHealth>().SetEnemyHealth(currentWaveInfo.enemyHealth);
        }

        private void EnemyDead()
        {
            // print("EnemyDead");
            remainingEnemies--;
            if(remainingEnemies == 0) {
                StartNextWave();
            }
        }

    }
}
