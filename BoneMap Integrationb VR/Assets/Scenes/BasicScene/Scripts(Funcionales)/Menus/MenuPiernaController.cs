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
            // --- Tu lista de descripciones médicas exactas ---
            "Hueso más largo y resistente del cuerpo. Conecta la pelvis al esqueleto de la pierna...",
            // ... resto del texto completo sin cambios
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
