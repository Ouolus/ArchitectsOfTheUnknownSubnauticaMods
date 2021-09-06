using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotA.Mono.VFX
{
    public class PrecursorIllumControl : MonoBehaviour
    {
        private Color _colorBefore;
        private Color _colorNow; // the color that the material will be set to each frame.
        private Color _targetColor;
        private float _timeColorShifted;
        private float _shiftLength;
        public List<Renderer> renderers;
        private List<Material> _materials;
        public Light light;

        public Color TargetColor { get { return _targetColor; } }

        private void InitializeMaterials()
        {
            _materials = new List<Material>();
            for(int i = 0; i < renderers.Count; i++)
            {
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    _materials.Add(renderers[i].materials[j]);
                }
            }
        }

        private void Start()
        {
            InitializeMaterials();

            Color defaultColor = GetColorForEnum(PrecursorColor.Green);
            _colorNow = defaultColor;
            SetTargetColor(defaultColor, 1f);
        }

        public void SetTargetColor(Color color, float shiftLength)
        {
            _colorBefore = _colorNow;
            _targetColor = color;
            _timeColorShifted = Time.time;
            _shiftLength = shiftLength;
            StopAllCoroutines();
        }

        public void SetTargetColor(PrecursorColor color, float shiftLength)
        {
            _colorBefore = _colorNow;
            _targetColor = GetColorForEnum(color);
            _timeColorShifted = Time.time;
            _shiftLength = shiftLength;
            StopAllCoroutines();
        }

        private void SetTargetColorWithoutStoppingCoroutine(Color color, float shiftLength)
        {
            _colorBefore = _colorNow;
            _targetColor = color;
            _timeColorShifted = Time.time;
            _shiftLength = shiftLength;
        }

        public void Pulse(Color intoColor, Color resetColor, float pulseLength, float transitionInLength, float transitionOutLength)
        {
            StartCoroutine(PulseCoroutine(intoColor, resetColor, pulseLength, transitionInLength, transitionOutLength));
        }

        public void Pulse(PrecursorColor intoColor, PrecursorColor resetColor, float pulseLength, float transitionInLength, float transitionOutLength)
        {
            StartCoroutine(PulseCoroutine(GetColorForEnum(intoColor), GetColorForEnum(resetColor), pulseLength, transitionInLength, transitionOutLength));
        }

        private IEnumerator PulseCoroutine(Color target, Color resetColor, float pulseLength, float transitionInLength, float transitionOutLength)
        {
            SetTargetColorWithoutStoppingCoroutine(target, transitionInLength);
            yield return new WaitForSeconds(transitionInLength + pulseLength);
            SetTargetColorWithoutStoppingCoroutine(resetColor, transitionOutLength);
        }

        void Update()
        {
            _colorNow = Color.Lerp(_colorBefore, _targetColor, (Time.time - _timeColorShifted) / _shiftLength);
            foreach (Material material in _materials)
            {
                material.SetColor("_GlowColor", _colorNow);
            }
            if (light is not null)
            {
                light.color = _colorNow;
            }
        }

        public enum PrecursorColor
        {
            Black,
            Green,
            Purple,
            Pink
        }

        static Color GetColorForEnum(PrecursorColor col)
        {
            switch (col)
            {
                default:
                    return Color.black;
                case PrecursorColor.Green:
                    return _green;
                case PrecursorColor.Purple:
                    return _purple;
                case PrecursorColor.Pink:
                    return _pink;
            }
        }

        static readonly Color _green = new Color(0.54f, 1f, 0.54f);
        static readonly Color _purple = new Color(0.75f, 0f, 1f);
        static readonly Color _pink = new Color(1f, 0.5f, 0.8f);
    }
}
