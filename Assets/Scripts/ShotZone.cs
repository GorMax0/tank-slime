using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShotZone : MonoBehaviour, IPointerDownHandler
{
    public event UnityAction<Vector3> Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            Vector3 targetPosition = hit.point;
            Debug.Log(targetPosition);
            Clicked?.Invoke(targetPosition);
        }
        
    }
}
