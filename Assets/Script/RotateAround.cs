using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        
    }
}

