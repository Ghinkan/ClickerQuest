using ClickerQuest.Combat;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    [CreateAssetMenu(fileName = "MobileClickerBehaviour", menuName = "ClickerBehaviour/MobileClickerBehaviour")]
    public class MobileClickerBehaviour: ClickerBehaviour
    {
        [SerializeField] private float _powerUpTimer;
        private CountdownTimer _timer;
        
        public override void Initialize()
        {
            Camera = Camera.main;
            _timer = new CountdownTimer(_powerUpTimer);
            _timer.OnTimerStop += () => PowerUpActive = false;
        }
        
        public override void ClickBehaviour()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
                    if (hit.transform)
                        if(hit.transform.TryGetComponent(out CharacterInCombat character))
                            character.Clicked(ClickerStats);
                        else if(hit.transform.TryGetComponent(out ClickerPowerUp powerUp))
                            powerUp.Effect();
                }
            }
        }

        public override void ActivatePowerUp()
        {
            PowerUpActive = true;
            _timer.Start();
        }
    }
}