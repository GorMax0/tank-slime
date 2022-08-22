using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShootZone : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private HitPoint[] _hitPoints;

    private int _index = 0;

    public event UnityAction<Vector3> Shooted;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 targetPosition = _hitPoints[_index].transform.position;
        targetPosition.x = 0f;
        Debug.Log(targetPosition);
        Shooted?.Invoke(targetPosition);

        _index++;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    Physics.Raycast(ray, out hit);

    //    if (hit.collider != null)
    //    {
    //        Vector3 targetPosition = hit.point;
    //        targetPosition.x = 0f;
    //        Debug.Log(targetPosition);
    //        Shooted?.Invoke(targetPosition);
    //    }
    //}
}
