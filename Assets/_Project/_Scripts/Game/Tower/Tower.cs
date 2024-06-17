using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using static UnityEngine.GraphicsBuffer;

namespace TowerOfDefence.Game
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private GameObject upgradeUI;
        [SerializeField]
        private float towerRange = 2f;
        [SerializeField]
        private LayerMask enemyMask;
        [SerializeField]
        private Transform tankGun;
        [SerializeField]
        private Transform bulletInitPoint;
        [SerializeField]
        private float bulletFireTime = 2.0f;
        [SerializeField]
        private int bulletDamage = 20;
        [SerializeField]
        private int bulletSpeed = 20;
        [SerializeField]
        private GameObject bulletPrefab = null;
        private float bulletTime = 0.0f;
        private Transform targetEnemy;
        private void FixedUpdate()
        {
            if (targetEnemy != null)
                if (!targetEnemy.gameObject.activeInHierarchy || Vector3.Distance(transform.position, targetEnemy.position) > towerRange) targetEnemy = null;
           

            if (targetEnemy == null) FindTargetEnemy();
        }
        private void Update()
        {
            LookAtEnemy();
            CheckBulletFire();
        }
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.forward, towerRange);
        }
        private void FindTargetEnemy()
        {
            //print("FindTargetEnemy");
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerRange, transform.forward, towerRange, enemyMask);
            if (hits.Length > 0) targetEnemy = hits[0].transform;
            //if (targetEnemy != null) print("target " + targetEnemy.name);
        }

        private void LookAtEnemy()
        {
            if (targetEnemy == null) return;
            Vector3 look = tankGun.transform.InverseTransformPoint(targetEnemy.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            tankGun.transform.Rotate(0, 0, angle);
        }

        private void CheckBulletFire()
        {
            bulletTime += Time.deltaTime;
            if (targetEnemy == null) return;

            if (bulletTime > bulletFireTime)
            {
                DoFire();
                bulletTime = 0;
            }
        }

        private void DoFire()
        {
            if (targetEnemy == null) return;
            GameObject bullet = Instantiate(bulletPrefab, bulletInitPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetTarget(targetEnemy);
            bullet.GetComponent<Bullet>().SetBulletValue(bulletDamage, bulletSpeed);
        }

        public void OpenUpgradeUI()
        {
            upgradeUI.SetActive(true);
        }

        public void CloseUpgradeUI()
        {
            upgradeUI.SetActive(true);
        }
    }
}
