using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace LeviathanEggs.MonoBehaviours
{
    class DroidWelder :  CreatureAction
    {
        List<Leakable> _leakables;
        BaseRoot _base;
        WalkBehaviour _walkBehaviour;
        MeleeAttack _meleeAttack;
        LiveMixin _weldableTarget;
        float _timeToPerform;
        float _emergencyWeld = 3;
        bool _weAreLeaking;

        void Start()
        {
            _walkBehaviour = GetComponent<WalkBehaviour>();
            _meleeAttack = GetComponent<MeleeAttack>();
        }

        void Update()
        {
            if (_base is null)
                return;

            _weAreLeaking = _base.IsLeaking();

            if (_weAreLeaking)
            {
                if (_leakables[0]?.leakingLeakPoints.Count <= 0)
                    _leakables.RemoveAt(0);
                
                if (_leakables.Count > 0)
                    return;
                
                _leakables = _base.flood.leakers.ToList();
            }
        }

        public void SetSubRoot(SubRoot subRoot)
        {
            if (subRoot is null)
                return;

            subRoot.gameObject.TryGetComponent(out _base);
        }

        // called when the object is picked up by the player
        void OnExamine()
        {
            _base = null;
            _leakables = null;
            _weldableTarget = null;
            _weAreLeaking = false;
        }
        
        public override float Evaluate(Creature creature)
        {
            if (_weAreLeaking && Time.time >= _timeToPerform)
            {
                return _emergencyWeld;
            }
            if (_weldableTarget is not null && _weldableTarget.health < _weldableTarget.maxHealth && Time.time >= _timeToPerform)
            {
                return evaluatePriority;
            }

            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            if (_weAreLeaking)
            {
                var pos = _leakables[0].leakingLeakPoints[0].transform.position;
                _walkBehaviour.WalkTo(pos, 4f);
            }
        }

        public override void Perform(Creature creature, float deltaTime)
        {
            if ((_weAreLeaking && Vector3.Distance(transform.position, _leakables[0].leakingLeakPoints[0].transform.position) < 2f && _leakables[0].live.AddHealth(2f) > 0f) || (_weldableTarget && Vector3.Distance(transform.position, _weldableTarget.transform.position) < 2f && _weldableTarget.AddHealth(2f) > 0f))
            {
                WeldingFx();
            }
        }

        public override void StopPerform(Creature creature)
        {
            _weldableTarget = null;
            _timeToPerform = Time.time + 5f;
        }

        void OnCollisionEnter(Collision other)
        {
            if (_weldableTarget)
                return;
            
            var obj = UWE.Utils.GetEntityRoot(other.collider.gameObject);
            obj = obj ? obj : other.collider.gameObject;
            if (obj.TryGetComponent(out LiveMixin lm) && lm.IsWeldable() && lm.health < lm.maxHealth)
                _weldableTarget = lm;
        }

        void WeldingFx()
        {
            var mouth = _meleeAttack.mouth.transform; 
            Instantiate(_meleeAttack.damageFX, mouth.position, mouth.rotation);
            Utils.PlayEnvSound(_meleeAttack.attackSound, mouth.position);
        }
    }
}
