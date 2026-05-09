using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class draginobject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{

    public bool IsHovering { get; private set; }
    public bool IsBeingDragged { get; private set; }
    public static event Action <draginobject> OnDragchange;
    public dropzone curerentTarget;
    public dropzone previousTarget;
    Canvas canvas;
    public event Action<dropzone> OnDropZoneChanged;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }


    private void Update()
    {
        if (!IsBeingDragged) return;
        transform.position = Pointer.current.position.ReadValue();

        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        IsBeingDragged = true;
        transform.SetParent(canvas.transform);
        OnDragchange?.Invoke(this);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       IsHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovering = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsBeingDragged = false;
        OnDragchange?.Invoke(null);
        if (curerentTarget && curerentTarget.CanAcceptItem(this))
        {

        transform.SetParent(previousTarget.transform);
            previousTarget = curerentTarget;
            curerentTarget = null;
            OnDropZoneChanged?.Invoke(curerentTarget);

        } else if (previousTarget) 
        {
            transform.SetParent(previousTarget.transform);
            transform.position = previousTarget.PlaceAtNearestLocation(transform.position);


        }



    }

    internal void SetTarget(dropzone dropzone)
    {
        if (dropzone == previousTarget)
        {
            return;
        }

        previousTarget = dropzone;
    }
}
