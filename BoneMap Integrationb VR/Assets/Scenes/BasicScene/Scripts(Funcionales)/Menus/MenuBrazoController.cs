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
    "Cuarto hueso metacarpiamo (L)", "Cuarto hueso metacarpiamo (R)",
    "Falange distal del cuarta dedo de la mano (R)","Falange distal del primer dedo de la mano (L)",
    "Falange distal del quinto dedo de la mano (R)", "Falange distal del segundo dedo de la mano (L)",
    "Falange distal del segundo dedo de la mano (R)", "Falange distal del tercer dedo de la mano (L)",
    "Falange distal del tercer dedo de la mano (R)", 
    "Falange media del cuarto dedo de la mano (L)", "Falange media del cuarto dedo de la mano (R)",
    "Falange media del quinto dedo de la mano (L)", "Falange media del quinto dedo de la mano (R)",
    "Falange media del segundo dedo de la mano (L)", "Falange media del segundo dedo de la mano (R)",
    "Falange media del tercer dedo de la mano (L)", "Falange media del tercer dedo de la mano (R)",
    "Falange proximal del cuarto dedo de la mano (L)", "Falange proximal del cuarto dedo de la mano (R)",
    "Falange proximal del primer dedo de la mano (L)", "Falange proximal del primer dedo de la mano (R)",
    "Falange proximal del quinto dedo de la mano (L)", "Falange proximal del quinto dedo de la mano (R)",
    "Falange proximal del segundo dedo de la mano (L)", "Falange proximal del segundo dedo de la mano (R)",
    "Falange proximal del tercer dedo de la mano (L)", "Falange proximal del tercer dedo de la mano (R)",
    "Hueso capitado (L)", "Hueso capitado (R)", "Hueso escafoides (L)",   "Hueso escafoides (R)", "Hueso hamatal (L)", "Hueso hamatal (R)",
    "Hueso lunado (L)", "Hueso lunado (R)", "Hueso pisiforme (L)",
    "Hueso pisiforme (R)", "Hueso trapezio (L)", "Hueso trapezio (R)",
    "Hueso trapezoide (L)", "Hueso trapezoide (R)", "Hueso triquetral (L)",
    "Hueso triquetral (R)", "Húmero (L)", "Húmero (R)", "Primer hueso metacarpiamo (L)",
    "Primer hueso metacarpiamo (R)","Quinto hueso metacarpiamo (L)" ,"Quinto hueso metacarpiamo (R)",
    "Radio (L)", "Radio (R)", "Segundo hueso metacarpiamo (L)",
    "Segundo hueso metacarpiamo (R)","Tercer hueso metacarpiamo (L)" ,"Tercer hueso metacarpiamo (R)",
    "Ulna (L)", "Ulna (R)"
        };


        descripciones = new string[]
        {
     "Hueso metacarpiano ubicado en la mano izquierda, situado entre la muñeca y los dedos. Forma parte de la estructura de la palma de la mano.",
    "Hueso metacarpiano ubicado en la mano derecha, situado entre la muñeca y los dedos. Forma parte de la estructura de la palma de la mano.",
    "Falange distal del cuarto dedo de la mano derecha, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange distal del primer dedo de la mano izquierda, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange distal del quinto dedo de la mano derecha, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange distal del segundo dedo de la mano izquierda, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange distal del segundo dedo de la mano derecha, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange distal del tercer dedo de la mano izquierda, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange distal del tercer dedo de la mano derecha, ubicada al final del dedo, permite la flexión y extensión.",
    "Falange media del cuarto dedo de la mano izquierda, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del cuarto dedo de la mano derecha, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del quinto dedo de la mano izquierda, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del quinto dedo de la mano derecha, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del segundo dedo de la mano izquierda, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del segundo dedo de la mano derecha, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del tercer dedo de la mano izquierda, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange media del tercer dedo de la mano derecha, ubicada entre la falange proximal y distal, permite la flexión y extensión del dedo.",
    "Falange proximal del cuarto dedo de la mano izquierda, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del cuarto dedo de la mano derecha, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del primer dedo de la mano izquierda, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del primer dedo de la mano derecha, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del quinto dedo de la mano izquierda, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del quinto dedo de la mano derecha, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del segundo dedo de la mano izquierda, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del segundo dedo de la mano derecha, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del tercer dedo de la mano izquierda, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Falange proximal del tercer dedo de la mano derecha, ubicada en la base del dedo, permite el movimiento y flexión de este.",
    "Hueso capitado ubicado en la muñeca izquierda, conecta con los huesos de la mano y forma parte de la estructura carpiana.",
    "Hueso capitado ubicado en la muñeca derecha, conecta con los huesos de la mano y forma parte de la estructura carpiana.",
    "Hueso escafoides ubicado en la muñeca izquierda, conecta con los huesos de la mano y facilita el movimiento de la muñeca.",
    "Hueso escafoides ubicado en la muñeca derecha, conecta con los huesos de la mano y facilita el movimiento de la muñeca.",
    "Hueso hamatal ubicado en la muñeca izquierda, conecta con los huesos de la mano y forma parte de la estructura carpiana.",
    "Hueso hamatal ubicado en la muñeca derecha, conecta con los huesos de la mano y forma parte de la estructura carpiana.",
    "Hueso lunado ubicado en la muñeca izquierda, ayuda en el movimiento de la muñeca al conectar con otros huesos carpianos.",
    "Hueso lunado ubicado en la muñeca derecha, ayuda en el movimiento de la muñeca al conectar con otros huesos carpianos.",
    "Hueso pisiforme ubicado en la muñeca izquierda, pequeño y redondeado, se encuentra en el extremo del carpo.",
    "Hueso pisiforme ubicado en la muñeca derecha, pequeño y redondeado, se encuentra en el extremo del carpo.",
    "Hueso trapezio ubicado en la muñeca izquierda, ayuda a conectar el pulgar con la muñeca.",
    "Hueso trapezio ubicado en la muñeca derecha, ayuda a conectar el pulgar con la muñeca.",
    "Hueso trapezoide ubicado en la muñeca izquierda, facilita el movimiento de la muñeca y los dedos.",
    "Hueso trapezoide ubicado en la muñeca derecha, facilita el movimiento de la muñeca y los dedos.",
    "Hueso triquetral ubicado en la muñeca izquierda, forma parte de los huesos carpianos y facilita el movimiento de la muñeca.",
    "Hueso triquetral ubicado en la muñeca derecha, forma parte de los huesos carpianos y facilita el movimiento de la muñeca.",
    "Húmero ubicado en el brazo izquierdo, conecta el codo con el hombro, permitiendo el movimiento del brazo.",
    "Húmero ubicado en el brazo derecho, conecta el codo con el hombro, permitiendo el movimiento del brazo.",
    "Primer hueso metacarpiano ubicado en la mano izquierda, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Primer hueso metacarpiano ubicado en la mano derecha, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Quinto hueso metacarpiano ubicado en la mano izquierda, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Quinto hueso metacarpiano ubicado en la mano derecha, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Radio ubicado en el antebrazo izquierdo, conecta el codo con la muñeca, permitiendo el movimiento del antebrazo.",
    "Radio ubicado en el antebrazo derecho, conecta el codo con la muñeca, permitiendo el movimiento del antebrazo.",
    "Segundo hueso metacarpiano ubicado en la mano izquierda, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Segundo hueso metacarpiano ubicado en la mano derecha, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Tercer hueso metacarpiano ubicado en la mano izquierda, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Tercer hueso metacarpiano ubicado en la mano derecha, parte de la palma de la mano y facilita el movimiento de los dedos.",
    "Ulna ubicada en el antebrazo izquierdo, conecta el codo con la muñeca y facilita los movimientos del brazo.",
    "Ulna ubicada en el antebrazo derecho, conecta el codo con la muñeca y facilita los movimientos del brazo."
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
