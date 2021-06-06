using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;
using ECCLibrary.Internal;
using RotA.Prefabs;

namespace RotA.Mono
{
    public class GargantuanMouthAttack : MeleeAttack
    {
        private AudioSource attackSource;
        private ECCAudio.AudioClipPool biteClipPool;
        private ECCAudio.AudioClipPool cinematicClipPool;
        private GargantuanBehaviour behaviour;
        public GameObject throat;
        private GargantuanRoar roar;

        private PlayerCinematicController playerDeathCinematic;

        public bool canAttackPlayer = true;
        public bool oneShotPlayer;
        public string attachBoneName;
        public bool canPerformCyclopsCinematic;
        public GargGrabFishMode grabFishMode;

        void Start()
        {
            attackSource = gameObject.AddComponent<AudioSource>();
            attackSource.minDistance = 10f;
            attackSource.maxDistance = 40f;
            attackSource.spatialBlend = 1f;
            attackSource.volume = ECCHelpers.GetECCVolume();
            attackSource.playOnAwake = false;
            biteClipPool = ECCAudio.CreateClipPool("GargBiteAttack");
            cinematicClipPool = ECCAudio.CreateClipPool("GargBiteAttack5");
            throat = gameObject.SearchChild("Head");
            gameObject.SearchChild("Mouth").EnsureComponent<OnTouch>().onTouch = new OnTouch.OnTouchEvent();
            gameObject.SearchChild("Mouth").EnsureComponent<OnTouch>().onTouch.AddListener(OnTouch);
            behaviour = GetComponent<GargantuanBehaviour>();
            roar = GetComponent<GargantuanRoar>();

            playerDeathCinematic = gameObject.AddComponent<PlayerCinematicController>();
            playerDeathCinematic.animatedTransform = gameObject.SearchChild(attachBoneName).transform;
            playerDeathCinematic.animator = creature.GetAnimator();
            playerDeathCinematic.animParamReceivers = new GameObject[0];
            playerDeathCinematic.animParam = "cin_player";
            playerDeathCinematic.playerViewAnimationName = "seadragon_attack";
        }
        public override void OnTouch(Collider collider) //A long method having to do with interaction with an object and the mouth.
        {
            if (liveMixin.IsAlive() && Time.time > behaviour.timeCanAttackAgain && !playerDeathCinematic.IsCinematicModeActive()) //If it can attack, continue
            {
                Creature gargantuan = gameObject.GetComponent<Creature>();
                if (gargantuan.Aggression.Value >= 0.1f || !canAttackPlayer) //This creature must have at least some level of aggression to bite
                {
                    GameObject target = GetTarget(collider);
                    if (!behaviour.CanEat(target))
                    {
                        return;
                    }
                    if (!behaviour.IsHoldingVehicle())
                    {
                        LiveMixin targetLm = target.GetComponent<LiveMixin>();
                        Player player = target.GetComponent<Player>();
                        if (player != null)
                        {
                            if (!player.CanBeAttacked() || !player.liveMixin.IsAlive() || player.cinematicModeActive || !GargantuanBehaviour.PlayerIsKillable())
                            {
                                return;
                            }
                            if (!canAttackPlayer)
                            {
                                //gargantuan baby nibble behavior
                                Pickupable held = Inventory.main.GetHeld();
                                if (held is not null && held.GetComponent<Creature>() != null)
                                {
                                    LiveMixin heldLm = held.GetComponent<LiveMixin>();
                                    if (heldLm.maxHealth < 100f)
                                    {
                                        animator.SetFloat("random", UnityEngine.Random.value);
                                        animator.SetTrigger("bite");
                                        attackSource.clip = biteClipPool.GetRandomClip();
                                        attackSource.Play();
                                        Destroy(held.gameObject);
                                    }
                                }
                                else
                                {
                                    StartCoroutine(PerformBiteAttack(target, 1f));
                                }
                                behaviour.timeCanAttackAgain = Time.time + 1f;
                                return;
                            }
                            else
                            {
                                //attack player normally
                                float baseDmg;
                                if (oneShotPlayer)
                                {
                                    baseDmg = 1000f;
                                }
                                else
                                {
                                    baseDmg = 80;
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
                            //try to perform vehicle attack
                            SeaMoth seamoth = target.GetComponent<SeaMoth>();
                            if (seamoth && !seamoth.docked)
                            {
                                behaviour.GrabGenericSub(seamoth);
                                gargantuan.Aggression.Value -= 0.5f;
                                return;
                            }
                            Exosuit exosuit = target.GetComponent<Exosuit>();
                            if (exosuit && !exosuit.docked)
                            {
                                behaviour.GrabExosuit(exosuit);
                                gargantuan.Aggression.Value -= 0.5f;
                                return;
                            }
                            if (canPerformCyclopsCinematic)
                            {
                                SubRoot subRoot = target.GetComponent<SubRoot>();
                                if (subRoot && !subRoot.rb.isKinematic && subRoot.live is not null)
                                {
                                    behaviour.GrabLargeSub(subRoot);
                                    behaviour.roar.PlayOnce(out _, GargantuanRoar.RoarMode.CloseOnly);
                                    gargantuan.Aggression.Value -= 1f;
                                    return;
                                }
                            }
                        }
                        if (targetLm == null) return;
                        if (!targetLm.IsAlive())
                        {
                            return;
                        }
                        if (grabFishMode == GargGrabFishMode.LeviathansOnlyAndSwallow || grabFishMode == GargGrabFishMode.LeviathansOnlyNoSwallow) //leviathan attack animation
                        {
                            Creature otherCreature = target.GetComponent<Creature>();
                            if (otherCreature is not null && otherCreature.liveMixin.IsAlive() && (otherCreature is GhostLeviathan || otherCreature is GhostLeviatanVoid || otherCreature is ReaperLeviathan || otherCreature is SeaDragon))
                            {
                                gargantuan.Aggression.Value -= 0.6f;
                                gargantuan.Hunger.Value = 0f;
                                otherCreature.flinch = 1f;
                                otherCreature.Scared.Value = 1f;
                                behaviour.GrabFish(otherCreature.gameObject);
                                return;
                            }
                        }
                        else if (grabFishMode == GargGrabFishMode.PickupableOnlyAndSwalllow) //baby "play with food" animation
                        {
                            Creature otherCreature = target.GetComponent<Creature>();
                            if (otherCreature is not null && otherCreature.liveMixin.IsAlive() && otherCreature.gameObject.GetComponent<Pickupable>() is not null)
                            {
                                gargantuan.Aggression.Value -= 0.6f;
                                gargantuan.Hunger.Value = 0f;
                                behaviour.GrabFish(otherCreature.gameObject);
                                otherCreature.flinch = 1f;
                                otherCreature.Scared.Value = 1f;
                                otherCreature.transform.localScale = Vector3.one * 0.5f;
                                otherCreature.liveMixin.TakeDamage(1f, otherCreature.transform.position);
                                return;
                            }
                        }
                        if (!CanAttackTargetFromPosition(target))
                        {
                            return;
                        }
                        if (behaviour.CanSwallowWhole(target, targetLm))
                        {
                            creature.GetAnimator().SetTrigger("bite");
                            gargantuan.Hunger.Value -= 0.15f;
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
                            gargantuan.Aggression.Value -= 0.15f;
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
            if (target is not null)
            {
                var targetLm = target.GetComponent<LiveMixin>();
                if (targetLm)
                {
                    targetLm.TakeDamage(damage, transform.position, DamageType.Normal, this.gameObject);
                    if (!targetLm.IsAlive())
                    {
                        creature.Aggression.Value = 0f;
                        creature.Hunger.Value = 0f;
                    }
                }
            }
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
