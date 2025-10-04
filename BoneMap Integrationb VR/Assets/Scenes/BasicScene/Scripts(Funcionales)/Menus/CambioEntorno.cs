using UnityEngine;

public class CambioEntorno : MonoBehaviour
{
    public GameObject EntornoLight;
    public GameObject EntornoDark;

    public void CambiarEntorno(bool usarEntornoDark)
    {
        EntornoLight.SetActive(!usarEntornoDark);
        EntornoDark.SetActive(usarEntornoDark);
    }
}
