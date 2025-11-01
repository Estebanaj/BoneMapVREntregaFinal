using UnityEngine;
using TMPro;

public class MenuPiernaController : MonoBehaviour
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
    private void InicializarDatos()
    {
        nombres = new string[]
        {
            "Fémur (L)", "Fémur (R)",
            "Tibia (L)", "Tibia (R)",
            "Fíbula (L)", "Fíbula (R)",
            "Rótula (L)", "Rótula (R)",
            "Hueso talo (L)", "Hueso talo (R)",
            "Calcáneo (L)", "Calcáneo (R)",
            "Hueso navicular (L)", "Hueso navicular (R)",
            "Hueso cuboide (L)", "Hueso cuboide (R)",
            "Hueso cuneiforme medial (L)", "Hueso cuneiforme medial (R)",
            "Hueso cuneiforme intermedio (L)", "Hueso cuneiforme intermedio (R)",
            "Hueso cuneiforme lateral (L)", "Hueso cuneiforme lateral (R)",
            "Primer hueso metatarsiano (L)", "Primer hueso metatarsiano (R)",
            "Segundo hueso metatarsiano (L)", "Segundo hueso metatarsiano (R)",
            "Tercer hueso metatarsiano (L)", "Tercer hueso metatarsiano (R)",
            "Cuarto hueso metatarsiano (L)", "Cuarto hueso metatarsiano (R)",
            "Quinto hueso metatarsiano (L)", "Quinto hueso metatarsiano (R)",
            "Falange proximal del primer dedo del pie (L)", "Falange proximal del primer dedo del pie (R)",
            "Falange distal del primer dedo del pie (L)", "Falange distal del primer dedo del pie (R)",
            "Falange proximal del segundo dedo del pie (L)", "Falange proximal del segundo dedo del pie (R)",
            "Falange media del segundo dedo del pie (L)", "Falange media del segundo dedo del pie (R)",
            "Falange distal del segundo dedo del pie (L)", "Falange distal del segundo dedo del pie (R)",
            "Falange proximal del tercer dedo del pie (L)", "Falange proximal del tercer dedo del pie (R)",
            "Falange media del tercer dedo del pie (L)", "Falange media del tercer dedo del pie (R)",
            "Falange distal del tercer dedo del pie (L)", "Falange distal del tercer dedo del pie (R)",
            "Falange proximal del cuarto dedo del pie (L)", "Falange proximal del cuarto dedo del pie (R)",
            "Falange media del cuarto dedo del pie (L)", "Falange media del cuarto dedo del pie (R)",
            "Falange distal del cuarto dedo del pie (L)", "Falange distal del cuarto dedo del pie (R)",
            "Falange proximal del quinto dedo del pie (L)", "Falange proximal del quinto dedo del pie (R)",
            "Falange media del quinto dedo del pie (L)", "Falange media del quinto dedo del pie (R)",
            "Falange distal del quinto dedo del pie (L)", "Falange distal del quinto dedo del pie (R)"
        };

        // (Tu bloque de descripciones completo se mantiene)
        descripciones = new string[]
        {
    "Hueso más largo y resistente del cuerpo. Conecta la pelvis al esqueleto de la pierna. Su cabeza hemisférica articula con el acetábulo, transmitiendo el peso corporal. El cuello y los trocánteres permiten inserción muscular y movimiento de flexión, extensión y rotación. Su robusta diáfisis soporta cargas y finaliza en cóndilos que forman la articulación de la rodilla.",
    "Homólogo al izquierdo. Su estructura tubular densa garantiza la estabilidad axial. El cuello forma un ángulo oblicuo que favorece la bipedestación. Los cóndilos medial y lateral se adaptan al platillo tibial, permitiendo desplazamiento suave. Participa en locomoción, absorción de impactos y control postural.",
    "Hueso largo, medial, situado entre fémur y astrágalo. Su cuerpo triangular y borde anterior subcutáneo conforman la “espinilla”. Soporta el peso corporal y estabiliza la articulación de la rodilla. Presenta cóndilos superiores para el fémur y maléolo medial que refuerza el tobillo.",
    "Simétrica. Principal eje de carga de la pierna derecha. Sus superficies articulares planas reciben los cóndilos femorales y se unen al peroné lateralmente. Transmite la fuerza vertical al pie. El maléolo medial se proyecta distalmente, formando parte de la mortaja tibioastragalina.",
    "Hueso delgado y lateral, paralelo a la tibia. No participa significativamente en la carga, pero proporciona inserción a músculos peroneos y ligamentos del tobillo. El extremo distal forma el maléolo lateral, esencial para la estabilidad articular.",
    "Estructura semejante a la izquierda. Mantiene la integridad del compartimento lateral y equilibra las tensiones de la pierna. Su cabeza proximal se articula con la tibia y el maléolo lateral protege el tobillo derecho.",
    "Hueso sesamoideo situado en el tendón del cuádriceps. Aumenta la eficiencia mecánica del músculo en la extensión de rodilla. Su cara posterior articula con la tróclea femoral. Protege la articulación anterior y distribuye fuerzas.",
    "Simétrica. Facilita el deslizamiento entre fémur y tendón rotuliano durante la flexión-extensión. Absorbe tensiones de compresión y mantiene alineación del aparato extensor.",
    "También llamado astrágalo, transmite el peso corporal desde la tibia hacia el pie. Carece de inserciones musculares, lo que le da gran movilidad. Su tróclea superior articula con tibia y fíbula; el cuello con el navicular; y su cara inferior con el calcáneo. Es pieza clave del tobillo y del equilibrio postural.",
    "Homólogo del izquierdo. Actúa como eje de la articulación tibiotarsiana, permitiendo flexión plantar y dorsal. Sus superficies articulares lisas distribuyen cargas y amortiguan impactos. Participa en la estabilidad dinámica del arco medial del pie.",
    "Hueso del talón y el más voluminoso del tarso. Sostiene el peso corporal y sirve de punto de inserción al tendón de Aquiles. Su cara superior se articula con el astrágalo y la anterior con el cuboide. Absorbe impactos durante la marcha y mantiene la curvatura plantar posterior.",
    "Simétrico, conforma la base posterior del pie derecho. Disipa fuerzas durante la fase de apoyo y facilita la propulsión al caminar. Su tuberosidad posterior es prominente y su estructura esponjosa contribuye a la amortiguación.",
    "Situado en el lado medial del pie, entre el talo y las cuñas. Su superficie cóncava recibe la cabeza del astrágalo. Posee una tuberosidad palpable que da inserción al tendón tibial posterior. Es esencial en el soporte del arco longitudinal medial.",
    "Simétrico, estabiliza el mediopié y conecta el astrágalo con los cuneiformes. Su forma de barca distribuye la presión entre los componentes del arco plantar.",
    "Ubicado en el lado lateral del pie, entre calcáneo y cuarto-quinto metatarsiano. Su surco inferior guía el tendón del peroneo largo. Aporta rigidez lateral y sirve como punto de palanca durante la propulsión.",
    "Simétrico, forma parte del arco plantar lateral. Estabiliza el borde externo del pie y distribuye fuerzas entre talón y metatarso.",
    "Más grande de las tres cuñas, se articula con el primer metatarsiano y el navicular. Ayuda al soporte del arco medial del pie y da inserción al tendón tibial anterior.",
    "Simétrico, estabiliza el primer radio del pie. Su orientación permite movimientos controlados del hallux durante la marcha.",
    "Entre el medial y el lateral. Se articula con el segundo metatarsiano y el navicular. Su forma en cuña refuerza el arco transversal del pie.",
    "Simétrico, participa en la transmisión de cargas al eje central del antepié. Proporciona rigidez estructural.",
    "El más pequeño de las tres cuñas. Conecta el cuboide con el tercer metatarsiano. Contribuye al arco transversal y la estabilidad lateral.",
    "Simétrico, sirve de base al tercer metatarsiano y equilibra la movilidad del mediopié.",
    "Corto y robusto, conecta el cuneiforme medial con el hallux. Soporta gran parte del peso durante la propulsión del paso. Su base ancha estabiliza el mediopié y su cabeza articula con la falange proximal del primer dedo. Inserta los músculos peroneo largo y tibial anterior, esenciales para la flexión plantar y dorsal.",
    "Homólogo al izquierdo. Transmite las fuerzas de impulso hacia el hallux durante la marcha. Su estructura gruesa absorbe la presión axial. Presenta sesamoideos plantares que mejoran la palanca del flexor del dedo gordo.",
    "El más largo del pie. Articula proximalmente con los tres cuneiformes y distalmente con la segunda falange. Es el eje central del antepié, confiriendo estabilidad al arco transversal. Resiste cargas verticales y limita movimientos excesivos laterales.",
    "Simétrico, actúa como pivote estructural del pie. Soporta compresión durante el apoyo medio de la marcha. Su base profunda lo hace menos móvil pero más resistente.",
    "Ubicado entre el segundo y cuarto metatarsiano. Articula con el cuneiforme lateral y las falanges del tercer dedo. Contribuye al equilibrio dinámico y al soporte del arco longitudinal.",
    "Simétrico, refuerza la estructura central del antepié. Permite flexión y extensión moderadas del tercer dedo.",
    "Articula con el cuboide y con el quinto metatarsiano. Su base alargada soporta las cargas laterales y proporciona elasticidad en la fase de empuje del paso.",
    "Simétrico, conecta el cuboide con el cuarto dedo. Participa en la distribución del peso y el movimiento coordinado del pie.",
    "Presenta una apófisis estiloides prominente donde se inserta el tendón del peroneo corto. Facilita la eversión del pie. Es hueso de palanca durante la propulsión.",
    "Simétrico, refuerza el borde lateral del pie y participa activamente en la estabilidad del arco externo. Es común sitio de fractura por torsión o impacto.",
    "Corta y robusta, articula con el primer metatarsiano. Permite flexión plantar y extensión del hallux. Soporta el impulso final durante la marcha y equilibra la carga axial del cuerpo.",
    "Simétrica. Su base ovalada transmite la fuerza del metatarsiano hacia la falange distal. Participa en la estabilidad del arco medial y la propulsión.",
    "Hueso pequeño, aplanado, termina en la zona ungueal. Recibe presión directa en el despegue del paso.",
    "Simétrica, protege la punta del hallux y distribuye la fuerza del impulso.",
    "Alargada, se articula con el segundo metatarsiano. Permite flexión y extensión.",
    "Simétrica, eje de movimiento del antepié medio.",
    "Une la proximal y distal, da estabilidad articular.",
    "Simétrica, contribuye a la flexión plantar y control digital.",
    "Pequeña, soporta presión terminal.",
    "Simétrica, amortigua el contacto con el suelo.",
    "Articula con el tercer metatarsiano. Proporciona flexibilidad al arco medio.",
    "Simétrica, controla la elevación del antepié.",
    "Intermedia, une la proximal y distal.",
    "Simétrica, facilita la extensión digital.",
    "Terminal, sostiene la uña y estabiliza el apoyo.",
    "Simétrica, protege el extremo del dedo.",
    "Articula con el cuarto metatarsiano. Permite movilidad lateral controlada.",
    "Simétrica, contribuye al ajuste del arco plantar.",
    "Une segmentos, mantiene alineación digital.",
    "Simétrica, ayuda en la flexión plantar.",
    "Pequeña, estabiliza la punta del dedo.",
    "Simétrica, distribuye presión al caminar.",
    "Se articula con el quinto metatarsiano. Da soporte lateral y ayuda en la propulsión.",
    "Simétrica, mantiene equilibrio del borde externo del pie.",
    "Segmento intermedio, une falanges.",
    "Simétrica, colabora en la flexión plantar.",
    "Pequeña, terminal, protege el extremo del meñique.",
    "Simétrica, absorbe impactos laterales."
        };


        Debug.Log($"[MenuPiernaController] Datos cargados: {nombres.Length} nombres y {descripciones.Length} descripciones.");
    }

    // ----------------------------------------------------
    private void CapturarIniciales()
    {
        if (huesos == null || huesos.Length == 0)
        {
            Debug.LogWarning("[MenuPiernaController] No hay huesos asignados en el Inspector.");
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
                    Debug.Log($"[MenuPiernaController] Mostrando info de: {nombres[i]}");
                }
                else
                {
                    Debug.LogWarning($"[MenuPiernaController] Índice fuera de rango en {name}: {i}");
                }

                break;
            }
        }

        if (!encontrado)
            Debug.LogWarning($"[MenuPiernaController] El hueso '{huesoSeleccionado.name}' no está en la lista.");
    }

    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona un hueso";
        if (descripcionTexto) descripcionTexto.text = "Agarra un hueso para ver su descripción médica.";
    }
}
