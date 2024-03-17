using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockSpawner : MonoBehaviour
{
    private float _screenLeftBorder, _screenRightBorder = 0.0f;
    private bool _isMovingRight = true;
    private Block _heldBlock;
    private bool _isHoldingBlock => _heldBlock != null;
    private InputAction _actionButton;

    public List<Object> blocks;
    private float _width = 1f;
    private DifficultyData _currentDifficulty;
    private LTDescr _movementTween;

    // Start is called before the first frame update
    void Start()
    {
        CalculateCameraBounds();

        _actionButton = Brain.ins.Controls.FindAction("Everything");
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(IncrementHeight);

        SpawnBlock();
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

    private void IncrementHeight([CanBeNull] Block _)
    {
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
        _movementTween.setSpeed(speed);
    }

    public void SetNextBlockWidth(float width)
    {
        _width = width;
    }

    private Plane plane = new(Vector3.back, Vector3.zero);

    public void SetDifficulty(DifficultyData currentDifficulty, Round round)
    {
        _currentDifficulty = currentDifficulty;
        LeanTween.cancel(_movementTween?.id ?? 0);
        float left = 0;
        float right = 0;

        var leftRay = Camera.main.ScreenPointToRay(new Vector3(0, 0));
        if (plane.Raycast(leftRay, out var distanceL))
        {
            left = leftRay.GetPoint(distanceL).x;
        }

        var rightRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width, 0));
        if (plane.Raycast(rightRay, out var distanceR))
        {
            right = rightRay.GetPoint(distanceR).x;
        }

        _movementTween = LeanTween.value(gameObject, 0, 1, _currentDifficulty.SpawnerSpeedCurve.Evaluate(round.Blocks.Count / round.Height))
            .setOnUpdate((val) =>
            {
                var progress = currentDifficulty.SpawnerPositionCurve.Evaluate(val);
                var newPos = (right - left) * progress + left;
                transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            }).setLoopCount(int.MaxValue).setSpeed(_currentDifficulty.SpawnerSpeedCurve.Evaluate(0));
    }
}