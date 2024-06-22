using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private List<Transform> pathPoint;
        [SerializeField] private int defaultCurrency = 100;
        [SerializeField] private int maxLevelHealth = 7;
        private int currentLevelHealth = 7;
        private int currency;

        private static LevelManager instance;
        public static LevelManager Instance => instance;
        public List<Transform> PathPoints { get { return pathPoint; } }
        public Transform GetStartPoint { get { return startPoint; } }
        public int GetCurrencyValue() { return currency; }

        public static event Action<int> OnCurrencyChange;
        public static event Action OnEnemyDead;
        public static event Action<GameState> OnGameStateChange;
        public static event Action ForceReset;

        private bool isMouseOnUI = false;

        private GameState gameState = GameState.None;

        private void Awake()
        {
            print("Level Manager Awake");
            if(instance != null)
            {
                print("Level Manager Instance Not Null");
            }
            instance = this;
            ResetCurrency();
        }
        
        private void ResetCurrency()
        {
            currency = defaultCurrency;
            OnUpdateCurrency();
        }
        private void Start()
        {
            OnUpdateCurrency();
            ResetLevelHealth();
        }

        private void ResetLevelHealth()
        {
            currentLevelHealth = maxLevelHealth;
        }
        private void OnUpdateCurrency()
        {
            OnCurrencyChange?.Invoke(currency);
        }
        public void UpdateMouseOverUI(bool isOverUI)
        {
            isMouseOnUI = isOverUI;
        }

        public bool IsMouseOverUI { get { return isMouseOnUI; } }
        public void IncreaseCurrency(int amount)
        {
            if (amount <= 0) return;
            currency += amount;
            OnUpdateCurrency();
        }
        public bool CheckAndPurchaseTower(int amount)
        {
            if (amount <= currency)
            {
                currency -= amount;
                OnUpdateCurrency();
                return true;
            }
            else
            {
                Debug.Log("In sufficient balance!");
                return false;
            }

        }

        public void UpdateEnemyDead()
        {
            OnEnemyDead?.Invoke();
        }

        private void GameOver()
        {
            gameState = GameState.GameOver;
            OnGameStateChange?.Invoke(gameState);
            ForceReset?.Invoke();
        }

        public void OnEnemyReachEndPoint()
        {
            currentLevelHealth--;
            if (currentLevelHealth <= 0)
            {
                currentLevelHealth = 0;
                print("Game over");
                GameOver();
            }
            else print("Enemy Reach End Point");

        }

        public void GameStartRequest()
        {
            ResetCurrency();
            ResetLevelHealth();
            gameState = GameState.GameStarted;
            OnGameStateChange?.Invoke(gameState);            
        }

        public void GameOverRequest()
        {
            GameOver();
        }
    }
}
