using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu]
public class Difficulty : ScriptableObject
{
    public List<DifficultyData> _difficultyData;

    public DifficultyData GetDifficultyData(int ante) => _difficultyData[Math.Min(ante - 1, _difficultyData.Count - 1)];
}
