using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Guardamos la posición y rotación inicial de este objeto
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Restaura posición, rotación y física
    public void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Si el objeto tiene Rigidbody, limpiamos la física
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep(); // fuerza a detenerlo
        }
    }
}
