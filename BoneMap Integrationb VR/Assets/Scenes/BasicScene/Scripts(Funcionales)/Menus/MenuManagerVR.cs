using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI; // Para trabajar con UI

public class MenuManagerVR : MonoBehaviour
{
    [Header("Referencias de los Canvas")]
    public GameObject CanvasMenuPrincipal;
    public GameObject CanvasMenuAprendizaje;
    public GameObject CanvasMenuPausa;
    public GameObject CanvasMenuRegionTutorial;
    public GameObject CanvasMenuRegionQuiz;
    public GameObject CanvasMenuConfiguracion;
    public GameObject CanvasMenuCreditos;

    [Header("Escenario (Menú/Pausa)")]
    public GameObject EscenarioMenu;   // "EscenarioMenu" (se usa tanto para el menú como para la pausa)
    public GameObject EscenarioModoLibre;  // "EscenarioModoLibre"
    public GameObject Modelos;         // "Modelos"

    [Header("Opcional: Cámara del jugador (para MenuPositioner)")]
    public Transform playerCamera;

    [Header("Entrada Quest 2")]
    public bool usarBotonMenuIzquierdo = true;
    private bool menuBtnPrev = false;

    // Coordenadas y rotaciones
    public Vector3 habitacionMenuPausa = new Vector3(11.4f, 0.158f, 9.821f); // Usamos la misma coordenada para el menú/pausa
    public Vector3 habitacionModoLibre = new Vector3(0.362f, 0.158f, 13.115f);
    public Quaternion rotacionMenuPausa = Quaternion.Euler(0, 0, 0);  // Usamos la misma rotación para el menú/pausa
    public Quaternion rotacionModoLibre = Quaternion.Euler(0, 180, 0);

    // Referencia al XR Rig
    public GameObject XRrig;

    // Botones de UI
    public Button botonModoLibre;
    public Button botonSalir;

    // Nueva variable para controlar si estamos en "modo libre" y si estamos en "partida activa"
    private bool enModoLibre = false;
    private bool enPartida = false;

    void Start()
    {
        // Asegúrate de que los botones estén conectados y añade el listener para el click
        if (botonModoLibre != null)
        {
            botonModoLibre.onClick.AddListener(IrAModoLibre);
        }

        if (botonSalir != null)
        {
            botonSalir.onClick.AddListener(IrAPausa);
        }
    }

    void Update()
    {
        if (!usarBotonMenuIzquierdo) return;

        // Botón "tres rayas" mando izquierdo
        var left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (left.isValid && left.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed))
        {
            if (pressed && !menuBtnPrev)
            {
                // Solo abre el menú si estamos en "modo libre"
                if (enPartida)
                {
                    AbrirMenuPausaDesdeControl();
                }
                else
                {
                    ToggleMenuPausa();
                }
            }
            menuBtnPrev = pressed;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenuPausa();
#endif
    }

    void OcultarTodosLosCanvas()
    {
        if (CanvasMenuAprendizaje) CanvasMenuAprendizaje.SetActive(false);
        if (CanvasMenuPausa) CanvasMenuPausa.SetActive(false);
        if (CanvasMenuRegionTutorial) CanvasMenuRegionTutorial.SetActive(false);
        if (CanvasMenuRegionQuiz) CanvasMenuRegionQuiz.SetActive(false);
        if (CanvasMenuConfiguracion) CanvasMenuConfiguracion.SetActive(false);
        if (CanvasMenuCreditos) CanvasMenuCreditos.SetActive(false);
        if (CanvasMenuPrincipal) CanvasMenuPrincipal.SetActive(true); // siempre activo
    }

    // ---------------- Método para teletransportarse ----------------
    void Teletransportarse(Vector3 nuevaPosicion, Quaternion nuevaRotacion)
    {
        XRrig.transform.position = nuevaPosicion;
        XRrig.transform.rotation = nuevaRotacion;
    }

    // ---------------- Modo Libre (UI) ----------------
    public void IrAModoLibre()
    {
        Teletransportarse(habitacionModoLibre, rotacionModoLibre);
        enModoLibre = true;  // Marcamos que estamos en modo libre
        enPartida = true;  // Activamos la partida
    }

    // ---------------- Pausa (toggle) ----------------
    public void ToggleMenuPausa()
    {
        if (CanvasMenuPausa != null && CanvasMenuPausa.activeSelf)
        {
            // Si ya está activo → reanudar
            Reanudar();
        }
        else
        {
            // Si no está activo → abrir pausa
            AbrirMenuPausaDesdeControl();
        }
    }

    public void AbrirMenuPausaDesdeControl()
    {
        OcultarTodosLosCanvas();
        if (CanvasMenuPausa) CanvasMenuPausa.SetActive(true);
        Teletransportarse(habitacionMenuPausa, rotacionMenuPausa);
        enModoLibre = false;  // Marcamos que estamos en el menú/pausa
        enPartida = true;  // Mantenemos la partida activa, ya que solo estamos en pausa
    }

    public void Reanudar()
    {
        // Ahora solo te teletransporta a modo libre sin volver al menú
        Teletransportarse(habitacionModoLibre, rotacionModoLibre);
        enModoLibre = true;  // Aseguramos que estemos en modo libre
        enPartida = true;  // Aseguramos que la partida sigue activa
    }

    // ---------------- Método para ir a pausa ----------------
    public void IrAPausa()
    {
        Teletransportarse(habitacionMenuPausa, rotacionMenuPausa);
        enModoLibre = false;  // Aseguramos que estemos en pausa, no en modo libre
        enPartida = false;  // Aseguramos que no estamos en partida
    }

    // ---------------- **Salir de la partida** ----------------
    public void SalirDePartida()
    {
        // Cerrar el Canvas de pausa y abrir el Canvas principal
        if (CanvasMenuPausa) CanvasMenuPausa.SetActive(false);  // Cierra el menú de pausa
        if (CanvasMenuPrincipal) CanvasMenuPrincipal.SetActive(true);  // Muestra el menú principal
        enPartida = false;  // Marcamos que no estamos en partida activa
        Debug.Log("Saliendo de la partida: Cierra el menú de pausa y abre el principal.");
    }

    // ---------------- **Salir del juego** ----------------
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    // ---------------- Métodos para activar los menús ----------------
    public void AbrirMenuAprendizaje() => ActivarSoloCanvas(CanvasMenuAprendizaje);
    public void AbrirMenuConfiguracion() => ActivarSoloCanvas(CanvasMenuConfiguracion);
    public void AbrirMenuCreditos() => ActivarSoloCanvas(CanvasMenuCreditos);
    public void AbrirMenuRegionTutorial() => ActivarSoloCanvas(CanvasMenuRegionTutorial);
    public void AbrirMenuRegionQuiz() => ActivarSoloCanvas(CanvasMenuRegionQuiz);

    // ---------------- Volver al Menú Principal ----------------
    public void VolverAlMenuPrincipal()
    {
        ActivarSoloCanvas(CanvasMenuPrincipal);
        if (EscenarioMenu) EscenarioMenu.SetActive(true); // Siempre está activo
    }

    // ---------------- Volver de Configuración ----------------
    public void VolverDeConfiguracion()
    {
        Debug.Log("Volver de Configuración, enPartida: " + enPartida); // Depuración

        if (enPartida) // Si estamos en partida, volver al menú de pausa
        {
            Debug.Log("Volviendo al menú de pausa desde configuración"); // Depuración
            AbrirMenuPausaDesdeControl();
        }
        else
        {
            // Si no estamos en partida, volver al menú principal
            Debug.Log("Volviendo al menú principal desde configuración"); // Depuración
            ActivarSoloCanvas(CanvasMenuPrincipal);  // Regresamos al Canvas del menú principal
        }
    }

    // ---------------- Volver de Créditos ----------------
    public void VolverDeCreditos()
    {
        Debug.Log("Volver de Créditos, enPartida: " + enPartida); // Depuración

        if (enPartida) // Si estamos en partida, volver al menú de pausa
        {
            Debug.Log("Volviendo al menú de pausa desde créditos"); // Depuración
            AbrirMenuPausaDesdeControl();
        }
        else
        {
            // Si no estamos en partida, volver al menú principal
            Debug.Log("Volviendo al menú principal desde créditos"); // Depuración
            ActivarSoloCanvas(CanvasMenuPrincipal);  // Regresamos al Canvas del menú principal
        }
    }


    // ---------------- Función para activar solo un Canvas ----------------
    void ActivarSoloCanvas(GameObject canvasActivo)
    {
        OcultarTodosLosCanvas();
        if (canvasActivo) canvasActivo.SetActive(true);
    }
}
