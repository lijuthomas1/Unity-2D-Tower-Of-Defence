using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using TowerOfDefence.Game;
using UnityEngine;

public class ShopMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;
    private void Start()
    {   
        LevelManager.OnCurrencyChange += OnCurrencyChange;
    }
    private void OnDestroy()
    {
        LevelManager.OnCurrencyChange -= OnCurrencyChange;
    }

    private void OnCurrencyChange(int value)
    {
        currencyText.text = value.ToString();
    }

}
