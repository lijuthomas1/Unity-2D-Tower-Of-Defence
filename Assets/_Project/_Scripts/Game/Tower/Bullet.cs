using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerOfDefence.Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 3f;
        [SerializeField]
        private LayerMask targetMask;
        [SerializeField]
        private int damageValue = 20;
        private Transform target;
        private bool isTargetAssigned = false;

        private void OnEnable()
        {
            isTargetAssigned = false;
        }
        public void SetTarget(Transform target)
        {
            this.target = target;
            isTargetAssigned = true;
        }
        public void SetBulletValue(int damageValue,int bulletSpeed)
        {
            this.damageValue = damageValue;
            this.bulletSpeed = bulletSpeed;
        }
        private void Update()
        {
            if (!isTargetAssigned) return;
            if (target == null)
                this.gameObject.SetActive(false);
            else if (!target.gameObject.activeInHierarchy) this.gameObject.SetActive(false);
            FollowTarget();
        }
        private void    FollowTarget()
        {
            if (target == null) return;
            Vector3 direction = (target.position -transform.position).normalized;
            transform.position += direction * Time.deltaTime * bulletSpeed;
        }

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("Collision Enter With "+ collision.gameObject.name);
            EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
            //Debug.Log("Collision Enter With " + collision.gameObject.name +" --"+ health);
            if (health != null)
            {
                //Debug.Log("Do Damage");
                health.DoDamage(damageValue);
            }
            gameObject.SetActive(false);
        }
        
    }
}
