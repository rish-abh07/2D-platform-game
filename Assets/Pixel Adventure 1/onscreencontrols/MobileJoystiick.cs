using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystiick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    private RectTransform joyStick;
    [SerializeField] private int dragMovementDistance = 30;
    [SerializeField] private int dragoffSentDistance = 100;
    public event Action<Vector2> Onmove;
    private float dragThreshhold = 0.6f;

    public int OnMove { get; internal set; }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joyStick, eventData.position, null, out offset);
        offset = Vector2.ClampMagnitude(offset, dragoffSentDistance)/dragoffSentDistance;
        joyStick.anchoredPosition = offset  * dragMovementDistance;
        Debug.Log(offset);
        Vector2 inputVector = CalculateMovementinput(offset);
        Onmove?.Invoke(inputVector);
    }

    private Vector2 CalculateMovementinput(Vector2 offset)
    {
        float x = Math.Abs(offset.x) > dragThreshhold ? offset.x : 0;
        float y = Math.Abs(offset.y) > dragThreshhold ? offset.y : 0;
        return new Vector2(x, y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStick.anchoredPosition = Vector2.zero;
        
    }
    public void Awake()
    {
        joyStick = (RectTransform)transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
}
}
