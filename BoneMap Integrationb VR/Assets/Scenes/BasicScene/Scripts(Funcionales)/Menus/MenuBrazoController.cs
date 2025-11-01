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
    // --- BRAZO ---
    "Húmero (L): Hueso largo del brazo, une el hombro con el codo. Su cabeza esférica se articula con la cavidad glenoidea de la escápula. El cuello anatómico separa la cabeza de los tubérculos mayor y menor, donde se insertan músculos del manguito rotador. La diáfisis tiene el canal radial y su extremo distal forma la tróclea y el cóndilo para articular con radio y ulna.",
    "Húmero (R): Homólogo al izquierdo. Soporta la fuerza de tracción del miembro superior. Su torsión anatómica permite amplios rangos de movimiento del hombro. El epicóndilo medial da origen a músculos flexores del antebrazo. Es fundamental en la mecánica de empuje y levantamiento.",
    "Radio (L): Hueso lateral del antebrazo. Su cabeza discal se articula con el capítulo humeral y con la ulna. La tuberosidad radial inserta el bíceps braquial. Participa en la pronación y supinación del antebrazo. Su extremo distal forma parte de la articulación radiocarpiana.",
    "Radio (R): Simétrico, transmite fuerzas del carpo al codo. El borde interóseo se une con la ulna. La apófisis estiloides se proyecta hacia la muñeca, brindando soporte lateral.",
    "Ulna (L): También conocida como cúbito. Hueso medial del antebrazo. Su olécranon forma la prominencia posterior del codo. Articula con el húmero mediante la escotadura troclear y con el radio lateralmente. Su extremo distal termina en la cabeza ulnar y la apófisis estiloides.",
    "Ulna (R): Simétrica. Aporta estabilidad al antebrazo durante flexión y extensión. Su estructura más gruesa proximalmente la hace esencial en el control del codo. Sirve de anclaje a los músculos extensores.",

    // --- CARPIANOS ---
    "Hueso escafoides (L): Hueso alargado con forma de barco, situado en la base del pulgar. Articula con radio, trapecio, trapezoide, capitado y semilunar. Es clave en la movilidad del carpo y suele fracturarse por caídas sobre la mano extendida.",
    "Hueso escafoides (R): Simétrico, estabiliza la articulación radiocarpiana y permite movimientos de flexión y desviación radial.",
    "Hueso semilunar (L): Central en la primera fila carpiana. Forma parte de la articulación de la muñeca junto con el radio y escafoides. Su forma semilunar facilita la flexión y extensión.",
    "Hueso semilunar (R): Simétrico, transmite fuerzas del carpo al antebrazo. Puede luxarse en movimientos forzados.",
    "Hueso piramidal (L): También llamado triquetral. Se articula con el pisiforme y semilunar. Situado medialmente, contribuye a la estabilidad ulnar del carpo.",
    "Hueso piramidal (R): Simétrico, proporciona soporte medial y limita la desviación cubital excesiva.",
    "Hueso pisiforme (L): Hueso sesamoideo pequeño, sobre el triquetral. Sirve de inserción para el flexor cubital del carpo y aumenta el brazo de palanca de este músculo.",
    "Hueso pisiforme (R): Simétrico, actúa como guía táctil del borde cubital de la muñeca.",
    "Hueso trapecio (L): Hueso cuadrangular, articulado con el primer metacarpiano. Permite la oposición del pulgar. Contiene surcos para tendones flexores.",
    "Hueso trapecio (R): Simétrico, esencial para la movilidad fina y la pinza pulgar-índice.",
    "Hueso trapezoide (L): Pequeño hueso en la segunda fila carpiana, entre trapecio y capitado. Refuerza la estabilidad del segundo metacarpiano.",
    "Hueso trapezoide (R): Simétrico, contribuye al arco rígido de la palma central.",
    "Hueso capitado (L): El mayor del carpo, con forma de cabeza. Articula con tercer metacarpiano y huesos vecinos. Eje central de rotación de la muñeca.",
    "Hueso capitado (R): Simétrico, transmite las cargas longitudinales hacia el radio.",
    "Hueso ganchoso (L): Ubicado medialmente, su apófisis en forma de gancho protege el canal de Guyon. Articula con cuarto y quinto metacarpiano.",
    "Hueso ganchoso (R): Simétrico, refuerza la parte medial de la palma y guía tendones flexores.",
    "Hueso lunado (L): Parte del complejo carpiano proximal, con forma de media luna. Conecta el radio y el capitado. Permite flexión y extensión de la muñeca.",
    "Hueso lunado (R): Simétrico, estabiliza el centro del carpo.",

    // --- METACARPIANOS ---
    "Cuarto hueso metacarpiano (L): Conecta el ganchoso con el cuarto dedo. Base cuadrada, cuerpo alargado y cabeza convexa. Participa en flexión y extensión del anular.",
    "Cuarto hueso metacarpiano (R): Simétrico, refuerza el arco palmar medial.",

    // --- FALANGES ---
    "Falange proximal del primer dedo de la mano (L): Base ancha que articula con el primer metacarpiano. Permite flexión y extensión del pulgar.",
    "Falange proximal del primer dedo de la mano (R): Simétrica, sostiene la movilidad principal del pulgar.",
    "Falange distal del primer dedo de la mano (L): Hueso terminal del pulgar, protege la uña y permite la pinza fina.",
    "Falange distal del primer dedo de la mano (R): Simétrica, da precisión al agarre.",
    "Falange proximal del segundo dedo de la mano (L): Articula con el segundo metacarpiano. Controla flexión-extensión del índice.",
    "Falange proximal del segundo dedo de la mano (R): Simétrica, elemento principal de la destreza digital.",
    "Falange media del segundo dedo de la mano (L): Une la proximal y distal, permitiendo precisión en la manipulación.",
    "Falange media del segundo dedo de la mano (R): Simétrica, guía el movimiento interfalángico.",
    "Falange distal del segundo dedo de la mano (L): Protege la yema digital.",
    "Falange distal del segundo dedo de la mano (R): Simétrica, punto de contacto en tareas finas.",
    "Falange proximal del tercer dedo de la mano (L): Se articula con el tercer metacarpiano. Soporta el eje medio de la mano.",
    "Falange proximal del tercer dedo de la mano (R): Simétrica, equilibra los movimientos de flexión.",
    "Falange media del tercer dedo de la mano (L): Puente óseo entre segmentos.",
    "Falange media del tercer dedo de la mano (R): Simétrica, coordina la extensión y cierre del puño.",
    "Falange distal del tercer dedo de la mano (L): Hueso terminal del dedo medio, sostiene la uña.",
    "Falange distal del tercer dedo de la mano (R): Simétrica, absorbe presión en la prensión.",
    "Falange proximal del cuarto dedo de la mano (L): Articula con el cuarto metacarpiano. Participa en cierre y apertura del anular.",
    "Falange proximal del cuarto dedo de la mano (R): Simétrica, estabiliza el arco palmar medial.",
    "Falange media del cuarto dedo de la mano (L): Intermedia, permite flexión-extensión precisa.",
    "Falange media del cuarto dedo de la mano (R): Simétrica, equilibra la movilidad del anular.",
    "Falange distal del cuarto dedo de la mano (L): Terminal, sostiene el extremo ungueal.",
    "Falange distal del cuarto dedo de la mano (R): Simétrica, distribuye presión táctil.",
    "Falange proximal del quinto dedo de la mano (L): Conecta con el quinto metacarpiano. Permite movimientos del meñique.",
    "Falange proximal del quinto dedo de la mano (R): Simétrica, otorga flexibilidad lateral a la palma.",
    "Falange media del quinto dedo de la mano (L): Une segmentos proximal y distal.",
    "Falange media del quinto dedo de la mano (R): Simétrica, facilita cierre de la mano.",
    "Falange distal del quinto dedo de la mano (L): Pequeña, terminal, protege la yema del meñique.",
    "Falange distal del quinto dedo de la mano (R): Simétrica, amortigua presión en la prensión lateral."
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
