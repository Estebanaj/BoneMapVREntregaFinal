using UnityEngine;
using TMPro;

public class MenuColumnaController : MonoBehaviour
{
    [System.Serializable]
    public class HuesoInfo
    {
        [Header("Asignaciones")]
        public string etiqueta;              // Nombre identificativo (ej: "Atlas (C1)")
        public Transform hueso;              // Objeto 3D del hueso real
        public Transform destinoFantasma;    // Punto donde encaja (para efecto de brillo)

        [HideInInspector] public Vector3 posInicialWorld;
        [HideInInspector] public Quaternion rotInicialWorld;
        [HideInInspector] public bool inicialCapturada;
    }

    [Header("Referencias UI")]
    public TMP_Text tituloTexto;             // Nombre del hueso
    public TMP_Text descripcionTexto;        // Descripción médica

    [Header("Lista de Huesos")]
    public HuesoInfo[] huesos;               // Un elemento por hueso

    private string[] nombres;
    private string[] descripciones;
    private bool datosInicializados = false;
    private bool escenaInicializada = false;

    // ----------------------------------------------------
    void Awake()
    {
        InicializarDatos();
    }

    void OnEnable()
    {
        if (!escenaInicializada)
        {
            CapturarIniciales();
            escenaInicializada = true;
        }

        if (!datosInicializados)
        {
            InicializarDatos();
            datosInicializados = true;
        }

        ResetMenu();
    }

    // ----------------------------------------------------
    private void InicializarDatos()
    {
        nombres = new string[]
        {
            "Atlas (C1)", "Axis (C2)", "Vértebra C3", "Vértebra C4", "Vértebra C5", "Vértebra C6", "Vértebra C7",
            "Vértebra T1", "Vértebra T2", "Vértebra T3", "Vértebra T4", "Vértebra T5", "Vértebra T6", "Vértebra T7",
            "Vértebra T8", "Vértebra T9", "Vértebra T10", "Vértebra T11", "Vértebra T12",
            "Vértebra L1", "Vértebra L2", "Vértebra L3", "Vértebra L4", "Vértebra L5",
            "Hueso sacro", "Cóccix", "Hueso coxal (L)", "Hueso coxal (R)"
        };

        descripciones = new string[]
        {
            "Primera vértebra cervical, carece de cuerpo y apófisis espinosa. Sostiene el cráneo y permite movimientos de asentir.",
            "Segunda vértebra cervical con apófisis odontoides; permite rotación lateral de la cabeza.",
            "Vértebra pequeña, con cuerpo ovalado y apófisis espinosa bífida. Transmite peso craneal.",
            "Estructura con carillas oblicuas que facilita inclinación y rotación cervical.",
            "Cuerpo mayor y apófisis prominente; conecta movilidad y estabilidad cervical.",
            "Posee tubérculo carotídeo, punto clínico para comprimir arteria carótida.",
            "“Vértebra prominente”, su apófisis espinosa marca el fin del cuello.",
            "Primera torácica, articula con primera costilla; une cuello y tórax.",
            "Articula con costillas 2 y 3, limita rotación cervical.",
            "Mantiene alineación torácica; inserción de músculos dorsales.",
            "Vértebra media torácica, canal estrecho y movilidad limitada.",
            "Sostiene quinta costilla; mantiene curvatura torácica.",
            "Eje central del tórax; control respiratorio y rigidez axial.",
            "Referencia de la escápula; inserción de músculos dorsales profundos.",
            "Transición hacia región inferior torácica; cuerpo voluminoso.",
            "Articula con novena costilla; soporte firme del tórax.",
            "Décima costilla; transición hacia zona lumbar.",
            "Conecta costilla flotante 11; inicio región lumbar.",
            "Última torácica; transfiere peso al sector lumbar.",
            "Primera lumbar, cuerpo macizo; movilidad amplia en flexión-extensión.",
            "Cuerpo cóncavo; contribuye a la lordosis lumbar.",
            "Centro de la lordosis; equilibrio postural clave.",
            "Bisagra lumbosacra; transmite peso al sacro.",
            "Cuerpo masivo; forma ángulo lumbosacro.",
            "Sacro fusionado de 5 vértebras; transmite peso al anillo pélvico.",
            "Cóccix vestigial; inserción de músculos del piso pélvico.",
            "Coxal izquierdo; fusiona ilion, isquion y pubis.",
            "Coxal derecho; completa el anillo pélvico."
        };

        Debug.Log($"[MenuColumnaController] Datos cargados: {nombres.Length} nombres y {descripciones.Length} descripciones.");
    }

    // ----------------------------------------------------
    private void CapturarIniciales()
    {
        if (huesos == null || huesos.Length == 0)
        {
            Debug.LogWarning("[MenuColumnaController] No hay huesos asignados en el Inspector.");
            return;
        }

        foreach (var h in huesos)
        {
            if (h == null || h.hueso == null) continue;

            if (!h.inicialCapturada)
            {
                h.posInicialWorld = h.hueso.position;
                h.rotInicialWorld = h.hueso.rotation;
                h.inicialCapturada = true;
            }
        }
    }

    // ----------------------------------------------------
    public void MostrarInfoDeHueso(Transform huesoSeleccionado)
    {
        bool encontrado = false;

        for (int i = 0; i < huesos.Length; i++)
        {
            if (huesos[i].hueso == huesoSeleccionado)
            {
                encontrado = true;

                if (i < nombres.Length && i < descripciones.Length)
                {
                    if (tituloTexto) tituloTexto.text = nombres[i];
                    if (descripcionTexto) descripcionTexto.text = descripciones[i];
                    Debug.Log($"[MenuColumnaController] Mostrando info de: {nombres[i]}");
                }
                else
                {
                    Debug.LogWarning($"[MenuColumnaController] Índice fuera de rango ({i}) para {huesoSeleccionado.name}");
                }
                break;
            }
        }

        if (!encontrado)
            Debug.LogWarning($"[MenuColumnaController] Hueso no encontrado en la lista: {huesoSeleccionado.name}");
    }

    // ----------------------------------------------------
    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona un hueso";
        if (descripcionTexto) descripcionTexto.text = "Agarra un hueso para ver su descripción médica.";
    }
}
