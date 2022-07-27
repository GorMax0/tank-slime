using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button _button;

    private void OnEnable()
    {
        
    }
}
