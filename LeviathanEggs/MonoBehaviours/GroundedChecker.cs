using UnityEngine;
using WorldStreaming;

namespace LeviathanEggs.MonoBehaviours
{
    class GroundedChecker : OnSurfaceTracker
    {
        public override Vector3 surfaceNormal => _surfaceNormal;

        bool CheckIsGrounded(Collision other)
        {
            var contacts = other.contacts;

            if (other.collider.GetComponentInParent<Voxeland>() != null ||
                other.collider.GetComponentInParent<ClipmapChunk>() != null)
            {
                int i = 0;
                while (i < contacts.Length)
                {
                    if (Vector3.Dot(Vector3.up, contacts[i].normal) >= minSurfaceCos)
                    {
                        _surfaceNormal = contacts[i].normal;
                        return true;
                    }
                    i++;
                }
            }

            return false;
        }
        
        void OnCollisionEnter(Collision collision)
        {
            _onSurface = CheckIsGrounded(collision);
        }

        public override void OnCollisionStay(Collision collision)
        {
            _onSurface = CheckIsGrounded(collision);
        }
    }
}