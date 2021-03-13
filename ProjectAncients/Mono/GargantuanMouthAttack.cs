using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;
using ECCLibrary.Internal;

namespace ProjectAncients.Mono
{
    public class GargantuanMouthAttack : MeleeAttack
    {
		private AudioSource attackSource;
		private ECCAudio.AudioClipPool biteClipPool;
		private ECCAudio.AudioClipPool cinematicClipPool;
		private GargantuanBehaviour behaviour;
		private GameObject throat;

		private PlayerCinematicController playerDeathCinematic;

		public bool canAttackPlayer = true;

		void Start()
		{
			attackSource = gameObject.AddComponent<AudioSource>();
			attackSource.minDistance = 10f;
			attackSource.maxDistance = 40f;
			attackSource.spatialBlend = 1f;
			attackSource.volume = ECCHelpers.GetECCVolume();
			biteClipPool = ECCAudio.CreateClipPool("GargBiteAttack");
			cinematicClipPool = ECCAudio.CreateClipPool("GargBiteAttack5");
			throat = gameObject.SearchChild("Head");
			gameObject.SearchChild("Mouth").EnsureComponent<OnTouch>().onTouch = new OnTouch.OnTouchEvent();
			gameObject.SearchChild("Mouth").EnsureComponent<OnTouch>().onTouch.AddListener(OnTouch);
			behaviour = GetComponent<GargantuanBehaviour>();

			playerDeathCinematic = gameObject.AddComponent<PlayerCinematicController>();
			playerDeathCinematic.animatedTransform = gameObject.SearchChild("AttachBone").transform;
			playerDeathCinematic.animator = creature.GetAnimator();
			playerDeathCinematic.animParamReceivers = new GameObject[0];
			playerDeathCinematic.animParam = "cin_player";
		}
		public override void OnTouch(Collider collider) //A long method having to do with interaction with an object and the mouth.
		{
			if (frozen) //Stasis rifle = no attack
			{
				return;
			}
			if (liveMixin.IsAlive() && Time.time > behaviour.timeCanAttackAgain && !playerDeathCinematic.IsCinematicModeActive()) //If it can attack, continue
			{
				Creature thisCreature = gameObject.GetComponent<Creature>();
				if (thisCreature.Aggression.Value >= 0.1f) //This creature must have at least some level of aggression to bite
				{
					GameObject target = GetTarget(collider);
					if (!behaviour.Edible(target))
					{
						return;
					}
					if (!behaviour.IsHoldingVehicle())
					{
						Player player = target.GetComponent<Player>();
						if (player != null)
						{
							if (!canAttackPlayer)
							{
								return;
							}
							if (!player.CanBeAttacked() || !player.liveMixin.IsAlive() || player.cinematicModeActive)
							{
								return;
							}
							else
							{
								var num = DamageSystem.CalculateDamage(GetBiteDamage(target), DamageType.Normal, target);
								if (liveMixin.health - num <= 0f) // make sure that the nodamage cheat is not on
								{
									StartCoroutine(PerformPlayerCinematic(player));
									return;
								}
							}
						}
						else if (canAttackPlayer && behaviour.GetCanGrabVehicle())
						{
							SeaMoth component4 = target.GetComponent<SeaMoth>();
							if (component4 && !component4.docked)
							{
								behaviour.GrabGenericSub(component4);
								thisCreature.Aggression.Value -= 0.25f;
								return;
							}
							Exosuit component5 = target.GetComponent<Exosuit>();
							if (component5 && !component5.docked)
							{
								behaviour.GrabExosuit(component5);
								thisCreature.Aggression.Value -= 0.25f;
								return;
							}
						}
						LiveMixin targetLm = target.GetComponent<LiveMixin>();
						if (targetLm == null) return;
						if (!targetLm.IsAlive())
						{
							return;
						}
						if (!CanAttackTargetFromPosition(target))
						{
							return;
						}
						if (behaviour.CanSwallowWhole(target, targetLm))
						{
							creature.GetAnimator().SetTrigger("bite");
							thisCreature.Hunger.Value -= 0.15f;
							var swallowing = target.AddComponent<BeingSuckedInWhole>();
							swallowing.target = throat.transform;
							swallowing.animationLength = 1f;
						}
						else
						{
							var num = DamageSystem.CalculateDamage(GetBiteDamage(target), DamageType.Normal, target);
							if (liveMixin.health - num <= 0f) // make sure that the nodamage cheat is not on
							{
								StartCoroutine(PerformBiteAttack(target));
								this.behaviour.timeCanAttackAgain = Time.time + 2f;
								attackSource.clip = biteClipPool.GetRandomClip();
								attackSource.Play();
								thisCreature.Aggression.Value -= 0.15f;
								creature.GetAnimator().SetTrigger("bite");
							}
						}
					}
				}
			}
		}
		private bool CanAttackTargetFromPosition(GameObject target) //A quick raycast check to stop the Gargantuan from attacking through walls. Taken from the game's code (shh).
		{
			Vector3 direction = target.transform.position - transform.position;
			float magnitude = direction.magnitude;
			int num = UWE.Utils.RaycastIntoSharedBuffer(transform.position, direction, magnitude, -5, QueryTriggerInteraction.Ignore);
			for (int i = 0; i < num; i++)
			{
				Collider collider = UWE.Utils.sharedHitBuffer[i].collider;
				GameObject gameObject = (collider.attachedRigidbody != null) ? collider.attachedRigidbody.gameObject : collider.gameObject;
				if (!(gameObject == target) && !(gameObject == base.gameObject) && !(gameObject.GetComponent<Creature>() != null))
				{
					return false;
				}
			}
			return true;
		}
		public override float GetBiteDamage(GameObject target) //Extra damage to Cyclops. Otherwise, does its base damage.
		{
			if (target.GetComponent<SubControl>() != null)
			{
				return 500f; //cyclops damage
			}
			return 2500f; //base damage
		}
		public void OnVehicleReleased() //Called by gargantuan behavior. Gives a cooldown until the next bite.
		{
			behaviour.timeCanAttackAgain = Time.time + 4f;
		}
		private IEnumerator PerformBiteAttack(GameObject target) //A delayed attack, to let him chomp down first.
		{
			yield return new WaitForSeconds(0.5f);
			if(target) target.GetComponent<LiveMixin>().TakeDamage(GetBiteDamage(target));
		}
		private IEnumerator PerformPlayerCinematic(Player player)
		{
			playerDeathCinematic.StartCinematicMode(player);
			float length = 2f;
			attackSource.clip = cinematicClipPool.GetRandomClip();
			attackSource.Play();
			behaviour.timeCanAttackAgain = Time.time + length;
			yield return new WaitForSeconds(length);
			Player.main.liveMixin.Kill(DamageType.Normal);
		}
	}
}
