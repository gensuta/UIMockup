using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class SnappableArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        DraggableImage draggedObj = droppedObj.GetComponent<DraggableImage>();
        draggedObj.parentAfterDrag = transform;
    }

 
}
