using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardLabel : MonoBehaviour
{
    //   [SerializeField] private Camera _lbCamera = null;
    [field: SerializeField] public TextMeshProUGUI NameBox { get; set; } = null;

    private Round _round;

    private void OnEnable()
    {
        Brain.ins.EventHandler.RoundLostEvent.AddListener(OnRoundLost);
    }

    private void OnRoundLost(Round round)
    {
        if (round == _round)
        {
            Destroy(gameObject);
        }
    }
    // void FixedUpdate()
    // {
    //     transform.rotation = Quaternion.LookRotation(_lbCamera.transform.position - transform.position);
    // }
}
