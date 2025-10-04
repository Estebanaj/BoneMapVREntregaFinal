using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Guardamos la posici�n y rotaci�n inicial de este objeto
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Restaura posici�n, rotaci�n y f�sica
    public void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Si el objeto tiene Rigidbody, limpiamos la f�sica
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep(); // fuerza a detenerlo
        }
    }
}
