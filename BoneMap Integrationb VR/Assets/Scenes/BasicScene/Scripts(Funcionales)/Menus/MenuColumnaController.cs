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
    "Atlas (C1)", "Axis (C2)", "Cócix", "Hueso coxal (L)", "Hueso coxal (R)",
    "Hueso sacro", "Vértebra C3", "Vértebra C4", "Vértebra C5", "Vértebra C6","Vértebra C7",
    "Vértebra L1", "Vértebra L2", "Vértebra L3", "Vértebra L4", "Vértebra L5",
    "Vértebra T1", "Vértebra T2", "Vértebra T3", "Vértebra T4", "Vértebra T5",
    "Vértebra T6", "Vértebra T7", "Vértebra T8", "Vértebra T9", "Vértebra T10",
    "Vértebra T11", "Vértebra T12"
    };


        descripciones = new string[]
        {
            "Primera vértebra cervical, carece de cuerpo y apófisis espinosa. Forma la articulación atlantooccipital y permite los movimientos de flexión-extensión de la cabeza.",
            "Segunda vértebra cervical; posee la apófisis odontoides (dens), que actúa como pivote para la rotación de la cabeza sobre C1.",
            "Estructura ósea al final de la columna vertebral formada por tres a cinco vértebras fusionadas. Sirve como punto de anclaje para músculos y ligamentos.",
            "Hueso grande y plano que forma la parte lateral y anterior de la pelvis. Proporciona soporte y protección a los órganos pélvicos.",
            "Hueso grande y plano que forma la parte lateral y anterior de la pelvis. Proporciona soporte y protección a los órganos pélvicos.",
            "Hueso triangular que se encuentra en la base de la columna vertebral. Conecta la columna con los huesos coxales para formar la pelvis.",
            "Tercera vértebra cervical, característica por tener una apófisis espinosa y un cuerpo vertebral más grande que las vértebras superiores.",
            "Cuarta vértebra cervical, con un cuerpo vertebral y apófisis espinosa que facilitan el movimiento del cuello.",
            "Quinta vértebra cervical, conocida por ser el nivel de las raíces nerviosas que controlan el movimiento del brazo.",
            "Sexta vértebra cervical, un poco más grande que las superiores, permite el movimiento de flexión y extensión del cuello.",
            "Séptima vértebra cervical, más prominente y a menudo palpable en la parte posterior del cuello.",
            "Primera vértebra lumbar, grande y robusta, diseñada para soportar el peso del cuerpo en la región lumbar.",
            "Segunda vértebra lumbar, continúa con la función de proporcionar soporte estructural en la región baja de la espalda.",
            "Tercera vértebra lumbar, sigue proporcionando soporte al torso y está involucrada en la flexión y extensión de la parte inferior de la espalda.",
            "Cuarta vértebra lumbar, una de las vértebras más grandes y fuertes que soporta la mayor parte del peso corporal.",
            "Quinta vértebra lumbar, la última vértebra lumbar que conecta con el sacro y ayuda a soportar la carga del cuerpo en la pelvis.",
            "Primera vértebra torácica, ubicada en la parte superior de la espalda, conecta con la clavícula y las costillas.",
            "Segunda vértebra torácica, también conecta con las costillas y ayuda a la movilidad de la parte media de la espalda.",
            "Tercera vértebra torácica, se encuentra en la zona media de la espalda y está asociada con las costillas.",
            "Cuarta vértebra torácica, situada en la parte media-alta de la espalda, también conecta con las costillas.",
            "Quinta vértebra torácica, en la parte superior de la espalda, permite movimientos limitados debido a su conexión con las costillas.",
            "Sexta vértebra torácica, forma parte de la región media de la espalda y está involucrada en la movilidad de la columna torácica.",
            "Séptima vértebra torácica, sigue siendo parte de la región torácica, entre la zona media y baja de la espalda.",
            "Octava vértebra torácica, ubicada en la zona baja de la parte media de la espalda, tiene un rol similar en la movilidad torácica.",
            "Novena vértebra torácica, conecta con las costillas en la parte baja de la espalda.",
            "Décima vértebra torácica, es la última de la parte alta de la columna torácica y conecta con las costillas inferiores.",
            "Undécima vértebra torácica, también parte de la zona baja de la columna torácica.",
            "Duodécima vértebra torácica, conecta con las últimas costillas y forma el límite entre la región torácica y lumbar."
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

    // ----------------------------------------------------
    public void ResetMenu()
    {
        if (tituloTexto) tituloTexto.text = "Selecciona un hueso";
        if (descripcionTexto) descripcionTexto.text = "Agarra un hueso para ver su descripción médica.";
    }
}
