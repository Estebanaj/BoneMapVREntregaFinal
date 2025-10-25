using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuColumnaController : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Text numeroTexto;       // Texto para "PASO 1", "PASO 2", etc.
    public TMP_Text tituloTexto;       // Texto para el nombre del hueso
    public TMP_Text descripcionTexto;  // Texto para la descripción médica
    public Button botonAtras;
    public Button botonAdelante;

    private int pasoActual = 0;
    private int totalPasos = 28;
    private string[] nombres;
    private string[] descripciones;

    private bool inicializado = false;

    void Awake()
    {
        InicializarDatos();
    }

    void OnEnable()
    {
        if (!inicializado)
        {
            InicializarDatos();
            inicializado = true;
        }

        ResetMenu();

        botonAtras.onClick.RemoveAllListeners();
        botonAdelante.onClick.RemoveAllListeners();

        botonAtras.onClick.AddListener(PasoAnterior);
        botonAdelante.onClick.AddListener(PasoSiguiente);
    }

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
            "Primera vértebra cervical, carece de cuerpo y apófisis espinosa. Su forma de anillo permite sostener el cráneo mediante las cavidades articulares superiores donde encaja el cóndilo occipital. Facilita los movimientos de asentir (flexión-extensión). Posee arcos anterior y posterior, tubérculos y masas laterales robustas. Estabiliza el paso de médula espinal y arterias vertebrales hacia el encéfalo.",
            "Segunda vértebra cervical con la apófisis odontoides (dentiforme), que se articula con el atlas permitiendo rotación lateral de la cabeza (“decir no”). Su cuerpo es fuerte, con carillas articulares planas y un arco vertebral que protege el conducto medular. Es eje de rotación cervical y base de equilibrio craneocervical.",
            "Pequeña, con cuerpo ovalado y apófisis espinosa bífida. Soporta peso craneal y transmite fuerzas a vértebras inferiores. Su foramen vertebral amplio protege médula espinal. Las apófisis transversas presentan agujeros para el paso de arterias vertebrales. Contribuye a flexión y ligera rotación cervical.",
            "Estructura similar a C3, pero con apófisis más cortas y carillas articulares orientadas oblicuamente, facilitando el movimiento combinado de inclinación y rotación. Interviene en la estabilidad cervical media. Su morfología permite la inserción de músculos escalenos y ligamentos longitudinales.",
            "Presenta cuerpo ligeramente mayor y apófisis espinosa bífida prominente. Canal vertebral aún amplio. Conecta la movilidad superior con la estabilidad inferior del cuello. Sirve de anclaje para músculos profundos del cuello como el longísimo y semiespinoso.",
            "Reconocida por su tubérculo anterior prominente (tubérculo carotídeo) usado como punto clínico para comprimir la arteria carótida. Su apófisis espinosa es corta, el cuerpo más ancho, y las carillas superiores permiten flexión cervical controlada.",
            "Conocida como “vértebra prominente”. Su apófisis espinosa larga y palpable marca el límite entre columna cervical y torácica. Posee cuerpo robusto, canal más estrecho y menor movilidad. Actúa como transición hacia la rigidez torácica.",
            "Primera torácica, une el cuello con el tórax. Tiene cuerpo grande y carillas costales para la primera costilla. Su apófisis espinosa se inclina hacia abajo. Estabiliza inicio de la caja torácica y protege médula torácica superior.",
            "Cuerpo intermedio con carillas costales superiores e inferiores. Limita rotación cervical y permite cierta flexión lateral. Conecta costillas 2 y 3. Su orientación articular favorece rigidez torácica.",
            "Cuerpo más circular y apófisis espinosa larga. Ayuda a mantener alineación del eje torácico. Punto de inserción de músculos interespinosos y trapecio. Relacionada con la escápula a nivel anatómico posterior.",
            "Vértebra media torácica. Su cuerpo más plano soporta presión del arco costal. Canal vertebral estrecho; movilidad limitada. Transmite cargas hacia porción inferior del tórax y estabiliza la caja costal.",
            "Cuerpo rectangular, con carillas para la quinta costilla. Apófisis espinosa inclinada oblicuamente. Mantiene curvatura fisiológica torácica y equilibrio respiratorio.",
            "Eje central del tórax, importante para rigidez axial. Las carillas costales articulan con la sexta costilla. Su posición equidistante permite flexión mínima y movimiento respiratorio controlado.",
            "Sirve como referencia anatómica de la escápula. Apófisis larga, proyectada inferiormente. Permite la inserción de músculos dorsales profundos y ligamentos interespinosos que sostienen el tronco.",
            "Transicional hacia la región inferior torácica. Carillas costales más grandes, cuerpo más voluminoso. Sostiene la presión mecánica del tórax medio y contribuye a la curvatura cifótica.",
            "Cuerpo ancho, carilla costal única superior. Articula con la novena costilla. Menor flexibilidad, pero soporte axial firme. Ayuda a transferir cargas hacia T10-T12 y región lumbar.",
            "Posee una sola carilla completa para la décima costilla. Sus apófisis articulares están más verticales. Participa en la transición mecánica hacia región toracolumbar.",
            "Carece de carilla costal inferior. Permite unión con costilla flotante 11. Estructura más robusta. Es punto de inflexión entre estabilidad torácica y movilidad lumbar.",
            "Última torácica. Articula con costilla 12 y con L1. Carillas articulares inferiores orientadas sagitalmente, iniciando movimiento lumbar. Transfiere peso torácico al sector lumbar.",
            "Primera lumbar, de cuerpo macizo y apófisis cuadradas. Permite flexión-extensión amplias. Su canal vertebral amplio protege el cono medular. Soporta peso axial elevado.",
            "Cuerpo voluminoso, superficie superior cóncava. Contribuye a la lordosis lumbar. Movilidad limitada en rotación, amplia en flexión. Inserción de músculos erectores espinales.",
            "Centro de la lordosis lumbar. Su cuerpo resiste gran compresión. Apófisis transversas largas para anclaje de músculos psoas y cuadrado lumbar. Equilibrio postural clave.",
            "Cuerpo grueso y robusto, bisagra de carga lumbosacra. Permite ligera flexión-extensión. Punto común de hernias discales. Transmite peso hacia L5 y sacro.",
            "Mayor de todas las vértebras móviles. Su cuerpo masivo soporta toda la presión axial. Articula con el sacro formando el ángulo lumbosacro. Crítica para estabilidad pélvica.",
            "Formado por fusión de cinco vértebras. Triangular, situado entre huesos coxales. Transmite peso corporal al anillo pélvico. Su cara anterior cóncava forma la pared posterior de la pelvis. Canal sacro protege terminaciones nerviosas.",
            "Remanente vestigial de la cola. Pequeño hueso triangular de tres a cinco vértebras fusionadas. Proporciona inserción a músculos del piso pélvico (coccígeo, elevador del ano). Soporte postural al sentarse.",
            "Mitad izquierda del anillo pélvico. Fusiona ilion, isquion y pubis. Sostiene peso desde el sacro hacia extremidad inferior. Aloja acetábulo para cabeza femoral. Protege vísceras pélvicas y define contorno lateral.",
            "Mitad derecha de la pelvis. Igual estructura que el izquierdo. Su articulación anterior (sínfisis púbica) y posterior (sacroilíaca) conforman la estabilidad pélvica. Distribuye carga simétrica y equilibra el movimiento de cadera."
        };
    }

    void PasoSiguiente()
    {
        if (pasoActual < totalPasos - 1)
        {
            pasoActual++;
            ActualizarPaso();
        }
    }

    void PasoAnterior()
    {
        if (pasoActual > 0)
        {
            pasoActual--;
            ActualizarPaso();
        }
    }

    void ActualizarPaso()
    {
        numeroTexto.text = "PASO " + (pasoActual + 1);
        tituloTexto.text = nombres[pasoActual];
        descripcionTexto.text = descripciones[pasoActual];
    }

    public void ResetMenu()
    {
        pasoActual = 0;
        ActualizarPaso();
    }
}
