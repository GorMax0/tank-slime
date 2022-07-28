using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Dreamteck.Splines;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Spline.Direction _direction;

    private bool _isClick;

    public event UnityAction<bool, Spline.Direction> PointerDown;
    public event UnityAction<bool, Spline.Direction> PointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClick = true;
        PointerDown?.Invoke(_isClick, _direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClick = false;
        PointerUp?.Invoke(_isClick , _direction);
    }
}
