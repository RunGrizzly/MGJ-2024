﻿using System;
using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _blockSpawnerPrefab;
        private GameObject _blockSpawner;

        [SerializeField] private GameObject _floorPrefab, _targetPrefab;
        private GameObject _floor, _target;

        [SerializeField] private CinemachineTargetGroup _targetGroup;
        
        public Difficulty difficulty;

        public BlockSpawner blockSpawnerScript;

        private DifficultyData _currentDifficulty;
        private Round _currentRound;
        private void OnEnable()
        {
            Brain.ins.EventHandler.StartRoundEvent.AddListener(OnStartRound);
            Brain.ins.EventHandler.EndRoundEvent.AddListener(OnEndRound);
            Brain.ins.EventHandler.BlockSettledEvent.AddListener(ApplyDifficulty);
        }

        private void ApplyDifficulty(Block block)
        {
            var normalised = _currentRound.Blocks.Count / _currentRound.Height;
            blockSpawnerScript.SetNextBlockWidth(
                _currentDifficulty.SizeScaler.Evaluate(normalised));
            
            _floor.transform.position = block.transform.position;
        }
        
        private void Start()
        {
            _floor = Instantiate(_floorPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _target = Instantiate(_targetPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            
            _targetGroup.AddMember(_floor.transform, 1f, 0);
            _targetGroup.AddMember(_target.transform.GetChild(0).transform, 1f, 0);
        }

        private void OnStartRound(Round round)
        {
            _currentRound = round;
            _currentDifficulty = difficulty._difficultyData[round.Ante - 1];
            _floor.transform.position = new Vector3(_floor.transform.position.x, round.StartHeight - 3f, _floor.transform.position.z);
            _floor .GetComponent<Collider>().enabled = true;
            
            _target.transform.position = new Vector3(_target.transform.position.x, round.StartHeight + round.Height, _target.transform.position.z);
            
            _blockSpawner = Instantiate(_blockSpawnerPrefab, new Vector3(0f, _target.transform.GetChild(0).transform.position.y, 0f), Quaternion.identity);
            blockSpawnerScript = _blockSpawner.GetComponent<BlockSpawner>();
                blockSpawnerScript.SetSpeed(_currentDifficulty.SpawnerSpeed);
        }

        private void OnEndRound(Round round)
        {
            Destroy(_blockSpawner);
        }

        private void OnDisable()
        {
            Brain.ins.EventHandler.StartRoundEvent.RemoveListener(OnStartRound);
            Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnEndRound);
        }
    }
}