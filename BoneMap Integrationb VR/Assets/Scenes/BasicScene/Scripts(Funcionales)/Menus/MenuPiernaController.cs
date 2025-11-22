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
    "Fíbula (R)", "Hueso cuboide (L)", "Hueso cuboide (R)", "Hueso cuneiforme intermedio (L)",
    "Hueso cuneiforme intermedio (R)", "Hueso cuneiforme lateral (L)", "Hueso cuneiforme lateral (R)",
    "Hueso cuneiforme medial (L)", "Hueso cuneiforme medial (R)", "Hueso Fémur (L)", "Hueso Fémur (R)",
    "Hueso navicular (L)", "Hueso navicular (R)", "Hueso Talo (L)", "Hueso Talo (R)", "Patela (L)",
    "Patela (R)", "Primer hueso metatarsiano (L)", "Primer hueso metatarsiano (R)",
    "Quinto hueso metatarsiano (L)", "Quinto hueso metatarsiano (R)", "Segundo hueso metatarsiano (L)",
    "Segundo hueso metatarsiano (R)", "Tercer hueso metatarsiano (L)", "Tercer hueso metatarsiano (R)",
    "Tibia (L)", "Tibia (R)", "Fíbula (L)", "Cuarto hueso metatarsiano (L)", "Cuarto hueso metatarsiano (R)",
    "Falange proximal del cuarto dedo del pie (L)", "Falange proximal del cuarto dedo del pie (R)",
    "Falange proximal del primer dedo del pie (L)", "Falange proximal del primer dedo del pie (R)",
    "Falange proximal del quinto dedo del pie (L)", "Falange proximal del quinto dedo del pie (R)",
    "Falange proximal del segundo dedo del pie (L)", "Falange proximal del segundo dedo del pie (R)",
    "Falange proximal del tercer dedo del pie (L)", "Falange proximal del tercer dedo del pie (R)",
    "Falange media del cuarto dedo del pie (L)", "Falange media del cuarto dedo del pie (R)",
    "Falange media del quinto dedo del pie (L)", "Falange media del quinto dedo del pie (R)",
    "Falange media del segundo dedo del pie (L)", "Falange media del segundo dedo del pie (R)",
    "Falange media del tercer dedo del pie (L)", "Falange media del tercer dedo del pie (R)",
    "Falange distal del cuarto dedo del pie (L)", "Falange distal del cuarto dedo del pie (R)",
    "Falange distal del primer dedo del pie (L)", "Falange distal del primer dedo del pie (R)",
    "Falange distal del quinto dedo del pie (L)", "Falange distal del quinto dedo del pie (R)",
    "Falange distal del segundo dedo del pie (L)", "Falange distal del segundo dedo del pie (R)",
    "Falange distal del tercer dedo del pie (R)", "Falange distal del tercer dedo del pie (L)",
    "Calcáneo (L)", "Calcáneo (R)"
        };


        // (Tu bloque de descripciones completo se mantiene)
        descripciones = new string[]
        {
            "Hueso largo ubicado en la parte lateral de la pierna, entre la rodilla y el tobillo. Actúa como soporte estructural y facilita el movimiento.",
            "Hueso cuboide ubicado en el pie izquierdo, parte de los huesos del medio pie. Ayuda a proporcionar estabilidad y facilita el movimiento del pie.",
            "Hueso cuboide ubicado en el pie derecho, parte de los huesos del medio pie. Ayuda a proporcionar estabilidad y facilita el movimiento del pie.",
            "Hueso cuneiforme intermedio ubicado en el pie izquierdo, una de las tres piezas que forman el medio pie y ayudan a la distribución del peso corporal.",
            "Hueso cuneiforme intermedio ubicado en el pie derecho, una de las tres piezas que forman el medio pie y ayudan a la distribución del peso corporal.",
            "Hueso cuneiforme lateral ubicado en el pie izquierdo, contribuye a la estructura del arco del pie y ayuda en la movilidad.",
            "Hueso cuneiforme lateral ubicado en el pie derecho, contribuye a la estructura del arco del pie y ayuda en la movilidad.",
            "Hueso cuneiforme medial ubicado en el pie izquierdo, una de las piezas que forman el medio pie y ayudan en la distribución del peso durante el caminar.",
            "Hueso cuneiforme medial ubicado en el pie derecho, una de las piezas que forman el medio pie y ayudan en la distribución del peso durante el caminar.",
            "Hueso largo ubicado en la parte superior de la pierna, conecta la pelvis con la rodilla y soporta la mayor parte del peso corporal.",
            "Hueso largo ubicado en la parte superior de la pierna, conecta la pelvis con la rodilla y soporta la mayor parte del peso corporal.",
            "Hueso en el pie izquierdo que conecta el tobillo con el pie. Es esencial para el soporte y el movimiento del pie.",
            "Hueso en el pie derecho que conecta el tobillo con el pie. Es esencial para el soporte y el movimiento del pie.",
            "Hueso en la parte superior del pie izquierdo, parte de los huesos que conforman la estructura del pie y facilitan el movimiento.",
            "Hueso en la parte superior del pie derecho, parte de los huesos que conforman la estructura del pie y facilitan el movimiento.",
            "Hueso talo izquierdo, forma parte de la articulación del tobillo y se encarga de transmitir el peso del cuerpo hacia el pie.",
            "Hueso talo derecho, forma parte de la articulación del tobillo y se encarga de transmitir el peso del cuerpo hacia el pie.",
            "Hueso redondeado ubicado en el centro de la rodilla, permite el movimiento de la pierna al facilitar la articulación entre el fémur y la tibia.",
            "Hueso redondeado ubicado en el centro de la rodilla, permite el movimiento de la pierna al facilitar la articulación entre el fémur y la tibia.",
            "Hueso metatarsiano en el pie izquierdo, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie derecho, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie izquierdo, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie derecho, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie izquierdo, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie derecho, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie izquierdo, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso metatarsiano en el pie derecho, forma parte de la parte media del pie y es importante para la estabilidad y el movimiento.",
            "Hueso largo ubicado en el lado interior de la pierna, forma parte del esqueleto de la pierna y facilita el movimiento.",
            "Hueso largo ubicado en el lado exterior de la pierna, forma parte del esqueleto de la pierna y facilita el movimiento.",
            "Hueso metatarsiano en el pie izquierdo, importante para el soporte del cuerpo y la distribución del peso durante la caminata.",
            "Hueso metatarsiano en el pie derecho, importante para el soporte del cuerpo y la distribución del peso durante la caminata.",
            "Hueso metatarsiano en el pie izquierdo, importante para el soporte del cuerpo y la distribución del peso durante la caminata.",
            "Hueso metatarsiano en el pie derecho, importante para el soporte del cuerpo y la distribución del peso durante la caminata.",
            "Falange proximal del cuarto dedo del pie izquierdo, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del cuarto dedo del pie derecho, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del primer dedo del pie izquierdo, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del primer dedo del pie derecho, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del quinto dedo del pie izquierdo, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del quinto dedo del pie derecho, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del segundo dedo del pie izquierdo, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del segundo dedo del pie derecho, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del tercer dedo del pie izquierdo, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange proximal del tercer dedo del pie derecho, permite el movimiento del dedo y contribuye a la función de la marcha.",
            "Falange media del cuarto dedo del pie izquierdo, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del cuarto dedo del pie derecho, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del quinto dedo del pie izquierdo, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del quinto dedo del pie derecho, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del segundo dedo del pie izquierdo, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del segundo dedo del pie derecho, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del tercer dedo del pie izquierdo, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange media del tercer dedo del pie derecho, contribuye a la flexión y extensión del dedo para la marcha.",
            "Falange distal del cuarto dedo del pie izquierdo, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del cuarto dedo del pie derecho, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del primer dedo del pie izquierdo, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del primer dedo del pie derecho, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del quinto dedo del pie izquierdo, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del quinto dedo del pie derecho, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del segundo dedo del pie izquierdo, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del segundo dedo del pie derecho, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del tercer dedo del pie derecho, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Falange distal del tercer dedo del pie izquierdo, permite la última parte de la flexión y extensión del dedo para la marcha.",
            "Hueso del talón izquierdo, el más grande y fuerte de los huesos del pie, esencial para la marcha y el soporte del peso del cuerpo.",
            "Hueso del talón derecho, el más grande y fuerte de los huesos del pie, esencial para la marcha y el soporte del peso del cuerpo."
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
