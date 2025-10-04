using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicaFondo;

    public void CambiarVolumen(float volumen)
    {
        if (musicaFondo != null)
            musicaFondo.volume = volumen;
    }
}
