using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalirJuego : MonoBehaviour
{
    // Método que se llamará cuando el usuario presione el botón de salir
    public void Salir()
    {
        // Cierra la aplicación
        Debug.Log("Cerrando el juego...");
        Application.Quit();

        // Si estás en el editor de Unity, también se detiene el juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
