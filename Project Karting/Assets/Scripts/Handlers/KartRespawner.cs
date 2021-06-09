using Kart;
using SplineEditor.Runtime;
using UnityEngine;

namespace Handlers
{
    public class KartRespawner : MonoBehaviour
    {
        public float distanceForRespawn = 20f;
        
        private bool _initialized;

        public void Init()
        {
            _initialized = true;
        }
        
        private void Update()
        {
            if (!_initialized) return;
            var karts = GameManager.Instance.karts;
            foreach (var kart in karts)
            {
                BezierUtils.BezierPos bezierPos = kart.closestBezierPos;
                if (Vector3.Distance(bezierPos.GlobalOrigin, kart.transform.position) > distanceForRespawn
                    || !kart.IsGrounded() && kart.currentVelocity.magnitude < 0.1f)
                {
                    Respawn(kart);
                }
            }
        }

        public void Respawn(KartBase kart)
        {
            kart.transform.position = kart.lastGroundBezierPos.GlobalOrigin;
            kart.transform.rotation = kart.lastGroundBezierPos.Rotation;
            kart.rigidBody.velocity = Vector3.zero;
            kart.rigidBody.angularVelocity = Vector3.zero;
        }
    }
}