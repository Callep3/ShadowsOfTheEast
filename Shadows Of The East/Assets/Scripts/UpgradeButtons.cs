using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButtons : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public GameObject info;

    public void OnPointerEnter(PointerEventData eventData)
    {
        info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info.SetActive(false);
    }
}
