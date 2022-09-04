using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShootZone : MonoBehaviour, IPointerDownHandler
{
    public event UnityAction<Vector3> Shooted;

    public void OnPointerDown(PointerEventData eventData)
    {
        Shooted?.Invoke(Vector3.forward);
    }
}
