using System.Collections;
using ArchitectsLibrary.Utility;
using RotA.Mono.Creatures.GargEssentials;
using UnityEngine;

namespace RotA.Mono.Creatures.Baby
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
		bool _goodBye;

		public void Start()
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
			if (!CanInteract()) 
				return;

			var goodByeText = Rocket.IsAnyRocketReady ? "SayFarewell" : null;
			
			if (!string.IsNullOrEmpty(goodByeText))
				goodByeText = LanguageCache.GetButtonFormat(goodByeText, GameInput.Button.RightHand);

			if (Rocket.IsAnyRocketReady)
				_goodBye = Player.main.GetRightHandDown();
			
#pragma warning disable 618
			HandReticle.main.SetInteractText("PlayWithFish", goodByeText, true, false, true);
#pragma warning restore 618
			HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
		}

		public void OnHandClick(GUIHand hand)
		{
			if (!CanInteract())
				return;
				
			PlayCinematic();
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
			roar.PlayOnce(out _, GargantuanRoar.RoarMode.CloseOnly);
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
			if(random <= 0.25f)
            {
				return 14.12f / 2f;
            }
			else if(random <= 0.5f)
            {
				return 8f / 2f;
            }
			else if(random <= 0.75f)
            {
				return 8f / 2f;
            }
            else
            {
				return 8.36f / 2f;
			}
        }

		void Update()
        {
	        if (cinematicPlaying)
	        {
		        swimBehaviour.Idle();
	        }
	        
	        if (_goodBye)
	        {
		        _goodBye = false;
		        PlayCinematic();
	        }
        }
	}
}
