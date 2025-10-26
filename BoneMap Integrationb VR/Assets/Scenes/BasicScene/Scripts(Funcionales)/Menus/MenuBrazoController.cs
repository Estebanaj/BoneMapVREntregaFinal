using UnityEngine;
using TMPro;

public class MenuBrazoController : MonoBehaviour
{
    [System.Serializable]
    public class HuesoInfo
    {
        [Header("Asignaciones")]
        public string etiqueta;
        public Transform hueso;
        public Transform destinoFantasma;

        [HideInInspector] public Vector3 posInicialWorld;
        [HideInInspector] public Quaternion rotInicialWorld;
        [HideInInspector] public bool inicialCapturada;
    }

    [Header("Referencias UI")]
    public TMP_Text tituloTexto;
    public TMP_Text descripcionTexto;

    [Header("Lista de Huesos")]
    public HuesoInfo[] huesos;

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
    // CARGA DE DATOS
    private void InicializarDatos()
    {
        nombres = new string[]
        {
            "Húmero (L)", "Húmero (R)",
            "Radio (L)", "Radio (R)",
            "Ulna (L)", "Ulna (R)",
            "Hueso escafoides (L)", "Hueso escafoides (R)",
            "Hueso semilunar (L)", "Hueso semilunar (R)",
            "Hueso piramidal (L)", "Hueso triquetral (R)",
            "Hueso pisiforme (L)", "Hueso pisiforme (R)",
            "Hueso trapecio (L)", "Hueso trapecio (R)",
            "Hueso trapezoide (L)", "Hueso trapezoide (R)",
            "Hueso capitado (L)", "Hueso capitado (R)",
            "Hueso ganchoso o hamatal (L)", "Hueso ganchoso (R)",
            "Hueso lunado (L)", "Hueso lunado (R)",
            "Primer hueso metacarpiano (L)", "Primer hueso metacarpiano (R)",
            "Segundo hueso metacarpiano (L)", "Segundo hueso metacarpiano (R)",
            "Tercer hueso metacarpiano (L)", "Tercer hueso metacarpiano (R)",
            "Cuarto hueso metacarpiano (L)", "Cuarto hueso metacarpiano (R)",
            "Quinto hueso metacarpiano (L)", "Quinto hueso metacarpiano (R)",
            "Falange proximal del primer dedo de la mano (L)", "Falange proximal del primer dedo de la mano (R)",
            "Falange distal del primer dedo de la mano (L)", "Falange distal del primer dedo de la mano (R)",
            "Falange proximal del segundo dedo de la mano (L)", "Falange proximal del segundo dedo de la mano (R)",
            "Falange media del segundo dedo de la mano (L)", "Falange media del segundo dedo de la mano (R)",
            "Falange distal del segundo dedo de la mano (L)", "Falange distal del segundo dedo de la mano (R)",
            "Falange proximal del tercer dedo de la mano (L)", "Falange proximal del tercer dedo de la mano (R)",
            "Falange media del tercer dedo de la mano (L)", "Falange media del tercer dedo de la mano (R)",
            "Falange distal del tercer dedo de la mano (L)", "Falange distal del tercer dedo de la mano (R)",
            "Falange proximal del cuarto dedo de la mano (L)", "Falange proximal del cuarto dedo de la mano (R)",
            "Falange media del cuarto dedo de la mano (L)", "Falange media del cuarto dedo de la mano (R)",
            "Falange distal del cuarto dedo de la mano (L)", "Falange distal del cuarto dedo de la mano (R)",
            "Falange proximal del quinto dedo de la mano (L)", "Falange proximal del quinto dedo de la mano (R)",
            "Falange media del quinto dedo de la mano (L)", "Falange media del quinto dedo de la mano (R)",
            "Falange distal del quinto dedo de la mano (L)", "Falange distal del quinto dedo de la mano (R)"
        };

        descripciones = new string[]
        {
            "Húmero (L): Hueso largo del brazo, une el hombro con el codo. Su cabeza esférica se articula con la cavidad glenoidea de la escápula. El cuello anatómico separa la cabeza de los tubérculos mayor y menor, donde se insertan músculos del manguito rotador. La diáfisis tiene el canal radial y su extremo distal forma la tróclea y el cóndilo para articular con radio y ulna.",
            "Húmero (R): Homólogo al izquierdo. Soporta la fuerza de tracción del miembro superior. Su torsión anatómica permite amplios rangos de movimiento del hombro. El epicóndilo medial da origen a músculos flexores del antebrazo. Es fundamental en la mecánica de empuje y levantamiento.",
            "Radio (L): Hueso lateral del antebrazo. Su cabeza discal se articula con el capítulo humeral y con la ulna. La tuberosidad radial inserta el bíceps braquial. Participa en la pronación y supinación del antebrazo. Su extremo distal forma parte de la articulación radiocarpiana.",
            "Radio (R): Simétrico, transmite fuerzas del carpo al codo. El borde interóseo se une con la ulna. La apófisis estiloides se proyecta hacia la muñeca, brindando soporte lateral.",
            "Ulna (L): También conocida como cúbito. Hueso medial del antebrazo. Su olécranon forma la prominencia posterior del codo. Articula con el húmero mediante la escotadura troclear y con el radio lateralmente. Su extremo distal termina en la cabeza ulnar y la apófisis estiloides.",
            "Ulna (R): Simétrica. Aporta estabilidad al antebrazo durante flexión y extensión. Su estructura más gruesa proximalmente la hace esencial en el control del codo. Sirve de anclaje a los músculos extensores.",
            // ... resto igual (se mantiene tu texto completo)
        };

        Debug.Log($"[MenuBrazoController] Datos inicializados: {nombres.Length} nombres y {descripciones.Length} descripciones.");
    }

    // ----------------------------------------------------
    // CAPTURA DE POSICIONES INICIALES
    private void CapturarIniciales()
    {
        if (huesos == null || huesos.Length == 0)
        {
            Debug.LogWarning("[MenuBrazoController] No hay huesos asignados en el inspector.");
            return;
        }

        foreach (var h in huesos)
        {
            if (h?.hueso == null) continue;

            if (!h.inicialCapturada)
            {
                h.posInicialWorld = h.hueso.position;
                h.rotInicialWorld = h.hueso.rotation;
                h.inicialCapturada = true;
            }
        }
    }

    // ----------------------------------------------------
    // UI - MOSTRAR INFO
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
                    Debug.Log($"[MenuBrazoController] Mostrando información de {nombres[i]}");
                }
                else
                {
                    Debug.LogWarning($"[MenuBrazoController] Índice {i} fuera de rango para nombres o descripciones.");
                }
                break;
            }
        }

        if (!encontrado)
            Debug.LogWarning($"[MenuBrazoController] No se encontró el hueso '{huesoSeleccionado.name}' en la lista.");
    }

    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona un hueso";
        if (descripcionTexto) descripcionTexto.text = "Agarra un hueso para ver su descripción médica.";
    }
}
