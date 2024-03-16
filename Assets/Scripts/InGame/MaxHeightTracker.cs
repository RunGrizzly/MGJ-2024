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
        private GameObject _activeDisplay;
        [SerializeField] private GameObject _aboveScreenDisplay;
        [SerializeField] private GameObject _onScreenDisplay;
        [SerializeField] private Camera _gameCamera;

        private void OnEnable()
        {
            Brain.ins.EventHandler.BlockSettledEvent.AddListener(OnBlockSettled);
        }

        private void OnDisable()
        {
            Brain.ins.EventHandler.BlockSettledEvent.RemoveListener(OnBlockSettled);
        }

        private void FixedUpdate()
        {
            var screenPoint = _gameCamera.WorldToScreenPoint(_currentHighestPoint);
            var newState = GetState(screenPoint);
            if (newState != _state)
            {
                _state = newState;
                if (_activeDisplay != null)
                {
                    _activeDisplay.SetActive(false);
                }

                _activeDisplay = _state switch
                {
                    State.Abovescreen => _aboveScreenDisplay,
                    State.Onscreen => _onScreenDisplay,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                _activeDisplay.SetActive(true);
            }
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

        private void OnBlockSettled(Block block)
        {
            var highestPoint = block.GetHighestPoint();
            if (highestPoint.y > _currentHighestPoint.y)
            {
                _currentHighestPoint = highestPoint;
                _player = block.Owner;
                
                Brain.ins.EventHandler.MedalEarnedEvent.Invoke(_player, MedalType.MostBlocks);
            }    
        }
        
    }
