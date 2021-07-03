using UnityEngine;

namespace CreatureEggs.MonoBehaviours
{
    class WalkingManager : MonoBehaviour
    {
        public Animator animator;
        
        GroundedChecker _groundedChecker;
        Rigidbody _rigidBody;

        void Start()
        {
            _groundedChecker = GetComponent<GroundedChecker>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            SafeAnimator.SetBool(animator, "on_ground", _groundedChecker.onSurface);
            if (_groundedChecker.onSurface)
            {
                Vector3 direction = Vector3.ProjectOnPlane(_rigidBody.velocity, _groundedChecker.surfaceNormal);
                Vector3 vector = transform.InverseTransformDirection(direction);
                SafeAnimator.SetFloat(animator, "move_speed_horizontal", vector.x);
                SafeAnimator.SetFloat(animator, "move_speed_vertical", vector.z);
            }
        }
    }
}