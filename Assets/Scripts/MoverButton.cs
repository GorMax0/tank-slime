using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Dreamteck.Splines;

public class MoverButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Spline.Direction _direction;

    private bool _isClick;

    public event UnityAction<bool, Spline.Direction> Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClick = true;
        Clicked?.Invoke(_isClick, _direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClick = false;
        Clicked?.Invoke(_isClick , _direction);
    }
}
