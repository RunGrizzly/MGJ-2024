using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardLabel : MonoBehaviour
{
    //   [SerializeField] private Camera _lbCamera = null;
    [field: SerializeField] public TextMeshProUGUI NameBox { get; set; } = null;

    // void FixedUpdate()
    // {
    //     transform.rotation = Quaternion.LookRotation(_lbCamera.transform.position - transform.position);
    // }
}
