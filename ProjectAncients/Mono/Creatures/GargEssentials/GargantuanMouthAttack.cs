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
        public bool oneShotPlayer;
        public string attachBoneName;
        public bool canPerformCyclopsCinematic;

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
            playerDeathCinematic.animatedTransform = gameObject.SearchChild(attachBoneName).transform;
            playerDeathCinematic.animator = creature.GetAnimator();
            playerDeathCinematic.animParamReceivers = new GameObject[0];
            playerDeathCinematic.animParam = "cin_player";
        }
        public override void OnTouch(Collider collider) //A long method having to do with interaction with an object and the mouth.
        {
            if (liveMixin.IsAlive() && Time.time > behaviour.timeCanAttackAgain && !playerDeathCinematic.IsCinematicModeActive()) //If it can attack, continue
            {
                Creature thisCreature = gameObject.GetComponent<Creature>();
                if (thisCreature.Aggression.Value >= 0.1f || !canAttackPlayer) //This creature must have at least some level of aggression to bite
                {
                    GameObject target = GetTarget(collider);
                    if (!behaviour.Edible(target))
                    {
                        return;
                    }
                    if (!behaviour.IsHoldingVehicle())
                    {
                        LiveMixin targetLm = target.GetComponent<LiveMixin>();
                        Player player = target.GetComponent<Player>();
                        if (player != null)
                        {
                            if (!canAttackPlayer)
                            {
                                StartCoroutine(PerformBiteAttack(target, 0.5f));
                                return;
                            }
                            if (!player.CanBeAttacked() || !player.liveMixin.IsAlive() || player.cinematicModeActive || !GargantuanBehaviour.PlayerIsKillable())
                            {
                                return;
                            }
                            else
                            {
                                float baseDmg;
                                if (oneShotPlayer)
                                {
                                    baseDmg = 1000f;
                                }
                                else
                                {
                                    baseDmg = 70;
                                }
                                var num = DamageSystem.CalculateDamage(baseDmg, DamageType.Normal, target);
                                if (targetLm.health - num <= 0f) // make sure that the nodamage cheat is not on
                                {
                                    StartCoroutine(PerformPlayerCinematic(player));
                                    return;
                                }
                                else
                                {
                                    StartCoroutine(PerformBiteAttack(target, baseDmg));
                                    behaviour.timeCanAttackAgain = Time.time + 2f;
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
                                thisCreature.Aggression.Value -= 0.5f;
                                return;
                            }
                            Exosuit component5 = target.GetComponent<Exosuit>();
                            if (component5 && !component5.docked)
                            {
                                behaviour.GrabExosuit(component5);
                                thisCreature.Aggression.Value -= 0.5f;
                                return;
                            }
                            if (canPerformCyclopsCinematic)
                            {
                                SubRoot subRoot = target.GetComponent<SubRoot>();
                                if (subRoot && !subRoot.rb.isKinematic && subRoot.live is not null)
                                {
                                    behaviour.GrabLargeSub(subRoot);
                                    thisCreature.Aggression.Value -= 1f;
                                    return;
                                }
                            }
                        }
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
                        else if (canAttackPlayer || (!canAttackPlayer && !behaviour.IsVehicle(target)))
                        {
                            StartCoroutine(PerformBiteAttack(target, GetBiteDamage(target)));
                            behaviour.timeCanAttackAgain = Time.time + 2f;
                            if (canAttackPlayer)
                            {
                                creature.Aggression.Value = 0f;
                            }
                            thisCreature.Aggression.Value -= 0.15f;
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
            return biteDamage; //base damage
        }
        public void OnVehicleReleased() //Called by gargantuan behavior. Gives a cooldown until the next bite.
        {
            behaviour.timeCanAttackAgain = Time.time + 4f;
        }
        private IEnumerator PerformBiteAttack(GameObject target, float damage) //A delayed attack, to let him chomp down first.
        {
            animator.SetFloat("random", UnityEngine.Random.value);
            animator.SetTrigger("bite");
            attackSource.clip = biteClipPool.GetRandomClip();
            attackSource.Play();
            yield return new WaitForSeconds(0.5f);
            if (target) target.GetComponent<LiveMixin>().TakeDamage(damage, transform.position, DamageType.Normal, this.gameObject);
        }
        private IEnumerator PerformPlayerCinematic(Player player)
        {
            if (oneShotPlayer)
            {
                CustomPDALinesManager.PlayPDAVoiceLine(ECCAudio.LoadAudioClip("DeathImminent"), "DeathImminent", "Warning: Death imminent.");
            }
            playerDeathCinematic.enabled = true;
            playerDeathCinematic.StartCinematicMode(player);
            float length = 1.8f;
            attackSource.clip = cinematicClipPool.GetRandomClip();
            attackSource.Play();
            behaviour.timeCanAttackAgain = Time.time + length;
            MainCameraControl.main.ShakeCamera(5f, length, MainCameraControl.ShakeMode.BuildUp); //camera shake doesnt actually work during cinematics
            yield return new WaitForSeconds(length / 3f);
            Player.main.liveMixin.TakeDamage(5f, transform.position, DamageType.Normal, gameObject);
            yield return new WaitForSeconds(length / 3f);
            Player.main.liveMixin.TakeDamage(5f, transform.position, DamageType.Normal, gameObject);
            yield return new WaitForSeconds(length / 3f);
            playerDeathCinematic.enabled = false;
            Player.main.liveMixin.TakeDamage(250f, transform.position, DamageType.Normal, gameObject);
        }
    }
}
