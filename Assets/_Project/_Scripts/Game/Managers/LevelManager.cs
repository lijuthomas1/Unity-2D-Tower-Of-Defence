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
        private void Awake ()
        {
            instance = this;
            currency = defaultCurrency;
        }
        public void IncreaseCurrency (int amount)
        {
            if(amount <= 0) return;
            currency += amount;
        }
        public  bool  CheckAndPurchaseTower(int amount)
        {
            if(amount <= currency)
            {
                currency -= amount;
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
