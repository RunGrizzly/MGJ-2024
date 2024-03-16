using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private MaxHeightTracker _maxHeightTracker;

    private void OnEnable()
    {
        Brain.ins.EventHandler.StartRoundEvent.AddListener(OnStartRound);
        Brain.ins.EventHandler.EndRoundEvent.AddListener(OnEndRound);
    }

    private void Start()
    {
        // _blockSpawner.gameObject.SetActive(false);
        // _maxHeightTracker.gameObject.SetActive(false);
    }

    private void OnStartRound(Round round)
    {
        _blockSpawner.gameObject.SetActive(true);
        _maxHeightTracker.gameObject.SetActive(true);
    }

    private void OnEndRound(Round round)
    {
        _blockSpawner.gameObject.SetActive(false);
        _maxHeightTracker.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Brain.ins.EventHandler.StartRoundEvent.RemoveListener(OnStartRound);
        Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnEndRound);
    }
}