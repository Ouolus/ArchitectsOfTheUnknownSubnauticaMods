using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace LeviathanEggs.MonoBehaviours
{
    // this behaviour is yoinked from MrPurple6411. Hippity Hoppity, your code is now our property ;)
    class RobotEggPulsating : MonoBehaviour
    {
        Renderer[] renderers;

        float _currentStrength = 0f;
        float _nextStrength = 2f;
        float _changeTime = 2f;
        float _timer = 0f;
        void Awake()
        {
            renderers = gameObject.GetComponentsInChildren<Renderer>();
        }
        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _changeTime)
            {
                _currentStrength = _nextStrength;
                _nextStrength = _currentStrength == 2f ? 0f : 2f;
                _timer = 0f;
            }
            foreach (var renderer in renderers)
            {
                renderer?.material?.SetFloat(ShaderPropertyID._GlowStrength, Mathf.Lerp(_currentStrength, _nextStrength, _timer / _changeTime));
                renderer?.material?.SetFloat(ShaderPropertyID._GlowStrengthNight, Mathf.Lerp(_currentStrength, _nextStrength, _timer / _changeTime));
            }
        }
        void OnDestroy()
        {
            foreach (var renderer in renderers)
            {
                renderer?.material?.SetFloat(ShaderPropertyID._GlowStrength, 1f);
                renderer?.material?.SetFloat(ShaderPropertyID._GlowStrengthNight, 1f);
            }
        }
    }
}
