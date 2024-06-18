using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TowerOfDefence.Game
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int bonusCurrency = 30;
        [SerializeField] private int maxHealth = 20;
        [SerializeField] private Slider healthSlider;
        private int currentHealth = 0;
        private void OnEnable()
        {
            currentHealth = maxHealth;
            UpdateHealthUI();
        }

        
        private void OnDisable()
        {
            LevelManager.Instance.UpdateEnemyDead();
        }

        public void SetEnemyHealth(int health)
        {
            if(health < 0) return;
            maxHealth = health;
            currentHealth = health;
            UpdateHealthUI();
        }

        private void UpdateHealthUI()
        {
            var healthRatio = (float)currentHealth / (float)maxHealth;
            //print("health " + healthRatio);
            healthSlider.value = healthRatio;
        }

        public void DoDamage(int damage)
        {
            // print("take " + damage);
            currentHealth -= damage;
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                LevelManager.Instance.IncreaseCurrency(bonusCurrency);
                gameObject.SetActive(false);
            }
        }
    }
}
