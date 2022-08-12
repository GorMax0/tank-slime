using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoverButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private bool _isMoveForward;

    private bool _isMove;

    public event UnityAction<bool, bool> Moved;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isMove = true;
        Moved?.Invoke(_isMove, _isMoveForward);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isMove = false;
        Moved?.Invoke(_isMove, _isMoveForward);
    }
}
