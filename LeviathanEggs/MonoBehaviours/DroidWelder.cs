using UnityEngine;

namespace LeviathanEggs.MonoBehaviours
{
    class DroidWelder :  CreatureAction
    {
        WalkBehaviour _walkBehaviour;
        MeleeAttack _meleeAttack;
        LiveMixin _weldableTarget;
        float _timeToPerform;

        void Start()
        {
            _walkBehaviour = GetComponent<WalkBehaviour>();
            _meleeAttack = GetComponent<MeleeAttack>();
        }
        
        public override float Evaluate(Creature creature)
        {
            if (_weldableTarget is not null && _weldableTarget.health < _weldableTarget.maxHealth && Time.time >= _timeToPerform)
            {
                return evaluatePriority;
            }

            return 0f;
        }

        public override void StartPerform(Creature creature)
        {
            _walkBehaviour.WalkTo(_weldableTarget.transform.position, 2f);
        }

        public override void Perform(Creature creature, float deltaTime)
        {
            if (_weldableTarget.AddHealth(2f) > 0f)
            {
                var mouth = _meleeAttack.mouth.transform; 
                Instantiate(_meleeAttack.damageFX, mouth.position, mouth.rotation);
                Utils.PlayEnvSound(_meleeAttack.attackSound, mouth.position);
            }
        }

        public override void StopPerform(Creature creature)
        {
            _weldableTarget = null;
            _timeToPerform = Time.time + 5f;
        }

        void Update()
        {
            if (_weldableTarget is not null) // no need to consistently check if we already found a weldable target
                return;

            var targets = UWE.Utils.OverlapSphereIntoSharedBuffer(transform.position, 0.5f);
            for (int i = 0; i < targets; i++)
            {
                var currentTarget = UWE.Utils.sharedColliderBuffer[i];

                var obj = UWE.Utils.GetEntityRoot(currentTarget.gameObject);
                obj = obj ? obj : currentTarget.gameObject;
                if (obj.TryGetComponent(out LiveMixin lm) && lm.IsWeldable() && lm.health < lm.maxHealth)
                    _weldableTarget = lm;
            }
        }
    }
}