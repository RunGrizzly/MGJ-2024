using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _settled;
    public bool Settled => _settled;

    private bool _settling;
    
    [SerializeField] private float timeToSettle = 3.0f;
    private float _settleTimer;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            Debug.LogError("Objects need a RigidBody");
        }

        _settleTimer = timeToSettle;
        Brain.ins.EventHandler.DropBlockEvent.AddListener(Drop);
    }

    private void Update()
    {
        CheckIfSettled();
    }

    private void Drop()
    {
        transform.parent = null;
        _rigidBody.isKinematic = false;
    }

    private void CheckIfSettled()
    {
        if (!_settling) return;
        
        _settleTimer -= Time.deltaTime;

        if (_settleTimer > 0) return;
        
        _settling = false;
        _settled = true;
        Brain.ins.EventHandler.BlockSettledEvent.Invoke(this);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (_settled) return;
        
        if (other.gameObject.layer == 6)
        {
            _settling = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (_settled || !_settling) return;
        
        _settling = false;
        _settleTimer = timeToSettle;
    }


}
