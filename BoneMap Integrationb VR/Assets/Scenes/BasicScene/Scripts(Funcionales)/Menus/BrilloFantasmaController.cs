using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BrilloFantasmaController : MonoBehaviour
{
    private Material matInstancia;
    private Coroutine transicionBrillo;
    private float brilloActual = 0f;

    [Header("Configuración del brillo")]
    public Color colorBrillo = new Color(0.3f, 0.8f, 1f, 1f);
    [Range(0f, 5f)] public float intensidadMaxima = 2f;
    [Range(1f, 10f)] public float velocidadTransicion = 3f;

    void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            // Instancia segura del material
            matInstancia = rend.material;
            matInstancia.EnableKeyword("_EMISSION");
            matInstancia.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            Debug.LogError($"[BrilloFantasmaController] Renderer no encontrado en {gameObject.name}");
        }
    }

    void OnEnable()
    {
        // Reinicia el estado visual del material al activarse
        if (matInstancia != null)
        {
            matInstancia.SetColor("_EmissionColor", Color.black);
            brilloActual = 0f;
        }
    }

    public void ActivarBrillo()
    {
        if (matInstancia == null)
        {
            Debug.LogWarning($"[BrilloFantasmaController] No hay material en {gameObject.name}");
            return;
        }

        // Evita intentar iniciar una coroutine si el objeto está inactivo
        if (!gameObject.activeInHierarchy)
            return;

        if (transicionBrillo != null)
            StopCoroutine(transicionBrillo);

        var rutina = FadeBrillo(1f);
        if (rutina != null)
            transicionBrillo = StartCoroutine(rutina);
    }

    public void DesactivarBrillo()
    {
        if (matInstancia == null)
        {
            Debug.LogWarning($"[BrilloFantasmaController] No hay material en {gameObject.name}");
            return;
        }

        // Evita coroutine si el objeto está inactivo
        if (!gameObject.activeInHierarchy)
            return;

        if (transicionBrillo != null)
            StopCoroutine(transicionBrillo);

        var rutina = FadeBrillo(0f);
        if (rutina != null)
            transicionBrillo = StartCoroutine(rutina);
    }

    private System.Collections.IEnumerator FadeBrillo(float objetivo)
    {
        if (matInstancia == null)
        {
            Debug.LogWarning($"[BrilloFantasmaController] Material nulo en FadeBrillo de {gameObject.name}");
            yield break;
        }

        while (Mathf.Abs(brilloActual - objetivo) > 0.01f)
        {
            brilloActual = Mathf.MoveTowards(brilloActual, objetivo, Time.deltaTime * velocidadTransicion);
            Color emis = colorBrillo * (brilloActual * intensidadMaxima);
            matInstancia.SetColor("_EmissionColor", emis);
            yield return null;
        }

        if (objetivo == 0f)
            matInstancia.SetColor("_EmissionColor", Color.black);
    }
}
