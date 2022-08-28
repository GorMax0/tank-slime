using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoverButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool _isMoveForward;

    private bool _isMove;

    public event UnityAction<bool, bool> Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isMove = true;
        Clicked?.Invoke(_isMove, _isMoveForward);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isMove = false;
        Clicked?.Invoke(_isMove, _isMoveForward);
    }
}
