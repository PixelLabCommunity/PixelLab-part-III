using UnityEngine;

public class GameObjectRotation : MonoBehaviour
{
    private const int BaseRotation = 360;
    private const int BaseValue = 0;
    [SerializeField] private float rotationSpeed = 2f;


    private void Update()
    {
        transform.Rotate(BaseValue, BaseValue, BaseRotation * rotationSpeed * Time.deltaTime);
    }
}