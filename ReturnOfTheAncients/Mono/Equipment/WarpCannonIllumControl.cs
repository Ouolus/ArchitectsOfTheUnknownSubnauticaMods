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
        private Color startingColor;
        private Color currentColor;
        private Color targetColor;
        private float timeColorShifted;
        private float shiftSpeed;

        private void Start()
        {
            currentColor = Color.green;
            SetColor(Color.green, 1f);
        }

        private void SetColor(Color color, float shiftSpeed)
        {
            targetColor = color;
        }

        void Update()
        {
            currentColor = Color.Lerp(startingColor, targetColor, (Time.time - timeColorShifted) * shiftSpeed)
        }
    }
}
