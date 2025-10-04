using UnityEngine;

public class MenuPositioner : MonoBehaviour
{
    public Transform playerCamera;
    public Vector3 offset = new Vector3(0f, -0.3f, 1.5f);

    private void OnEnable()
    {
        ColocarFrenteAlJugador();
    }

    
    public void ColocarFrenteAlJugador()
    {
        if (playerCamera != null)
        {
            transform.position = playerCamera.position + playerCamera.forward * offset.z + playerCamera.up * offset.y;
            transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
        }
    }
}
