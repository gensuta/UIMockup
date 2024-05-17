using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InvestigatableUIObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public MonoTweener highlightTween;
    private bool isHighlighted;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnHover()
    {
        StartHighlighting();
    }

    public void OnHoverExit()
    {
        StopHighlighting();
    }

    public void StartHighlighting()
    {
        highlightTween.Play();
    }

    public void StopHighlighting()
    {
        highlightTween.Kill();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }
}
