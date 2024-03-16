using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Player Owner { get; private set; }
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
        Owner = Brain.ins.RoundManager.CurrentRound.Player;
        Brain.ins.EventHandler.DropBlockEvent.AddListener(Drop);
    }

    private void FixedUpdate()
    {
        CheckIfSettled();
    }

    private void Drop(Block _)
    {
        transform.parent = null;
        _rigidBody.isKinematic = false;
    }

    private void CheckIfSettled()
    {
        if (!_settling) return;

        _settleTimer -= Time.fixedDeltaTime;

        if (_settleTimer > 0) return;

        _settling = false;
        _settled = true;
        Brain.ins.EventHandler.BlockSettledEvent.Invoke(this);
        Brain.ins.EventHandler.DropBlockEvent.RemoveListener(Drop);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (!_rigidBody.isKinematic)
            {
                Brain.ins.RoundManager.EndRound(false);
                other.enabled = false;
            }
        }
    }

    public void FreezeBlock()
    {
        _rigidBody.isKinematic = true;
    }
    
    public Vector3 GetHighestPoint()
    {
        return _rigidBody.ClosestPointOnBounds(new Vector3(0, 100f, 0));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}