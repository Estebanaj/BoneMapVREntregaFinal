using UnityEngine;
using TMPro;

public class MenuEscapulaController : MonoBehaviour
{
    [System.Serializable]
    public class HuesoInfo
    {
        [Header("Asignaciones")]
        public string etiqueta;              // Nombre del elemento anatómico
        public Transform hueso;              // Objeto 3D del hueso real
        public Transform destinoFantasma;    // Punto de encaje para el brillo

        [HideInInspector] public Vector3 posInicialWorld;
        [HideInInspector] public Quaternion rotInicialWorld;
        [HideInInspector] public bool inicialCapturada;
    }

    [Header("Referencias UI")]
    public TMP_Text tituloTexto;             // Nombre del hueso o estructura
    public TMP_Text descripcionTexto;        // Descripción médica

    [Header("Lista de Huesos / Cartílagos")]
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
    private void InicializarDatos()
    {
        nombres = new string[]
        {
            "Cartílago costal de la primera costilla (L)", "Cartílago costal de la primera costilla (R)",
            "Cartílago costal de la segunda costilla (L)", "Cartílago costal de la segunda costilla (R)",
            "Cartílago costal de la tercera costilla (L)", "Cartílago costal de la tercera costilla (R)",
            "Cartílago costal de la cuarta costilla (L)", "Cartílago costal de la cuarta costilla (R)",
            "Cartílago costal de la quinta costilla (L)", "Cartílago costal de la quinta costilla (R)",
            "Cartílago costal de la sexta costilla (L)", "Cartílago costal de la sexta costilla (R)",
            "Cartílago costal de la séptima costilla (L)", "Cartílago costal de la séptima costilla (R)",
            "Cartílago costal de la octava costilla (L)", "Cartílago costal de la octava costilla (R)",
            "Cartílago costal de la novena costilla (L)", "Cartílago costal de la novena costilla (R)",
            "Cartílago costal de la décima costilla (L)", "Cartílago costal de la décima costilla (R)",
            "Clavícula (L)", "Clavícula (R)",
            "Primera costilla (L)", "Primera costilla (R)",
            "Segunda costilla (L)", "Segunda costilla (R)",
            "Tercera costilla (L)", "Tercera costilla (R)",
            "Cuarta costilla (L)", "Cuarta costilla (R)",
            "Quinta costilla (L)", "Quinta costilla (R)",
            "Sexta costilla (L)", "Sexta costilla (R)",
            "Séptima costilla (L)", "Séptima costilla (R)",
            "Octava costilla (L)", "Octava costilla (R)",
            "Novena costilla (L)", "Novena costilla (R)",
            "Décima costilla (L)", "Décima costilla (R)",
            "Undécima costilla (L)", "Undécima costilla (R)",
            "Duodécima costilla (L)", "Duodécima costilla (R)",
            "Manubrio del esternón", "Cuerpo del esternón", "Proceso xifoides",
            "Escápula (L)", "Escápula (R)"
        };

        descripciones = new string[]
        {
    "Banda de cartílago hialino que conecta la primera costilla izquierda con el manubrio del esternón. Permite la expansión torácica y actúa como amortiguador entre hueso y esternón.",
    "Estructura simétrica que une la primera costilla derecha con el esternón. Firme, no segmentado, contribuye a la estabilidad de la caja torácica superior.",
    "Une la segunda costilla izquierda al ángulo esternal. Favorece la movilidad respiratoria mediante su elasticidad y unión fibrosa.",
    "Simétrico al izquierdo, sirve de punto anatómico para el conteo costal clínico. Aporta flexibilidad y soporte mecánico al tórax.",
    "Estructura cartilaginosa elástica que conecta la tercera costilla izquierda con el cuerpo esternal. Permite ligera movilidad y absorbe impactos respiratorios.",
    "Conecta costilla derecha con el esternón. Participa en el movimiento costal anterior durante la inspiración.",
    "Une la cuarta costilla izquierda con el esternón, participando en el ensanchamiento del tórax. Su tejido hialino le confiere flexibilidad.",
    "Simétrico al izquierdo. Facilita el ascenso costal en inspiración y el retorno elástico en espiración.",
    "Conecta la quinta costilla izquierda con el cuerpo del esternón. Estructura semiflexible esencial en la movilidad respiratoria media.",
    "Une costilla derecha con el esternón. Absorbe tensiones durante la expansión torácica.",
    "Une la sexta costilla izquierda con el esternón. Su flexibilidad favorece la ventilación pulmonar.",
    "Simétrico, une la sexta costilla derecha al esternón. Protege estructuras internas del mediastino.",
    "Se articula con el extremo inferior del esternón y el cartílago de la sexta costilla, completando el arco costal.",
    "Simétrico, forma parte del borde costal derecho. Determina el límite inferior del tórax.",
    "Se une al cartílago de la séptima costilla. Es parte de las costillas falsas, contribuyendo a la elasticidad del arco costal.",
    "Simétrico, se une indirectamente al esternón a través del cartílago superior.",
    "Articulación indirecta con el esternón mediante cartílago séptimo. Permite movilidad lateral del tórax.",
    "Simétrico, contribuye al borde costal derecho y protección hepática.",
    "Conecta la décima costilla izquierda al arco costal. Termina en unión fibrosa, no esternal.",
    "Simétrico, articula con novena costilla. Participa en flexión costal inferior.",
    "Hueso alargado en forma de “S”, une esternón con escápula izquierda. Actúa como puntal que separa el hombro del tórax. Protege vasos subclavios.",
    "Simétrica, conecta esternón con escápula derecha. Transmite fuerzas del miembro superior al tronco.",
    "Corta, plana y curva. Se articula con T1 y el manubrio esternal. Protege el plexo braquial y vasos subclavios.",
    "Simétrica, forma la base torácica superior. Importante en anatomía clínica del cuello.",
    "Más larga y delgada, con tubérculo de inserción para músculos serratos.",
    "Simétrica, articulación con T2 y cartílago costal del segundo espacio intercostal.",
    "Curva y estrecha, contribuye a la expansión torácica.",
    "Idéntica a la izquierda, participa en la flexión costal.",
    "De longitud media, curvatura pronunciada. Protege pulmones y grandes vasos.",
    "Simétrica, conecta con T4 y el esternón.",
    "Costilla típica, aporta rigidez media y movilidad.",
    "Simétrica, protege el corazón y arterias mamarias.",
    "Curva y robusta, punto de inserción muscular torácico.",
    "Simétrica, delimita el borde pulmonar inferior.",
    "Última verdadera, conecta directamente con el esternón.",
    "Simétrica, forma parte del límite torácico inferior.",
    "Falsa costilla, une su cartílago con el de la séptima.",
    "Simétrica, contribuye al arco costal.",
    "Forma parte de las costillas falsas, unida indirectamente al esternón.",
    "Similar, protege órganos abdominales superiores.",
    "Flotante o semimóvil, protege riñón izquierdo.",
    "Equivalente, resguarda el riñón derecho.",
    "Costilla flotante, sin conexión esternal. Protege órganos abdominales.",
    "Simétrica, anclaje muscular abdominal posterior.",
    "Corta, flotante, final de la parrilla costal. Inserta músculos lumbares.",
    "Simétrica, marca el límite posterior del tórax.",
    "Porción superior del esternón. Se articula con clavículas y primeras costillas. Protege grandes vasos torácicos.",
    "Parte media, larga y plana. Conecta con cartílagos costales 2–7. Centro estructural de la caja torácica.",
    "Extremo inferior del esternón, cartilaginoso en juventud, osificado en adultez. Punto de referencia en RCP y diafragma.",
    "Hueso plano triangular posterior al tórax. Se articula con clavícula y húmero. Permite movilidad del hombro y anclaje muscular dorsal.",
    "Simétrica, coordina movimientos escapulohumerales y protege la parrilla costal."
        };


        Debug.Log($"[MenuEscapulaController] Datos inicializados: {nombres.Length} nombres y {descripciones.Length} descripciones.");
    }

    // ----------------------------------------------------
    private void CapturarIniciales()
    {
        if (huesos == null || huesos.Length == 0)
        {
            Debug.LogWarning("[MenuEscapulaController] No hay huesos asignados en el Inspector.");
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
                    Debug.Log($"[MenuEscapulaController] Mostrando información de: {nombres[i]}");
                }
                else
                {
                    Debug.LogWarning($"[MenuEscapulaController] Índice fuera de rango ({i}) para {huesoSeleccionado.name}.");
                }

                break;
            }
        }

        if (!encontrado)
            Debug.LogWarning($"[MenuEscapulaController] Hueso no encontrado en la lista: {huesoSeleccionado.name}");
    }

    // ----------------------------------------------------
    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona una estructura ósea";
        if (descripcionTexto) descripcionTexto.text = "Agarra una pieza para ver su descripción médica.";
    }
}
