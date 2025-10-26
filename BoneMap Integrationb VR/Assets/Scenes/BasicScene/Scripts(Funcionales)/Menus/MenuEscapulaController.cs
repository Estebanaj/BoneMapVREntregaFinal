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
            // --- Tu bloque de descripciones exacto ---
            "Banda de cartílago hialino que conecta la primera costilla izquierda con el manubrio del esternón...",
            // ... resto de tu texto médico completo (idéntico al original)
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
