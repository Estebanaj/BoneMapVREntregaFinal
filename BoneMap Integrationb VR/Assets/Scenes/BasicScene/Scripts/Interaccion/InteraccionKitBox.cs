// InteraccionKitBox.cs
// Úsalo JUNTO al XRSocketInteractor en cada objeto "... _Fantasma".
// - Desactiva el hover azul del socket.
// - Hover VERDE si coincide y el socket está libre.
// - Hover ROJO si NO coincide y el socket está libre.
// - Sin color cuando está seleccionado (ocupado).
// - No muestra rojo si ya hay un hueso encajado.
// - Cancela selección si intentan encajar uno incorrecto (opcional rebote).

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class InteraccionKitBox : MonoBehaviour
{
    [Tooltip("Si está vacío, se deduce del nombre de este objeto quitando el sufijo '_Fantasma'.")]
    public string acceptedName = "";

    [Header("Feedback de color (sobre el FANTASMA)")]
    public Color matchColor = Color.green; // coincide -> verde
    public Color mismatchColor = Color.red;   // no coincide -> rojo

    [Header("Rechazo (opcional)")]
    [Tooltip("Impulso aplicado al objeto incorrecto al intentar encajar (0 = sin rebote).")]
    public float rejectImpulse = 0f; // pon >0 si quieres un empujón

    XRSocketInteractor socket;

    struct ColorState { public bool hasBase; public Color baseCol; public bool hasColor; public Color col; }
    readonly Dictionary<Renderer, ColorState> ghostOriginalColors = new Dictionary<Renderer, ColorState>();

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();

        // Quitar el hover azul por defecto del socket
        socket.showInteractableHoverMeshes = false;

        if (string.IsNullOrWhiteSpace(acceptedName))
            acceptedName = InferBaseName(gameObject.name);

        // Suscripción (UnityEvents en XRI)
        socket.hoverEntered.AddListener(OnHoverEntered);
        socket.hoverExited.AddListener(OnHoverExited);
        socket.selectEntered.AddListener(OnSelectEntered);
        socket.selectExited.AddListener(OnSelectExited);
    }

    void OnDestroy()
    {
        if (!socket) return;
        socket.hoverEntered.RemoveListener(OnHoverEntered);
        socket.hoverExited.RemoveListener(OnHoverExited);
        socket.selectEntered.RemoveListener(OnSelectEntered);
        socket.selectExited.RemoveListener(OnSelectExited);
    }

    void OnDisable()
    {
        // Por si el objeto se desactiva mientras está tintado
        RestoreGhostTint();
    }

    // ---------- EVENTOS ----------
    void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Si el socket ya está ocupado, no mostrar ningún color
        if (socket.hasSelection) return;

        bool isMatch = Matches(args.interactableObject.transform);
        TintGhost(isMatch ? matchColor : mismatchColor);
    }

    void OnHoverExited(HoverExitEventArgs args)
    {
        // Siempre restauramos el color del fantasma al salir del hover
        RestoreGhostTint();
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        var t = args.interactableObject.transform;

        if (!Matches(t))
        {
            // Cancelar encaje y (opcional) rebote
            socket.interactionManager.CancelInteractableSelection(args.interactableObject);
            Bounce(t);
            RestoreGhostTint();
            return;
        }

        // Es el correcto: encaja y el fantasma vuelve a su color original (sin verde permanente)
        RestoreGhostTint();
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        // Al soltar, aseguramos que el fantasma vuelve a su color original
        RestoreGhostTint();
    }

    // ---------- LÓGICA ----------
    static string InferBaseName(string socketName)
    {
        var n = socketName.Replace("(Clone)", "", StringComparison.OrdinalIgnoreCase).Trim();
        // Quita el sufijo "_Fantasma" tolerando espacios: "Nombre ... _Fantasma"
        n = Regex.Replace(n, @"\s*_Fantasma\s*$", "", RegexOptions.IgnoreCase);
        return n.Trim();
    }

    static string CleanName(Transform t)
        => t.name.Replace("(Clone)", "", StringComparison.OrdinalIgnoreCase).Trim();

    bool Matches(Transform t)
        => CleanName(t).Equals(acceptedName, StringComparison.OrdinalIgnoreCase);

    void Bounce(Transform t)
    {
        if (rejectImpulse <= 0f) return;
        var rb = t.GetComponent<Rigidbody>();
        if (!rb) return;

        var dir = (t.position - transform.position);
        if (dir.sqrMagnitude < 1e-6f) dir = Vector3.up;
        rb.AddForce(dir.normalized * rejectImpulse, ForceMode.VelocityChange);
    }

    // ---------- TINTE DEL FANTASMA ----------
    void TintGhost(Color c)
    {
        foreach (var r in GetComponentsInChildren<Renderer>(true))
        {
            if (!ghostOriginalColors.ContainsKey(r))
            {
                var st = new ColorState();
                var mat = r.sharedMaterial;
                if (mat != null)
                {
                    if (mat.HasProperty("_BaseColor")) { st.hasBase = true; st.baseCol = mat.GetColor("_BaseColor"); }
                    if (mat.HasProperty("_Color")) { st.hasColor = true; st.col = mat.GetColor("_Color"); }
                }
                ghostOriginalColors[r] = st;
            }

            var block = new MaterialPropertyBlock();
            r.GetPropertyBlock(block);
            if (HasProp(r, "_BaseColor")) block.SetColor("_BaseColor", c);
            if (HasProp(r, "_Color")) block.SetColor("_Color", c);
            r.SetPropertyBlock(block);
        }
    }

    void RestoreGhostTint()
    {
        foreach (var r in GetComponentsInChildren<Renderer>(true))
        {
            ColorState st;
            if (!ghostOriginalColors.TryGetValue(r, out st)) continue;

            var block = new MaterialPropertyBlock();
            r.GetPropertyBlock(block);
            if (st.hasBase && HasProp(r, "_BaseColor")) block.SetColor("_BaseColor", st.baseCol);
            if (st.hasColor && HasProp(r, "_Color")) block.SetColor("_Color", st.col);
            r.SetPropertyBlock(block);
        }
    }

    bool HasProp(Renderer r, string prop)
    {
        var mat = r.sharedMaterial;
        return mat != null && mat.HasProperty(prop);
    }
}
