using System;
using Unity.VisualScripting;
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

    void OnCollisionStay(Collision collisionInfo)
    {
        if (!_isGrounded)
        {
            if (collisionInfo.gameObject.layer == 6)
            {
                _isGrounded = true;
                EventHandler.BlockSettled.Invoke(gameObject);
            }
        }
    }
}
