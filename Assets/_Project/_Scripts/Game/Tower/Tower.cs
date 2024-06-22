using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

namespace TowerOfDefence.Game
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private GameObject upgradeUI;
        [SerializeField]
        private Button upgradeBtn;
        [SerializeField]
        private LayerMask enemyMask;
        [SerializeField]
        private Transform tankGun;
        [SerializeField]
        private Transform bulletInitPoint;
        [SerializeField]
        private TowerUpgradeInfo[] towerUpgradeInfo;
        [SerializeField]
        private GameObject bulletPrefab = null;
        private float bulletTime = 0.0f;
        private Transform targetEnemy;
        private int towerLevelIndex = 0;

        private void OnEnable()
        {
            upgradeBtn.interactable = true;
            LevelManager.ForceReset += ResetTower;
        }
        private void OnDisable()
        {
            LevelManager.ForceReset -= ResetTower;
        }
        private void ResetTower()
        {
            gameObject.SetActive(false);
        }



        private void FixedUpdate()
        {
            if (targetEnemy != null)
                if (!targetEnemy.gameObject.activeInHierarchy || Vector3.Distance(transform.position, targetEnemy.position) > towerUpgradeInfo[towerLevelIndex].towerRange) targetEnemy = null;
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
            Handles.DrawWireDisc(transform.position, transform.forward, towerUpgradeInfo[towerLevelIndex].towerRange);
        }
        private void FindTargetEnemy()
        {
            //// print("FindTargetEnemy");
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerUpgradeInfo[towerLevelIndex].towerRange, transform.forward, towerUpgradeInfo[towerLevelIndex].towerRange, enemyMask);
            if (hits.Length > 0) targetEnemy = hits[0].transform;
            //if (targetEnemy != null) // print("target " + targetEnemy.name);
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

            if (bulletTime > towerUpgradeInfo[towerLevelIndex].bulletFireTime)
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
            bullet.GetComponent<Bullet>().SetBulletValue(towerUpgradeInfo[towerLevelIndex].bulletDamage, towerUpgradeInfo[towerLevelIndex].bulletSpeed);
        }

        private void CheckAndDisableUpgradeBtn()
        {
            // print("Level " + towerLevelIndex + " length" + towerUpgradeInfo.Length);
            upgradeBtn.interactable = !(towerLevelIndex >= towerUpgradeInfo.Length);
        }

        public void OpenUpgradeUI()
        {
            upgradeUI.SetActive(true);
        }

        public void CloseUpgradeUI()
        {
            upgradeUI.SetActive(false);
        }

        public void OnUpgradeClick()
        {
            CloseUpgradeUI();
            // print("---Level " + towerLevelIndex + " length" + towerUpgradeInfo.Length);
            if (towerLevelIndex >= towerUpgradeInfo.Length - 1) return;
            if (!LevelManager.Instance.CheckAndPurchaseTower(towerUpgradeInfo[towerLevelIndex + 1].price)) return;
            towerLevelIndex++;
            CloseUpgradeUI();
            CheckAndDisableUpgradeBtn();
            Debug.Log("towerLevelIndex" + towerLevelIndex);
        }
        public void OnCloseClick()
        {
            CloseUpgradeUI();
        }
    }
}
