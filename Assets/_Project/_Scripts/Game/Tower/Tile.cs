using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerOfDefence.Game
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private Color selectColor = Color.gray;
        private SpriteRenderer sriteRenderer;
        private Color   startColor;
        private GameObject towerObject;
        private Tower towerScript;
        private void Start()
        {
            sriteRenderer = GetComponent<SpriteRenderer>();
            startColor = sriteRenderer.color;   
        }

        private void OnMouseEnter()
        {
            sriteRenderer.color = selectColor;
        }
        private void OnMouseExit()
        {
            sriteRenderer.color = startColor;
        }

        private void OnMouseDown()
        {
            //// print(LevelManager.Instance.IsMouseOverUI);
           
            if(towerObject != null)
            {
                towerScript.OpenUpgradeUI();
                return;
            } 
                
            if (LevelManager.Instance.IsMouseOverUI) return;
            TowerInfo towerInfo = TowerManager.Instance.GetSelectedTower();
            if (towerInfo.price > LevelManager.Instance.GetCurrencyValue())
            {
                // print("Not enough currency");
                return;
            }
            
            else LevelManager.Instance.CheckAndPurchaseTower(towerInfo.price);
            towerObject = ObjectPoolManager.Instance.SpawnObjectFromPool(towerInfo.name, transform.position, Quaternion.identity);
            if (towerObject != null)
            {
                towerScript = towerObject.GetComponent<Tower>();
            }
        }


    }
}
