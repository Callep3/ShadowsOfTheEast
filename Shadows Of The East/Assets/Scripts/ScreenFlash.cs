using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    private Image image;
    
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void TakeDamage()
    {
        image.color = new Color(255, 0, 0, 0.4f);
        image.DOColor(new Color(255, 0, 0, 0), 0.2f);
    }
}
