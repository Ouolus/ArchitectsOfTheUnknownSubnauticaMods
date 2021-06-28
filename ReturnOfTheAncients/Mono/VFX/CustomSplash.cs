using UnityEngine;

namespace RotA.Mono.VFX
{
    public class CustomSplash : MonoBehaviour
    {
        Renderer renderer;
        float animTime;
        bool playing = true;
        Vector3 newScale;
        Vector3 startScale;

        public float duration = 5f;
        public AnimationCurve surfScaleX;
        public AnimationCurve surfScaleY;
        public AnimationCurve surfScaleZ;
        public AnimationCurve surfMaskCurve;
        public float scale;

        void Start()
        {
            renderer = gameObject.GetComponentInChildren<Renderer>();
            startScale = new Vector3(scale, scale, scale);
        }
        void Update()
        {
            if (!playing) 
                return;
            
            animTime += Time.deltaTime / duration;
            if (animTime > 0.99f)
            {
                playing = false;
                Destroy(gameObject);
            }
                
            renderer.material.SetTextureOffset(ShaderPropertyID._MaskTex, new Vector2(surfMaskCurve.Evaluate(animTime), 0.5f));
            newScale.x = startScale.x * surfScaleX.Evaluate(animTime);
            newScale.y = startScale.y * surfScaleY.Evaluate(animTime);
            newScale.z = startScale.z * surfScaleZ.Evaluate(animTime);
            transform.localScale = newScale;
        }
    }
}
