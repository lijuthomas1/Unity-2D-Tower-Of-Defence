using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int bonusCurrency = 30;
        [SerializeField] private int maxHealth = 20;
        private int currentHealth = 0;
        private void OnEnable()
        {
            currentHealth = maxHealth;
        }

        
        private void OnDisable()
        {
            LevelManager.Instance.UpdateEnemyDead();
        }

        public void SetEnemyHealth(int health)
        {
            if(health < 0) return;
            currentHealth = health;
        }

        public void DoDamage(int damage)
        {
            // print("take " + damage);
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                LevelManager.Instance.IncreaseCurrency(bonusCurrency);
                gameObject.SetActive(false);
            }
        }
    }
}
