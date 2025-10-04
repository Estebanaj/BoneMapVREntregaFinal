// InteraktorHuesosQuetzal.cs
// Úsalo JUNTO al XRSocketInteractor en cada objeto "... _Fantasma".
// - Quita el hover azul del socket.
// - Hover VERDE si coincide y está encima (dentro de hoverDistance).
// - Sin color cuando no coincide, el socket está ocupado o el objeto está lejos.
// - Cancela selección y rebota si intentan encajar uno incorrecto (opcional).

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace BoneMapVR
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class InteraktorHuesosQuetzal : MonoBehaviour
    {
        [Tooltip("Si está vacío, se deduce del nombre quitando el sufijo '_Fantasma'.")]
        public string acceptedName = "";

        [Header("Feedback de color (sobre el FANTASMA)")]
        public Color matchColor = Color.green; // coincide -> verde

        [Header("Rechazo (opcional)")]
        [Tooltip("Impulso aplicado al objeto incorrecto al intentar encajar (0 = sin rebote).")]
        public float rejectImpulse = 0f; // pon >0 si quieres un empujón

        [Header("Distancia de activación")]
        [Tooltip("Distancia máxima para considerar que el objeto está 'encima' del socket.")]
        public float hoverDistance = 0.2f; // ajusta según tu escala

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
            RestoreGhostTint();
        }

        // ---------- EVENTOS ----------
        void OnHoverEntered(HoverEnterEventArgs args)
        {
            if (socket.hasSelection) return;

            var t = args.interactableObject.transform;
            float dist = Vector3.Distance(t.position, transform.position);

            // Solo mostramos color si realmente está "encima" del socket
            if (dist > hoverDistance)
            {
                RestoreGhostTint();
                return;
            }

            bool isMatch = Matches(t);

            // Solo pintamos si coincide
            if (isMatch)
                TintGhost(matchColor);
            else
                RestoreGhostTint();
        }

        void OnHoverExited(HoverExitEventArgs args)
        {
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

            // Correcto: encaja y limpiamos color
            RestoreGhostTint();
        }

        void OnSelectExited(SelectExitEventArgs args)
        {
            RestoreGhostTint();
        }

        // ---------- LÓGICA ----------
        static string InferBaseName(string socketName)
        {
            var n = socketName.Replace("(Clone)", "", StringComparison.OrdinalIgnoreCase).Trim();
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
                if (!ghostOriginalColors.TryGetValue(r, out var st)) continue;

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
}
