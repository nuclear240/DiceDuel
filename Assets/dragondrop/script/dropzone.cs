using UnityEngine;

public class dropzone : MonoBehaviour
{
    [SerializeField] private RectTransform IDK;
    private draginobject ballright;
    [SerializeField] private LayerMask allowed;

    private void Awake ()
    {
        
        draginobject.OnDragchange += OnDragStateUpdate;
        OnDragStateUpdate(null);
    }

    private void Update ()
    {
       if (RectTransformUtility.RectangleContainsScreenPoint(IDK, ballright.transform.position ))
        {
            ballright.SetTarget(this);
        }
    }

    public bool CanAcceptItem (draginobject item)
    {
        return item && (allowed & (1 << item.gameObject.layer)) !=0;
    }

    private void OnDragStateUpdate (draginobject item)
    {
        ballright = item;
        enabled = CanAcceptItem (item);
    }

    public Vector2 PlaceAtNearestLocation (Vector2 position)
    {
        Vector2 origin = IDK.position;
        return new Vector2(Mathf.Clamp(position.x, IDK.rect.min.x + origin.x, IDK.rect.max.x + origin.x),
            Mathf.Clamp(position.y, IDK.rect.min.y + origin.y, IDK.rect.max.y + origin.y));
    }

    public void OnDestroy()
    {
        draginobject.OnDragchange -= OnDragStateUpdate;
    }

}