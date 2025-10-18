using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 initialWorldPosition;
    private Quaternion initialWorldRotation;
    private bool hasInitialTransform = false;

    void OnEnable()
    {
        // Si el objeto se activa por primera vez, guarda su posición global
        if (!hasInitialTransform)
        {
            initialWorldPosition = transform.position;
            initialWorldRotation = transform.rotation;
            hasInitialTransform = true;

            // Debug.Log($"{name} guardó su posición inicial en OnEnable.");
        }
    }

    public void ResetTransform()
    {
        if (!hasInitialTransform)
        {
            Debug.LogWarning($"{name} no tiene posición inicial guardada (no se activó nunca antes).");
            return;
        }

        // Restaurar posición y rotación global
        transform.SetPositionAndRotation(initialWorldPosition, initialWorldRotation);

        // Resetear física
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }

        // Debug.Log($"{name} restablecido a su posición inicial.");
    }
}
