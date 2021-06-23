using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotA.Mono.VFX
{
    public class CustomSplash : MonoBehaviour
    {
		private float animTime = 0f;
		private bool playing;
		private Vector3 newScale;
		public Vector3 startScale;

		public float duration = 5f;
		public AnimationCurve surfScaleX;
		public AnimationCurve surfScaleY;
		public AnimationCurve surfScaleZ;
		public AnimationCurve surfMaskCurve;

		private void Update()
		{
			if (playing)
			{
				animTime += Time.deltaTime / duration;
				if (animTime > 0.99f)
				{
					playing = false;
					Destroy(gameObject);
				}
				if (gameObject != null)
				{
					gameObject.GetComponentInChildren<Renderer>().material.SetTextureOffset(ShaderPropertyID._MaskTex, new Vector2(surfMaskCurve.Evaluate(this.animTime), 0.5f));
					newScale.x = startScale.x * surfScaleX.Evaluate(animTime);
					newScale.y = startScale.y * surfScaleY.Evaluate(animTime);
					newScale.z = startScale.z * surfScaleZ.Evaluate(animTime);
					transform.localScale = newScale;
				}
			}
		}
	}
}
