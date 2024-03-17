using System;
using UnityEngine;

public class MaxHeightTracker : MonoBehaviour
{
    private enum State
    {
        Abovescreen,
        Onscreen,
    }

    private State _state;
    private Vector3 _currentHighestPoint;
    private Player _player;
    private ScreenLabel _activeDisplay;
    private bool _isDisplaying = false;
    [SerializeField] private ScreenLabel _aboveScreenDisplay;
    [SerializeField] private ScreenLabel _onScreenDisplay;
    [SerializeField] private Camera _gameCamera;

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
        Brain.ins.EventHandler.BlockSettledEvent.RemoveListener(OnBlockSettled);
    }

    private void Start()
    {
        _aboveScreenDisplay.Hide();
        _onScreenDisplay.Hide();
    }

    private void OnStartRound(Round round)
    {
        _isDisplaying = true;
    }

    private void OnEndRound(Round round)
    {
        _isDisplaying = false;
        _aboveScreenDisplay.Hide();
        _onScreenDisplay.Hide();
        
        if (_hasChanged)
        {
            Brain.ins.EventHandler.MedalEarnedEvent.Invoke(_player, MedalType.HighestPointReached, Convert.ToInt32(_currentHighestPoint.y));
            _hasChanged = false;
        }
    }

    private void FixedUpdate()
    {
        if (_currentHighestPoint == default || !_isDisplaying)
        {
            return;
        }

        var screenPoint = _gameCamera.WorldToScreenPoint(_currentHighestPoint);
        var newState = GetState(screenPoint);
        if (newState != _state)
        {
            _state = newState;
            if (_activeDisplay != null)
            {
                _activeDisplay.Hide();
            }

            _activeDisplay = _state switch
            {
                State.Abovescreen => _aboveScreenDisplay,
                State.Onscreen => _onScreenDisplay,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (_state == State.Onscreen)
            {
                _activeDisplay.SetHeight(screenPoint.y);
            }

            _activeDisplay.Show();
        }

        _activeDisplay.SetText(StringFormat.FormatDistance(_currentHighestPoint.y));
    }

    private State GetState(Vector3 screenPoint)
    {
        if (screenPoint.y > _gameCamera.pixelHeight)
        {
            return State.Abovescreen;
        }
        else
        {
            return State.Onscreen;
        }
    }

    private bool _hasChanged = false;

    private void OnBlockSettled(Block block)
    {
        var highestPoint = block.GetHighestPoint();
        if (highestPoint.y > _currentHighestPoint.y)
        {
            _hasChanged = true;
            _currentHighestPoint = highestPoint;
            _player = block.Owner;
        }
    }
}