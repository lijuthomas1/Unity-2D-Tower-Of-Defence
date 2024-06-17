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
        private GameObject tower;
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
            if(tower != null) return;
            TowerInfo towerInfo = TowerManager.Instance.GetSelectedTower();
            if (towerInfo.price > LevelManager.Instance.GetCurrencyValue())
            {
                print("Not enough currency");
                return;
            }
            else LevelManager.Instance.CheckAndPurchaseTower(towerInfo.price);
            tower = Instantiate(towerInfo.prefab, transform.position, Quaternion.identity) as GameObject;
        }


    }
}
