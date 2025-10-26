using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class MostrarInfoHuesoPierna : MonoBehaviour
{
    private MenuPiernaController menu;
    private BrilloFantasmaController brilloAsociado;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private Transform fantasmaTransform;
    private bool inicializado = false;
    private bool estaColocado = false;

    void OnEnable()
    {
        if (!inicializado)
        {
            InicializarReferencias();
            inicializado = true;
        }
    }

    private void InicializarReferencias()
    {
        menu = FindObjectOfType<MenuPiernaController>();
        if (menu == null)
        {
            Debug.LogError("[MostrarInfoHuesoPierna] No se encontró MenuPiernaController en la escena.");
            return;
        }

        brilloAsociado = BuscarFantasmaCorrespondiente();
        if (brilloAsociado != null)
            fantasmaTransform = brilloAsociado.transform;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        interactable.selectEntered.RemoveAllListeners();
        interactable.selectExited.RemoveAllListeners();

        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    private BrilloFantasmaController BuscarFantasmaCorrespondiente()
    {
        foreach (var h in menu.huesos)
        {
            if (h.hueso == transform && h.destinoFantasma != null)
                return h.destinoFantasma.GetComponent<BrilloFantasmaController>();
        }
        return null;
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (estaColocado) return;

        if (menu != null)
            menu.MostrarInfoDeHueso(transform);

        if (brilloAsociado != null)
            brilloAsociado.ActivarBrillo();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (estaColocado)
        {
            if (brilloAsociado != null)
                brilloAsociado.DesactivarBrillo();
            return;
        }

        if (brilloAsociado != null)
            brilloAsociado.DesactivarBrillo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fantasmaTransform != null && other.transform == fantasmaTransform)
        {
            estaColocado = true;
            if (brilloAsociado != null)
                brilloAsociado.DesactivarBrillo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (fantasmaTransform != null && other.transform == fantasmaTransform)
            estaColocado = false;
    }
}
