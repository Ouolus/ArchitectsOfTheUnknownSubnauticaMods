using UnityEngine;
using UnityEngine.Events;

namespace ProjectAncients.Mono.AlienTech
{
    /// <summary>
    /// A simple piece of code to be attached to Alien Bases. All fields should be set for it to work properly. Has a callback for playing voicelines and stuff.
    /// </summary>
    public class AlienBasePlayerTrigger : MonoBehaviour
    {
        public GameObject triggerObject;

        public class OnTriggered : UnityEvent<GameObject>
        {
        }

        public OnTriggered onTrigger;

        void Start()
        {
            if (triggerObject == null)
            {
                ECCLibrary.Internal.ECCLog.AddMessage("AlienBasePlayerTrigger.triggerObject is null");
            }
            OnTouch onTouch = triggerObject.AddComponent<OnTouch>();
            onTouch.onTouch = new OnTouch.OnTouchEvent();
            onTouch.onTouch.AddListener(OnTouch);
        }

        public void OnTouch(Collider collider)
        {
            if (collider.GetComponentInParent<Player>() is not null)
            {
                if (onTrigger == null)
                {
                    ECCLibrary.Internal.ECCLog.AddMessage("AlienBasePlayerTrigger.onTrigger is null");
                }
                onTrigger.Invoke(gameObject);
            }
        }
    }
}
