using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        /*if (!gameObject.CompareTag("Bow")) return;*/
        var mousePosition = Input.mousePosition;
        if (Camera.main != null) mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        var direction = transform.position - mousePosition;
        direction.z = 0;
        transform.right = -direction;
    }
}