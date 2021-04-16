using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono
{
	//For
	public class GargBabyTarget : HandTarget, IHandTarget
	{
		public bool cinematicPlaying = false;
		Animator animator;
		LiveMixin lm;
		SwimBehaviour swimBehaviour;
		Pickupable pickupable;
		Creature creature;

		void Start()
		{
			animator = transform.parent.GetComponentInChildren<Animator>();
			swimBehaviour = GetComponentInParent<SwimBehaviour>();
			lm = GetComponentInParent<LiveMixin>();
			gameObject.layer = 13;
			pickupable = GetComponentInParent<Pickupable>();
			creature = GetComponent<Creature>();
		}
		public void OnHandHover(GUIHand hand)
		{
			if (CanInteract())
			{
				HandReticle.main.SetInteractText("PlayWithFish", true, HandReticle.Hand.Right);
				HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
			}
		}

		public void OnHandClick(GUIHand hand)
		{
			if (CanInteract())
			{
				PlayCinematic();
			}
		}

		public void PlayCinematic()
		{
			StartCoroutine(Cinematic());
		}
		private IEnumerator Cinematic()
		{
			cinematicPlaying = true;
			pickupable.isPickupable = false;
			swimBehaviour.Idle();
			float random = Random.value;
			swimBehaviour.LookAt(Player.main.transform);
			animator.SetFloat("random", random);
			animator.SetTrigger("cin_play");
			yield return new WaitForSeconds(GetAnimationLength(random));
			swimBehaviour.LookAt(null);
			pickupable.isPickupable = true;
			cinematicPlaying = false;
			creature.Aggression.Value = 0f;
		}

		bool CanInteract()
		{
			if (cinematicPlaying)
			{
				return false;
			}
			if (!lm.IsAlive())
			{
				return false;
			}
			return true;
		}

		float GetAnimationLength(float random)
        {
			if(random <= 0.33333f)
            {
				return 4.02f;
            }
			else if(random <= 0.677777f)
            {
				return 8f;
            }
            else
            {
				return 4f;
			}
        }
	}
}
