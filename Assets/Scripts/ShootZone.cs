using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShootZone : MonoBehaviour, IPointerDownHandler
{
    public event UnityAction<Vector3> Shooted;

    public void OnPointerDown(PointerEventData eventData)
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            Vector3 targetPosition = hit.point;
            Debug.Log(targetPosition);
            Shooted?.Invoke(targetPosition);
        }        
    }
}
