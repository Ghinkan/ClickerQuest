using ClickerQuest.Combat;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    [CreateAssetMenu(fileName = "PCClickerBehaviour", menuName = "ClickerBehaviour/PCClickerBehaviour")]
    public class PCClickerBehaviour: ClickerBehaviour
    {
        [SerializeField] private Texture2D _defaultCursor;
        [SerializeField] private Texture2D _attackCursor;
        [SerializeField] private Texture2D _healCursor;
        
        //TODO: Fix Offset of cursor
        private Vector2 _hotSpot;
        
        [SerializeField] private float _powerUpTimer;
        private CountdownTimer _timer;

        public override void Initialize()
        {
            Camera = Camera.main;
            _hotSpot = Vector2.zero;
            Cursor.SetCursor(_defaultCursor, _hotSpot, CursorMode.Auto);
            _timer = new CountdownTimer(_powerUpTimer);
            _timer.OnTimerStop += () => PowerUpActive = false;
        }
        
        public override void ClickBehaviour()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform)
            {
                if (hit.transform.gameObject.CompareTag("Hero"))
                    Cursor.SetCursor(_healCursor, _hotSpot, CursorMode.Auto);
                else if (hit.transform.gameObject.CompareTag("Enemy"))
                    Cursor.SetCursor(_attackCursor, _hotSpot, CursorMode.Auto);
            }
            else
                Cursor.SetCursor(_defaultCursor, _hotSpot, CursorMode.Auto);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform)
                    if(hit.transform.TryGetComponent(out CharacterInCombat character))
                        character.Clicked(ClickerStats);
                    else if(hit.transform.TryGetComponent(out ClickerPowerUp powerUp))
                        powerUp.Effect();
            }
        }
        
        public override void ActivatePowerUp()
        {
            PowerUpActive = true;
            _timer.Start();
        }
    }
}