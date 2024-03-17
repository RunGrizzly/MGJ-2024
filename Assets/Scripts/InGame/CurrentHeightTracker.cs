using System.Linq;
using UnityEngine;

public class CurrentHeightTracker : MonoBehaviour
{
    private Vector3 _currentHighestPoint;
    private bool _isDisplaying = false;
    [SerializeField] private ScreenLabel _display;

    private void OnEnable()
    {
        Brain.ins.EventHandler.StartRoundEvent.AddListener(OnStartRound);
        Brain.ins.EventHandler.EndRoundEvent.AddListener(OnEndRound);
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(OnBlockSettled);
    }

    private void OnDisable()
    {
        Brain.ins.EventHandler.StartRoundEvent.RemoveListener(OnStartRound);
        Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnEndRound);
    }

    private void Start()
    {
        _isDisplaying = false;
        _display.Hide();
    }

    private void OnStartRound(Round round)
    {
        _isDisplaying = true;
        _display.Show();
    }

    private void OnEndRound(Round round)
    {
        _isDisplaying = false;
        _display.Hide();
    }

    private void OnBlockSettled(Block block)
    {
        var blocks = Brain.ins.RoundManager.CurrentRound.Blocks;
        var highest = blocks.Select(block => block.GetHighestPoint()).OrderByDescending(f => f.y).First();
        _currentHighestPoint = highest;
        _display.SetText(StringFormat.FormatDistance(highest.y));
    }
}