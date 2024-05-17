using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DraggableImage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //[SerializeField] private Canvas canvas;
    private Image img;
    private RectTransform rectTransform;
    public Transform parentAfterDrag;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(string.Format("Beginning to drag {0}", gameObject.name));
        parentAfterDrag = transform.parent;
        img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(string.Format("Stopped dragging {0}", gameObject.name));
        transform.SetParent(parentAfterDrag);
        img.raycastTarget = true;
    }
}
