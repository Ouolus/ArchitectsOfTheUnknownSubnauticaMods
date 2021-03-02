using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace ProjectAncients.Mono
{
    public class GargantuanMouthAttack : MeleeAttack
    {
		private AudioSource attackSource;
		private ECCAudio.AudioClipPool biteClipPool;
		private GargantuanBehaviour behaviour;

		void Start()
		{
			attackSource = gameObject.AddComponent<AudioSource>();
			attackSource.minDistance = 10f;
			attackSource.maxDistance = 40f;
			attackSource.spatialBlend = 1f;
			attackSource.volume = ECCHelpers.GetECCVolume();
			biteClipPool = ECCAudio.CreateClipPool("GargBiteAttack");
			gameObject.SearchChild("Mouth").EnsureComponent<OnTouch>().onTouch = new OnTouch.OnTouchEvent();
			gameObject.SearchChild("Mouth").EnsureComponent<OnTouch>().onTouch.AddListener(OnTouch);
			behaviour = GetComponent<GargantuanBehaviour>();
		}
		public override void OnTouch(Collider collider) //A long method having to do with interaction with an object and the mouth.
		{
			if (frozen) //Stasis rifle = no attack
			{
				return;
			}
			if (liveMixin.IsAlive() && Time.time > behaviour.timeCanAttackAgain) //If it can attack, continue
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
							if (!player.CanBeAttacked() || !player.liveMixin.IsAlive() || player.cinematicModeActive)
							{
								return;
							}
						}
						else if (behaviour.GetCanGrabVehicle())
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
						LiveMixin liveMixin = target.GetComponent<LiveMixin>();
						if (liveMixin == null) return;
						if (!liveMixin.IsAlive())
						{
							return;
						}
						if (!CanAttackTargetFromPosition(target))
						{
							return;
						}
						else
						{
							StartCoroutine(PerformBiteAttack(target));
							this.behaviour.timeCanAttackAgain = Time.time + 2f;
							attackSource.clip = biteClipPool.GetRandomClip();
							attackSource.Play();
						}
						creature.GetAnimator().SetTrigger("bite");
						thisCreature.Aggression.Value -= 0.15f;
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
				return 300f; //cyclops damage
			}
			return 100f; //base damage
		}
		public void OnVehicleReleased() //Called by gargantuan behavior. Gives a cooldown until the next bite.
		{
			behaviour.timeCanAttackAgain = Time.time + 4f;
		}
		private IEnumerator PerformBiteAttack(GameObject target) //A delayed attack, to let him chomp down first.
		{
			yield return new WaitForSeconds(0.5f);
			if(target) liveMixin.TakeDamage(GetBiteDamage(target));
		}
	}
}
