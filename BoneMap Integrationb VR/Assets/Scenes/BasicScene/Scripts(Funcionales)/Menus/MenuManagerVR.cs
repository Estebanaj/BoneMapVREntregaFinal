using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class MenuManagerVR : MonoBehaviour
{
    [Header("Referencias de los Canvas")]
    public GameObject CanvasMenuPrincipal;
    public GameObject CanvasMenuAprendizaje;
    public GameObject CanvasMenuPausa;
    public GameObject CanvasMenuRegionTutorial;
    public GameObject CanvasMenuConfiguracion;
    public GameObject CanvasMenuCreditos;

    [Header("Escenarios del juego")]
    public GameObject EscenarioMenu;                     // 🔸 Este nunca se apaga
    public GameObject EscenarioPrincipal;
    public GameObject EscenarioPrincipalColumna;
    public GameObject EscenarioPrincipalCraneo;
    public GameObject EscenarioPrincipalEscapula;
    public GameObject EscenarioPrincipalPiernas;
    public GameObject EscenarioPrincipalBrazos;

    [Header("Opcional: Cámara del jugador (para MenuPositioner)")]
    public Transform playerCamera;

    [Header("Entrada Quest 2")]
    public bool usarBotonMenuIzquierdo = true;
    private bool menuBtnPrev = false;

    // ---------------- POSICIONES ----------------
    public Vector3 habitacionMenuPausa = new Vector3(11.4f, 0.158f, 9.821f);
    public Vector3 habitacionModoLibre = new Vector3(0.362f, 0.158f, 13.115f);
    public Quaternion rotacionMenuPausa = Quaternion.Euler(0, 0, 0);
    public Quaternion rotacionModoLibre = Quaternion.Euler(0, 180, 0);

    // Nuevos modos
    public Vector3 habitacionColumna = new Vector3(23.735f, 0.158f, -5.671f);
    public Quaternion rotacionColumna = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);

    public Vector3 habitacionCraneo = new Vector3(23.735f, 0.158f, 11.472f);
    public Quaternion rotacionCraneo = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);

    public Vector3 habitacionEscapula = new Vector3(23.735f, 0.158f, 28.443f);
    public Quaternion rotacionEscapula = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);

    public Vector3 habitacionPiernas = new Vector3(-13.83f, 0.158f, 11.12f);
    public Quaternion rotacionPiernas = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);

    public Vector3 habitacionBrazos = new Vector3(-14.13f, 0.158f, -4.32f);
    public Quaternion rotacionBrazos = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);

    // ---------------- REFERENCIAS ----------------
    public GameObject XRrig;
    public Button botonModoLibre;
    public Button botonSalir;

    // ---------------- ESTADOS ----------------
    private bool enPartida = false;
    private bool enModoLibre = false;
    private bool enModoColumna = false;
    private bool enModoCraneo = false;
    private bool enModoEscapula = false;
    private bool enModoPiernas = false;
    private bool enModoBrazos = false;

    void Start()
    {
        // 🔹 Apagar todos los escenarios excepto el menú (que nunca se apaga)
        ApagarTodosLosEscenarios();
        if (EscenarioMenu) EscenarioMenu.SetActive(true);

        // 🔹 Configurar botones
        if (botonModoLibre != null)
            botonModoLibre.onClick.AddListener(IrAModoLibre);
        if (botonSalir != null)
            botonSalir.onClick.AddListener(IrAPausa);
    }

    void Update()
    {
        if (!usarBotonMenuIzquierdo) return;

        var left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (left.isValid && left.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed))
        {
            if (pressed && !menuBtnPrev)
            {
                if (enPartida)
                    AbrirMenuPausaDesdeControl();
                else
                    ToggleMenuPausa();
            }
            menuBtnPrev = pressed;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenuPausa();
#endif
    }

    // ---------------- ESCENARIOS ----------------
    void ApagarTodosLosEscenarios()
    {
        // ⚠️ EscenarioMenu nunca se apaga
        if (EscenarioPrincipal) EscenarioPrincipal.SetActive(false);
        if (EscenarioPrincipalColumna) EscenarioPrincipalColumna.SetActive(false);
        if (EscenarioPrincipalCraneo) EscenarioPrincipalCraneo.SetActive(false);
        if (EscenarioPrincipalEscapula) EscenarioPrincipalEscapula.SetActive(false);
        if (EscenarioPrincipalPiernas) EscenarioPrincipalPiernas.SetActive(false);
        if (EscenarioPrincipalBrazos) EscenarioPrincipalBrazos.SetActive(false);
    }

    void ActivarEscenario(GameObject escenario)
    {
        // Apagar los escenarios principales, pero sin tocar EscenarioMenu
        if (EscenarioPrincipal) EscenarioPrincipal.SetActive(false);
        if (EscenarioPrincipalColumna) EscenarioPrincipalColumna.SetActive(false);
        if (EscenarioPrincipalCraneo) EscenarioPrincipalCraneo.SetActive(false);
        if (EscenarioPrincipalEscapula) EscenarioPrincipalEscapula.SetActive(false);
        if (EscenarioPrincipalPiernas) EscenarioPrincipalPiernas.SetActive(false);
        if (EscenarioPrincipalBrazos) EscenarioPrincipalBrazos.SetActive(false);

        // 🔹 Asegurar que EscenarioMenu SIEMPRE permanezca encendido
        if (EscenarioMenu && !EscenarioMenu.activeSelf)
            EscenarioMenu.SetActive(true);

        // 🔹 Activar el escenario correspondiente
        if (escenario)
        {
            escenario.SetActive(true);
            Debug.Log($"[MenuManagerVR] Activando escenario: {escenario.name}");
        }
        else
        {
            Debug.LogWarning("[MenuManagerVR] Escenario no asignado o nulo.");
        }
    }


    // ---------------- CANVAS ----------------
    void OcultarTodosLosCanvas()
    {
        if (CanvasMenuAprendizaje) CanvasMenuAprendizaje.SetActive(false);
        if (CanvasMenuPausa) CanvasMenuPausa.SetActive(false);
        if (CanvasMenuRegionTutorial) CanvasMenuRegionTutorial.SetActive(false);
        if (CanvasMenuConfiguracion) CanvasMenuConfiguracion.SetActive(false);
        if (CanvasMenuCreditos) CanvasMenuCreditos.SetActive(false);
        if (CanvasMenuPrincipal) CanvasMenuPrincipal.SetActive(true);
    }

    void Teletransportarse(Vector3 nuevaPos, Quaternion nuevaRot)
    {
        XRrig.transform.position = nuevaPos;
        XRrig.transform.rotation = nuevaRot;
    }

    void ResetModos()
    {
        enModoLibre = false;
        enModoColumna = false;
        enModoCraneo = false;
        enModoEscapula = false;
        enModoPiernas = false;
        enModoBrazos = false;
    }

    // ---------------- MODOS ----------------
    public void IrAModoLibre()
    {
        ResetModos();
        ActivarEscenario(EscenarioPrincipal);
        Teletransportarse(habitacionModoLibre, rotacionModoLibre);
        enModoLibre = true;
        enPartida = true;
    }

    public void IrAModoColumna()
    {
        ResetModos();
        ActivarEscenario(EscenarioPrincipalColumna);
        Teletransportarse(habitacionColumna, rotacionColumna);
        enModoColumna = true;
        enPartida = true;
    }

    public void IrAModoCraneo()
    {
        ResetModos();
        ActivarEscenario(EscenarioPrincipalCraneo);
        Teletransportarse(habitacionCraneo, rotacionCraneo);
        enModoCraneo = true;
        enPartida = true;
    }

    public void IrAModoEscapula()
    {
        ResetModos();
        ActivarEscenario(EscenarioPrincipalEscapula);
        Teletransportarse(habitacionEscapula, rotacionEscapula);
        enModoEscapula = true;
        enPartida = true;
    }

    public void IrAModoPiernas()
    {
        ResetModos();
        ActivarEscenario(EscenarioPrincipalPiernas);
        Teletransportarse(habitacionPiernas, rotacionPiernas);
        enModoPiernas = true;
        enPartida = true;
    }

    public void IrAModoBrazos()
    {
        ResetModos();
        ActivarEscenario(EscenarioPrincipalBrazos);
        Teletransportarse(habitacionBrazos, rotacionBrazos);
        enModoBrazos = true;
        enPartida = true;
    }

    // ---------------- PAUSA ----------------
    public void ToggleMenuPausa()
    {
        if (CanvasMenuPausa != null && CanvasMenuPausa.activeSelf)
            Reanudar();
        else
            AbrirMenuPausaDesdeControl();
    }

    public void AbrirMenuPausaDesdeControl()
    {
        OcultarTodosLosCanvas();
        if (CanvasMenuPausa) CanvasMenuPausa.SetActive(true);
        Teletransportarse(habitacionMenuPausa, rotacionMenuPausa);
        enPartida = true;
    }

    public void Reanudar()
    {
        if (CanvasMenuPausa)
            CanvasMenuPausa.SetActive(false);

        if (enModoLibre)
            Teletransportarse(habitacionModoLibre, rotacionModoLibre);
        else if (enModoColumna)
            Teletransportarse(habitacionColumna, rotacionColumna);
        else if (enModoCraneo)
            Teletransportarse(habitacionCraneo, rotacionCraneo);
        else if (enModoEscapula)
            Teletransportarse(habitacionEscapula, rotacionEscapula);
        else if (enModoPiernas)
            Teletransportarse(habitacionPiernas, rotacionPiernas);
        else if (enModoBrazos)
            Teletransportarse(habitacionBrazos, rotacionBrazos);
        else
            Teletransportarse(habitacionMenuPausa, rotacionMenuPausa);

        enPartida = true;
    }

    // ---------------- SALIR Y MENÚ ----------------
    public void IrAPausa()
    {
        Teletransportarse(habitacionMenuPausa, rotacionMenuPausa);
        ResetModos();
        ApagarTodosLosEscenarios();
        // EscenarioMenu nunca se apaga, por lo tanto no lo tocamos
        enPartida = false;
    }

    // ---------------- SALIR Y MENÚ ----------------
    public void SalirDePartida()
    {
        if (CanvasMenuPausa) CanvasMenuPausa.SetActive(false);

        // 🔹 Antes de apagar los escenarios, reseteamos los objetos
        ResetearObjetosDeEscenario();

        // 🔹 Apagamos todos los escenarios excepto el menú
        ApagarTodosLosEscenarios();
        if (EscenarioMenu) EscenarioMenu.SetActive(true);

        // 🔹 Activamos el menú principal
        if (CanvasMenuPrincipal) CanvasMenuPrincipal.SetActive(true);

        // 🔹 Reseteamos flags
        enPartida = false;
        ResetModos();

        Debug.Log("Saliendo de la partida: objetos reestablecidos y regreso al menú principal.");
    }

    // ---------------- RESETEO DE OBJETOS ----------------
    void ResetearObjetosDeEscenario()
    {
        // Busca todos los objetos activos o inactivos con el script ResettableObject
        ResettableObject[] objetos = FindObjectsOfType<ResettableObject>(true); // true = incluye desactivados

        foreach (ResettableObject obj in objetos)
        {
            obj.ResetTransform();
        }

        Debug.Log($"🔄 Se reestablecieron {objetos.Length} objetos a su posición original.");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    // ---------------- MENÚS ----------------
    public void AbrirMenuAprendizaje() => ActivarSoloCanvas(CanvasMenuAprendizaje);
    public void AbrirMenuConfiguracion() => ActivarSoloCanvas(CanvasMenuConfiguracion);
    public void AbrirMenuCreditos() => ActivarSoloCanvas(CanvasMenuCreditos);
    public void AbrirMenuRegionTutorial() => ActivarSoloCanvas(CanvasMenuRegionTutorial);

    public void VolverAlMenuPrincipal()
    {
        ActivarSoloCanvas(CanvasMenuPrincipal);
        // EscenarioMenu siempre está activo
    }

    public void VolverDeConfiguracion()
    {
        if (enPartida) AbrirMenuPausaDesdeControl();
        else ActivarSoloCanvas(CanvasMenuPrincipal);
    }

    public void VolverDeCreditos()
    {
        if (enPartida) AbrirMenuPausaDesdeControl();
        else ActivarSoloCanvas(CanvasMenuPrincipal);
    }



    public void VolverDeRegionTutorial()
    {
        if (enPartida) AbrirMenuPausaDesdeControl();
        else ActivarSoloCanvas(CanvasMenuPrincipal);
    }

    // ---------------- AUXILIAR ----------------
    void ActivarSoloCanvas(GameObject canvas)
    {
        OcultarTodosLosCanvas();
        if (canvas) canvas.SetActive(true);
    }
}
