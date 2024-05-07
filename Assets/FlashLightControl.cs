using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public float distanceFromPlayer = 1f; 

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.parent.position; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = (Vector2)transform.parent.position + direction.normalized * distanceFromPlayer;
    }
}
