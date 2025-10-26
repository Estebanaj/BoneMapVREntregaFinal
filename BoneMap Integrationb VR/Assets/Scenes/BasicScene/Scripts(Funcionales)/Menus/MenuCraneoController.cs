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
            "Hueso cigomático (L): Forma el pómulo izquierdo y parte del borde lateral de la órbita. Se articula con el frontal, temporal, maxilar y esfenoides. Su estructura en arco proporciona soporte facial y protección ocular. Participa en la inserción del músculo masetero y contribuye a la estética y simetría del rostro.",

            "Hueso cigomático (R): Simétrico al izquierdo, compone el pómulo derecho. Conecta la región orbitotemporal con la maxilar. Sirve como punto de anclaje para músculos faciales y de masticación. Refuerza la resistencia lateral del cráneo ante impactos.",

            "Hueso esfenoide: Hueso impar con forma de mariposa ubicado en la base del cráneo. Une frontal, temporales, occipital y etmoides. Posee el seno esfenoidal y la silla turca que alberga la hipófisis. Es esencial en la arquitectura del cráneo y paso de nervios craneales por los forámenes ópticos y redondos.",

            "Hueso etmoides: Pequeño y esponjoso, situado entre las órbitas. Forma parte del tabique nasal y del techo de las cavidades nasales. Contiene las celdillas etmoidales y la lámina cribosa, atravesada por filetes del nervio olfatorio. Su fragilidad lo hace susceptible a fracturas nasoetmoidales.",

            "Hueso frontal: Hueso plano que forma la frente y el techo de las órbitas. Posee senos frontales y una cresta que se articula con el etmoides. Protege el lóbulo frontal del cerebro. Interviene en la expresión facial y la estructura del macizo craneofacial anterior.",

            "Hueso maxilar (L): Constituye el maxilar superior izquierdo, sostiene los dientes superiores y forma parte de la órbita, cavidad nasal y paladar duro. Contiene el seno maxilar, cavidad neumática clave para resonancia vocal. Se articula con cigomático, nasal y palatino.",

            "Hueso maxilar (R): Homólogo al izquierdo. Estructura ósea esencial para la masticación y la expresión facial. Participa en la fijación dental y la conformación del arco infraorbitario. Interviene en el soporte del tabique nasal y la comunicación con el seno maxilar derecho.",

            "Hueso nasal (L): Pequeño hueso plano que forma la porción superior izquierda del dorso nasal. Une el frontal y el maxilar. Protege el cartílago nasal subyacente y define el perfil medio facial.",

            "Hueso nasal (R): Simétrico al izquierdo, compone la mitad derecha del puente nasal. Su unión central conforma la raíz de la nariz. Articula con el frontal y el maxilar derecho. Contribuye a la morfología nasal externa y al soporte del tabique cartilaginoso.",

            "Hueso occipital: Ubicado en la parte posterior e inferior del cráneo. Presenta el foramen magno por donde pasa la médula espinal. Se articula con el atlas y los huesos parietales. Aloja estructuras del cerebelo y forma la base craneocervical. Su protuberancia externa es palpable.",

            "Hueso palatino (L): Forma parte del paladar duro posterior, la cavidad nasal y la órbita izquierda. Su lámina horizontal se une al maxilar y contribuye al tabique nasal. Canaliza estructuras vasculares y nerviosas palatinas.",

            "Hueso palatino (R): Estructura par simétrica al izquierdo. Participa en la formación del paladar óseo y del piso de la cavidad nasal. Su lámina perpendicular delimita la pared nasal lateral. Es esencial para la separación buconasal.",

            "Hueso parietal (L): Forma la porción lateral y superior izquierda del cráneo. De tipo plano, protege el cerebro y define la bóveda craneal. Se une mediante suturas con frontal, occipital y temporal. Inserta músculos aponeuróticos del cuero cabelludo.",

            "Hueso parietal (R): Simétrico al izquierdo, cubre el hemisferio cerebral derecho. Aporta rigidez estructural al neurocráneo y se articula con su par contralateral en la sutura sagital. Permite la fijación de membranas meníngeas internas.",

            "Hueso temporal (L): Hueso complejo que protege el oído interno y medio. Contiene la apófisis mastoides, el conducto auditivo externo y la fosa mandibular. Aloja el sistema vestibular y participa en la audición. Su porción escamosa se articula con el parietal y esfenoides.",

            "Hueso temporal (R): Idéntico al izquierdo, resguarda las estructuras auditivas derechas. Su apófisis cigomática forma parte del arco cigomático. Permite la inserción de músculos de masticación y movimientos mandibulares.",

            "Mandíbula: Único hueso móvil del cráneo. Forma el arco inferior de la cara y sostiene los dientes inferiores. Se articula con los temporales mediante la articulación temporomandibular. Interviene en masticación, fonación y expresión facial. Contiene el foramen mentoniano y el canal mandibular.",

            "Vómer: Hueso delgado y plano que forma la porción posterior e inferior del tabique nasal. Divide las cavidades nasales y se articula con esfenoides, etmoides, palatinos y maxilares. Es esencial en la respiración y soporte del tabique medio."
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

        for (int i = 0; i < huesos.Length; i++)
        {
            if (huesos[i].hueso == huesoSeleccionado)
            {
                encontrado = true;

                if (i < nombres.Length && i < descripciones.Length)
                {
                    if (tituloTexto) tituloTexto.text = nombres[i];
                    if (descripcionTexto) descripcionTexto.text = descripciones[i];
                    Debug.Log($"[MenuCraneoController] Mostrando información de: {nombres[i]}");
                }
                else
                {
                    Debug.LogWarning($"[MenuCraneoController] Índice fuera de rango: {i}");
                }
                break;
            }
        }

        if (!encontrado)
            Debug.LogWarning($"[MenuCraneoController] Hueso no encontrado: {huesoSeleccionado.name}");
    }

    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona un hueso";
        if (descripcionTexto) descripcionTexto.text = "Agarra un hueso para ver su descripción médica.";
    }
}
