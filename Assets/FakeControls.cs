using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FakeControls : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position += Vector3.up * 0.1f;
        }
    }

}
