using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public float distanceFromPlayer = 1f; // Distance of the flashlight from the player

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.parent.position; // Assuming the player is the parent
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the flashlight
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Update the position of the flashlight to be a certain distance from the player, in the direction of the mouse
        transform.position = (Vector2)transform.parent.position + direction.normalized * distanceFromPlayer;
    }
}
