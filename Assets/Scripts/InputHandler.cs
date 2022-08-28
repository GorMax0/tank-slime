using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private MoverButton _buttonForward;
    [SerializeField] private MoverButton _buttonBackward;

    public event UnityAction<bool, Vector3, Vector3> Moved;

    private void OnEnable()
    {
        _buttonForward.Clicked += OnClicked;
        _buttonBackward.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _buttonForward.Clicked -= OnClicked;
        _buttonBackward.Clicked -= OnClicked;
    }
    
    private void OnClicked(bool canMove, bool isMoveForward)
    {
        Vector3 moveDirection = isMoveForward ? Vector3.forward : -Vector3.forward;
        Vector3 rotationDirection = isMoveForward ? -Vector3.left : Vector3.left;
        
        Moved?.Invoke(canMove, moveDirection, rotationDirection);
    }
}
