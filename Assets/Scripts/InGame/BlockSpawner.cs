using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BlockSpawner : MonoBehaviour
{
    private float _screenLeftBorder, _screenRightBorder = 0.0f;
    private float _objectWidth;
    private bool _isMovingRight = true;
    private Block _heldBlock;
    private bool _isHoldingBlock => _heldBlock != null;
    private InputAction _actionButton;

    [FormerlySerializedAs("speed")] [SerializeField] private float _speed = 0f;

    public List<Object> blocks;
    private float _width = 1f;

    // Start is called before the first frame update
    void Start()
    {
        CalculateCameraBounds();

        var meshRenderer = GetComponent<MeshRenderer>();
        var size = meshRenderer.bounds.size;
        _objectWidth = size.x;

        _actionButton = Brain.ins.Controls.FindAction("Everything");
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(IncrementHeight);

        SpawnBlock();
        StartMovement();
    }

    void FixedUpdate()
    {
        var positionX = transform.position.x;
        if (positionX >= _screenRightBorder || positionX <= _screenLeftBorder)
        {
            BounceDirection();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            CalculateCameraBounds();
        }

        if (!_actionButton.WasPressedThisFrame() || !_isHoldingBlock) return;

        Brain.ins.EventHandler.DropBlockEvent.Invoke(_heldBlock);
        _heldBlock = null;
    }

    private void CalculateCameraBounds()
    {
        if (Camera.main == null)
        {
            Debug.LogError("MainCamera is not tagged, that's why you can't see anything");
            return;
        }

        var main = Camera.main;
        var position = main.transform.position;
        var orthographicSize = main.orthographicSize;

        _screenLeftBorder = position.x - orthographicSize * Screen.width / Screen.height;
        _screenRightBorder = position.x + orthographicSize * Screen.width / Screen.height;
    }

    private void BounceDirection()
    {
        LeanTween.cancel(gameObject);
        _isMovingRight = !_isMovingRight;

        LeanTween.moveX(gameObject, _isMovingRight ? _screenRightBorder : _screenLeftBorder, _speed * 2);
    }

    private void IncrementHeight([CanBeNull] Block _)
    {
        //LeanTween.cancel(gameObject);
        //LeanTween.moveY(gameObject, transform.position.y + 1f, 0.2f).setOnComplete(StartMovement);

        SpawnBlock();
    }

    private void SpawnBlock()
    {
        var block = blocks[Random.Range(0, blocks.Count - 1)];

        var t = transform;
        var p = t.position;
        _heldBlock = Instantiate(block,
            new Vector3(p.x, p.y - 1f, p.z),
            Quaternion.identity,
            t).GetComponent<Block>();
        _heldBlock.transform.localScale = new Vector3(_width, 1, 1);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetNextBlockWidth(float width)
    {
        _width = width;
    }

    private void StartMovement()
    {
        LeanTween.moveX(gameObject, _isMovingRight ? _screenRightBorder : _screenLeftBorder, _speed);
    }
}