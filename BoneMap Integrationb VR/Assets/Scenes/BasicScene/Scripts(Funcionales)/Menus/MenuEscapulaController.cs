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
    "Cartílago costal de la cuarta costilla (L)", "Cartílago costal de la cuarta costilla (R)",
    "Cartílago costal de la décima costilla (L)", "Cartílago costal de la décima costilla (R)",
    "Cartílago costal de la novena costilla (L)", "Cartílago costal de la novena costilla (R)",
    "Cartílago costal de la octava costilla (L)", "Cartílago costal de la octava costilla (R)",
    "Cartílago costal de la primera costilla (L)", "Cartílago costal de la primera costilla (R)",
    "Cartílago costal de la quinta costilla (L)", "Cartílago costal de la quinta costilla (R)",
    "Cartílago costal de la segunda costilla (L)", "Cartílago costal de la segunda costilla (R)",
    "Cartílago costal de la sexta costilla (L)", "Cartílago costal de la sexta costilla (R)",
    "Cartílago costal de la séptima costilla (L)", "Cartílago costal de la séptima costilla (R)",
    "Cartílago costal de la tercera costilla (L)", "Cartílago costal de la tercera costilla (R)",
    "Clavícula (L)", "Clavícula (R)", "Cuarta costilla (L)", "Cuarta costilla (R)",
    "Cuerpo del esternón", "Duodécima costilla (L)", "Duodécima costilla (R)", 
    "Décima costilla (L)", "Décima costilla (R)", "Escápula (L)", "Escápula (R)",
    "Manubrio del esternón", "Novena costilla (L)", "Novena costilla (R)",
    "Octava costilla (L)", "Octava costilla (R)", "Primera costilla (L)", "Primera costilla (R)",
    "Proceso xifoides", "Quinta costilla (L)", "Quinta costilla (R)", "Segunda costilla (L)",
    "Segunda costilla (R)", "Sexta costilla (L)", "Sexta costilla (R)", "Séptima costilla (L)",
    "Séptima costilla (R)", "Tercera costilla (L)", "Tercera costilla (R)", "Undécima costilla (L)",
    "Undécima costilla (R)"
};


        descripciones = new string[]
        {
    "Cartílago costal que conecta la cuarta costilla izquierda con el esternón. Proporciona flexibilidad a la caja torácica para la expansión durante la respiración.",
    "Cartílago costal que conecta la cuarta costilla derecha con el esternón. Proporciona flexibilidad a la caja torácica para la expansión durante la respiración.",
    "Cartílago costal que conecta la décima costilla izquierda con el esternón. Facilita la expansión torácica en la respiración.",
    "Cartílago costal que conecta la décima costilla derecha con el esternón. Facilita la expansión torácica en la respiración.",
    "Cartílago costal que conecta la novena costilla izquierda con el esternón. Ayuda a la flexibilidad y expansión de la caja torácica.",
    "Cartílago costal que conecta la novena costilla derecha con el esternón. Ayuda a la flexibilidad y expansión de la caja torácica.",
    "Cartílago costal que conecta la octava costilla izquierda con el esternón. Facilita la expansión torácica durante la respiración.",
    "Cartílago costal que conecta la octava costilla derecha con el esternón. Facilita la expansión torácica durante la respiración.",
    "Cartílago costal que conecta la primera costilla izquierda con el esternón. Proporciona flexibilidad en la caja torácica.",
    "Cartílago costal que conecta la primera costilla derecha con el esternón. Proporciona flexibilidad en la caja torácica.",
    "Cartílago costal que conecta la quinta costilla izquierda con el esternón. Ayuda a la flexibilidad de la caja torácica.",
    "Cartílago costal que conecta la quinta costilla derecha con el esternón. Ayuda a la flexibilidad de la caja torácica.",
    "Cartílago costal que conecta la segunda costilla izquierda con el esternón. Facilita la flexibilidad torácica.",
    "Cartílago costal que conecta la segunda costilla derecha con el esternón. Facilita la flexibilidad torácica.",
    "Cartílago costal que conecta la sexta costilla izquierda con el esternón. Facilita la expansión y flexibilidad torácica.",
    "Cartílago costal que conecta la sexta costilla derecha con el esternón. Facilita la expansión y flexibilidad torácica.",
    "Cartílago costal que conecta la séptima costilla izquierda con el esternón. Facilita la flexibilidad torácica.",
    "Cartílago costal que conecta la séptima costilla derecha con el esternón. Facilita la flexibilidad torácica.",
    "Cartílago costal que conecta la tercera costilla izquierda con el esternón. Facilita la expansión de la caja torácica.",
    "Cartílago costal que conecta la tercera costilla derecha con el esternón. Facilita la expansión de la caja torácica.",
    "Clavícula izquierda. Conecta el esternón con el omóplato, permitiendo el movimiento del brazo.",
    "Clavícula derecha. Conecta el esternón con el omóplato, permitiendo el movimiento del brazo.",
    "Cuarta costilla izquierda. Conecta con el esternón y contribuye a la expansión torácica.",
    "Cuarta costilla derecha. Conecta con el esternón y contribuye a la expansión torácica.",
    "Cuerpo del esternón. Soporta las costillas y conecta con la clavícula.",
    "Duodécima costilla izquierda. Forma parte de la caja torácica.",
    "Duodécima costilla derecha. Forma parte de la caja torácica.",
    "Décima costilla izquierda. Conecta con el esternón, ayudando a la expansión torácica.",
    "Décima costilla derecha. Conecta con el esternón, ayudando a la expansión torácica.",
    "Escápula izquierda. Conecta los omóplatos con el esternón.",
    "Escápula derecha. Conecta los omóplatos con el esternón.",
    "Manubrio del esternón. Conecta las clavículas y las primeras costillas.",
    "Novena costilla izquierda. Contribuye a la expansión de la caja torácica.",
    "Novena costilla derecha. Contribuye a la expansión de la caja torácica.",
    "Octava costilla izquierda. Ayuda a la flexibilidad torácica.",
    "Octava costilla derecha. Ayuda a la flexibilidad torácica.",
    "Primera costilla izquierda. Conecta con el esternón y facilita la respiración.",
    "Primera costilla derecha. Conecta con el esternón y facilita la respiración.",
    "Proceso xifoides. Se encuentra en la parte inferior del esternón, sirviendo de anclaje para los músculos del abdomen.",
    "Quinta costilla izquierda. Facilita la expansión torácica.",
    "Quinta costilla derecha. Facilita la expansión torácica.",
    "Segunda costilla izquierda. Conecta con el esternón y facilita la respiración.",
    "Segunda costilla derecha. Conecta con el esternón y facilita la respiración.",
    "Sexta costilla izquierda. Ayuda a la flexibilidad de la caja torácica.",
    "Sexta costilla derecha. Ayuda a la flexibilidad de la caja torácica.",
    "Séptima costilla izquierda. Facilita la expansión torácica.",
    "Séptima costilla derecha. Facilita la expansión torácica.",
    "Tercera costilla izquierda. Facilita la flexibilidad torácica.",
    "Tercera costilla derecha. Facilita la flexibilidad torácica.",
    "Undécima costilla izquierda. Ayuda a la flexibilidad de la caja torácica.",
    "Undécima costilla derecha. Ayuda a la flexibilidad de la caja torácica."
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
        if (tituloTexto) tituloTexto.text = "Selecciona una estructura ósea";
        if (descripcionTexto) descripcionTexto.text = "Agarra una pieza para ver su descripción médica.";
    }
}
