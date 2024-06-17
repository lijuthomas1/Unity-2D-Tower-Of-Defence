using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using TowerOfDefence.Game;
using UnityEngine;

public class ShopMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;
    int UILayer;

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");    
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

    private void OnMouseEnter()
    {
        print("Mouse Enter on UI");
    }


    private void Update()
    {
        print(IsPointerOverUIElement() ? "Over UI" : "Not over UI");
        LevelManager.Instance.UpdateMouseOverUI(IsPointerOverUIElement());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

}
