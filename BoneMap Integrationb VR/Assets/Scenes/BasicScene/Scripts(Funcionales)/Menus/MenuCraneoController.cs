using UnityEngine;
using TMPro;

public class MenuCraneoController : MonoBehaviour
{
    [System.Serializable]
    public class HuesoInfo
    {
        [Header("Asignaciones")]
        public string etiqueta;              // Nombre identificativo (ej: "Hueso frontal")
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
    // DATOS BASE
    private void InicializarDatos()
    {
        nombres = new string[]
        {
            "Hueso cigomático (L)", "Hueso cigomático (R)",
            "Hueso esfenoide", "Hueso etmoides", "Hueso frontal",
            "Hueso maxilar (L)", "Hueso maxilar (R)",
            "Hueso nasal (L)", "Hueso nasal (R)",
            "Hueso occipital",
            "Hueso palatino (L)", "Hueso palatino (R)",
            "Hueso parietal (L)", "Hueso parietal (R)",
            "Hueso temporal (L)", "Hueso temporal (R)",
            "Mandíbula", "Vómer"
        };

        descripciones = new string[]
        {
            // Cigomáticos
    "Estructura lateral izquierda del macizo facial que forma parte del pómulo y refuerza el borde orbitario lateral, aportando soporte a músculos faciales y masticatorios.",
    "Estructura lateral derecha del macizo facial encargada de sostener el pómulo y reforzar la órbita lateral, contribuyendo a la simetría y resistencia del rostro.",

    // Esfenoides
    "Hueso impar central en la base del cráneo con forma de mariposa que conecta múltiples regiones y aloja la cavidad para la glándula hipofisaria, permitiendo el paso de nervios importantes.",

    // Etmoides
    "Estructura ligera y esponjosa ubicada entre las órbitas, conforma parte del tabique nasal y del techo de la cavidad nasal, permitiendo el paso del nervio olfatorio a través de su lámina perforada.",

    // Frontal
    "Porción anterior del cráneo que forma la frente y el techo orbitario, protege el lóbulo frontal y contiene cavidades neumáticas que intervienen en la resonancia de la voz.",

    // Maxilares
    "Mitad izquierda del maxilar superior que sostiene dientes, forma parte de la órbita y cavidad nasal, y alberga un seno neumático de gran volumen.",
    "Mitad derecha del maxilar superior que interviene en la fijación dental, define la apariencia facial y participa en la estructura del paladar duro.",

    // Nasales
    "Segmento óseo izquierdo del puente nasal que contribuye al contorno superior de la nariz y sirve de soporte al cartílago nasal.",
    "Segmento óseo derecho del puente nasal que completa la parte superior del dorso nasal y aporta estabilidad al tabique cartilaginoso.",

    // Occipital
    "Región posterior e inferior del cráneo que contiene la gran apertura para el paso de la médula, se articula con la columna cervical y protege estructuras del cerebelo.",

    // Palatinos
    "Porción izquierda del paladar óseo posterior que contribuye a separar la cavidad oral de la nasal y ayuda a formar la pared nasal lateral.",
    "Porción derecha del paladar óseo posterior, complementa la estructura del tabique y participa en la arquitectura del piso de la cavidad nasal.",

    // Parietales
    "Mitad izquierda de la bóveda craneal que protege al encéfalo y forma parte de suturas que dan estabilidad al neurocráneo.",
    "Mitad derecha de la bóveda craneal responsable de cubrir el hemisferio cerebral derecho y contribuir a la robustez del cráneo mediante uniones suturales.",

    // Temporales
    "Región lateral izquierda del cráneo que alberga estructuras auditivas y vestibulares, y participa en movimientos mandibulares mediante su fosa articular.",
    "Región lateral derecha del cráneo que protege el sistema auditivo, forma parte del arco cigomático y sostiene inserciones musculares implicadas en la masticación.",

    // Mandíbula
    "Única pieza móvil del cráneo que sostiene los dientes inferiores y permite movimientos de masticación y fonación gracias a su articulación con los huesos temporales.",

    // Vómer
    "Lámina ósea delgada ubicada en el centro de la cavidad nasal, responsable de formar la parte posterior del tabique nasal y separar ambas fosas nasales."
        };

        Debug.Log($"[MenuCraneoController] Datos inicializados correctamente ({nombres.Length} huesos).");
    }

    // ----------------------------------------------------
    // CAPTURA DE POSICIONES INICIALES
    private void CapturarIniciales()
    {
        if (huesos == null || huesos.Length == 0)
        {
            Debug.LogWarning("[MenuCraneoController] No hay huesos asignados en el Inspector.");
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
    // UI
    public void MostrarInfoDeHueso(Transform huesoSeleccionado)
    {
        bool encontrado = false;

        // Iteramos sobre el array de nombres
        for (int i = 0; i < nombres.Length; i++)
        {
            // Comparar el nombre del hueso seleccionado con el array de nombres
            if (nombres[i] == huesoSeleccionado.name)
            {
                encontrado = true;

                // Mostrar el nombre y la descripción correspondiente usando el índice 'i'
                if (tituloTexto)
                    tituloTexto.text = nombres[i];  // Muestra el nombre del hueso

                if (descripcionTexto)
                    descripcionTexto.text = descripciones[i];  // Muestra la descripción correspondiente

                Debug.Log($"[MenuEscapulaController] Mostrando información de: {nombres[i]}");
                break;
            }
        }

        if (!encontrado)
        {
            Debug.LogWarning($"[MenuEscapulaController] Hueso no encontrado en la lista: {huesoSeleccionado.name}");
        }
    }

    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona un hueso";
        if (descripcionTexto) descripcionTexto.text = "Agarra un hueso para ver su descripción médica.";
    }
}
