using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _blockSpawnerPrefab;
        private GameObject _blockSpawner;

        [SerializeField] private MaxHeightTracker _maxHeightTracker;

        private void OnEnable()
        {
            Brain.ins.EventHandler.StartRoundEvent.AddListener(OnStartRound);
            Brain.ins.EventHandler.EndRoundEvent.AddListener(OnEndRound);
        }

        private void Start()
        {
            _blockSpawner = Instantiate(_blockSpawnerPrefab, new Vector3(0f, 6f, 0f), Quaternion.identity);
        }

        private void OnStartRound(Round round)
        {
            _blockSpawner.SetActive(true);
        }

        private void OnEndRound(Round round)
        {
            _blockSpawner.SetActive(false);
        }

        private void OnDisable()
        {
            Brain.ins.EventHandler.StartRoundEvent.RemoveListener(OnStartRound);
            Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnEndRound);
        }
    }
}