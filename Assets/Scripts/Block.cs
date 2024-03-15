using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _isGrounded, _isReleased;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            Debug.LogError("Objects need a RigidBody");
        }
        Brain.ins.EventHandler.DropBlockEvent.AddListener(Drop);
    }

    void FixedUpdate()
    {
        if (_isGrounded) return;
        if (!_isReleased) return;
        if (!HasComeToAStop()) return;

        _isGrounded = true;
    }

    private void Update()
    {
        if (_isGrounded)
        {
            //EventHandler.BlockDied.Invoke();
        }
    }

    private void Drop()
    {
        transform.parent = null;
        _rigidBody.isKinematic = false;
        _isReleased = true;

        Debug.Log("Dropped a block");
    }

    private bool HasComeToAStop()
    {
        Debug.Log(_rigidBody.velocity.y == 0);
        return _rigidBody.velocity.y == 0;
    }
}
