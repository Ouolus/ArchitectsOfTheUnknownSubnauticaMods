using System;
using System.Collections.Generic;
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
        /// The color that the material is set to each frame.
        /// </summary>
        private Color colorNow;
        private Color targetColor;
        private float timeColorShifted;
        private float shiftSpeed;
        private Renderer renderer;

        private void Start()
        {
            colorNow = Color.green;
            SetTargetColor(Color.green, 1f);
            renderer = GetComponentInChildren<Renderer>();
        }

        public void SetTargetColor(Color color, float shiftSpeed)
        {
            colorBefore = colorNow;
            targetColor = color;
            timeColorShifted = Time.time;
            this.shiftSpeed = shiftSpeed;
        }

        void Update()
        {
            colorNow = Color.Lerp(colorBefore, targetColor, (Time.time - timeColorShifted) * shiftSpeed);
            renderer.material.SetColor("_Color", colorNow);
        }
    }
}
