using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int currentHealth = 0;
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(int damage)
    {
        print("take "+damage);
        currentHealth -= damage;
        if (currentHealth < 0) gameObject.SetActive(false);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("Collision Enter");
    //}
}
