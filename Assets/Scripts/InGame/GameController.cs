using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BlockSpawner _blockSpawner;

        private void OnEnable()
        {
            Brain.ins.EventHandler.StartRoundEvent.AddListener(OnStartRound);
            Brain.ins.EventHandler.EndRoundEvent.AddListener(OnEndRound);
        }

        private void Start()
        {
            _blockSpawner.gameObject.SetActive(false);
        }

        private void OnStartRound(Round round)
        {
            _blockSpawner.gameObject.SetActive(true);
        }

        private void OnEndRound(Round round)
        {
            _blockSpawner.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Brain.ins.EventHandler.StartRoundEvent.RemoveListener(OnStartRound);
            Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnEndRound);
        }
    }
}