using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using ArchitectsLibrary.Utility;

namespace ProjectAncients.Mono
{
	public class GargBabyTarget : HandTarget, IHandTarget
	{
		public bool cinematicPlaying = false;
		Animator animator;
		LiveMixin lm;
		SwimBehaviour swimBehaviour;
		Pickupable pickupable;
		Creature creature;
		GargantuanRoar roar;
		PlayerCinematicController cinematicController;
		Rigidbody rb;

		void Start()
		{
			animator = transform.parent.GetComponentInChildren<Animator>();
			rb = transform.parent.GetComponentInChildren<Rigidbody>();
			swimBehaviour = GetComponentInParent<SwimBehaviour>();
			lm = GetComponentInParent<LiveMixin>();
			gameObject.layer = 13;
			pickupable = GetComponentInParent<Pickupable>();
			creature = GetComponentInParent<Creature>();
			roar = GetComponentInParent<GargantuanRoar>();
			cinematicController = gameObject.EnsureComponent<PlayerCinematicController>();
			cinematicController.playerViewAnimationName = "cutefish_tickled";
			cinematicController.animator = animator;
			cinematicController.animParam = "cin_play";
			cinematicController.animParamReceivers = new GameObject[0];
			cinematicController.animatedTransform = transform.parent.gameObject.SearchChild("PlayerCam").transform;

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
			cinematicController.StartCinematicMode(Player.main);
			rb.isKinematic = true;
			roar.PlayOnce(out float _, GargantuanRoar.RoarMode.CloseOnly);
			yield return new WaitForSeconds(GetAnimationLength(random));
			rb.isKinematic = false;
			cinematicController.EndCinematicMode();
			cinematicPlaying = false;
			swimBehaviour.LookAt(null);
			pickupable.isPickupable = true;
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
				return 4.8f;
            }
            else
            {
				return 4f;
			}
        }

		void Update()
        {
            if (cinematicPlaying)
            {
				swimBehaviour.Idle();
			}
		}
	}
}
