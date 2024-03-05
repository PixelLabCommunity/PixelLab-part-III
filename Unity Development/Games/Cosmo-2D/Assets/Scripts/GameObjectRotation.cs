using UnityEngine;

public class GameObjectRotation : MonoBehaviour
{
    private const int BaseRotation = 360;
    [SerializeField] private float rotationSpeed = 2f;


    private void Update()
    {
        transform.Rotate(0, 0, BaseRotation * rotationSpeed * Time.deltaTime);
    }
}