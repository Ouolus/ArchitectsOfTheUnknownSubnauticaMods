using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotA.Mono.Equipment
{
    public class WarpCannonIllumControl : MonoBehaviour
    {
        private Color colorBefore;
        /// <summary>
        /// The color that the material will be set to each frame.
        /// </summary>
        private Color colorNow;
        private Color targetColor;
        private float timeColorShifted;
        private float shiftLength;
        private Renderer renderer;

        private void Start()
        {
            colorNow = Color.green;
            SetTargetColor(Color.green, 1f);
            renderer = GetComponentInChildren<Renderer>();
        }

        public void SetTargetColor(Color color, float shiftLength)
        {
            colorBefore = colorNow;
            targetColor = color;
            timeColorShifted = Time.time;
            this.shiftLength = shiftLength;
            this.StopAllCoroutines();
        }

        private void SetTargetColorWithoutStoppingCoroutine(Color color, float shiftLength)
        {
            colorBefore = colorNow;
            targetColor = color;
            timeColorShifted = Time.time;
            this.shiftLength = shiftLength;
            this.StopAllCoroutines();
        }

        public void Pulse(Color intoColor, Color resetColor, float pulseLength, float transitionInLength, float transitionOutLength)
        {
            StartCoroutine(PulseCoroutine(intoColor, resetColor, pulseLength, transitionInLength, transitionOutLength));
        }

        private IEnumerator PulseCoroutine(Color target, Color resetColor, float pulseLength, float transitionInLength, float transitionOutLength)
        {
            SetTargetColorWithoutStoppingCoroutine(target, transitionInLength);
            yield return new WaitForSeconds(transitionInLength + pulseLength);
            SetTargetColorWithoutStoppingCoroutine(resetColor, transitionOutLength);
        }

        void Update()
        {
            colorNow = Color.Lerp(colorBefore, targetColor, (Time.time - timeColorShifted) / shiftLength);
            renderer.material.SetColor("_Color", colorNow);
        }
    }
}
