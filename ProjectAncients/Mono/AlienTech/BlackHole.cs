using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono.AlienTech
{
    public class BlackHole : HandTarget, IHandTarget
    {
        int _attempts = 3;
        float _clickCooldown;
        
        public void OnHandHover(GUIHand hand)
        {
            HandReticle.main.SetIcon(HandReticle.IconType.HandDeny);
            HandReticle.main.SetInteractText("Do not touch");
        }

        public void OnHandClick(GUIHand hand)
        {
            if (Time.time > _clickCooldown)
                return;
            
            if (_attempts is not 0)
            {
                ErrorMessage.AddMessage("Do not touch mate");
                _attempts--;
                _clickCooldown = Time.time + 5;
                return;
            }
            
            ErrorMessage.AddMessage("Time to crash lol");
            StartCoroutine(Crash());
        }

        IEnumerator Crash()
        {
            IngameMenu.main.mainPanel.SetActive(false);
            yield return IngameMenu.main.SaveGameAsync();
            
            yield return IngameMenu.main.QuitGameAsync(true);
        }
    }
}