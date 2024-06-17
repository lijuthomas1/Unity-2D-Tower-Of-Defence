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
        [SerializeField] private int defaultCurrency =100;
        private int currency;
        
        private static LevelManager instance;
        public static LevelManager Instance => instance;
        public List<Transform> PathPoints {  get { return pathPoint; } }
        public Transform GetStartPoint{ get { return startPoint; } }
        public int GetCurrencyValue() { return currency; }

        public static event Action<int> OnCurrencyChange;

        private bool isMouseOnUI = false;
        
        private void Awake ()
        {
            instance = this;
            currency = defaultCurrency;
            OnUpdateCurrency();
        }
        private void Start() {
            OnUpdateCurrency();
        }
        private void OnUpdateCurrency()
        {
            OnCurrencyChange?.Invoke(currency);
        }
        public void UpdateMouseOverUI(bool isOverUI)
        {
            isMouseOnUI = isOverUI;
        }

        public bool IsMouseOverUI { get {  return isMouseOnUI; } }
        public void IncreaseCurrency (int amount)
        {
            if(amount <= 0) return;
            currency += amount;
            OnUpdateCurrency();
        }
        public  bool  CheckAndPurchaseTower(int amount)
        {
            if(amount <= currency)
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
        
    }
}
