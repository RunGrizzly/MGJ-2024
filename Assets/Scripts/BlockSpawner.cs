using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockSpawner : MonoBehaviour
{
    private float _screenLeftBorder, _screenRightBorder = 0.0f;
    private float _objectWidth;
    private bool _isMovingRight = true;
    private bool _isHoldingBlock = false;
    private InputAction actionButton;
    
    [SerializeField]
    private float speed = 0f;

    public List<Object> blocks;
    
    // Start is called before the first frame update
    void Start()
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

        var meshRenderer = GetComponent<MeshRenderer>();
        var size = meshRenderer.bounds.size;
        _objectWidth = size.x;

        actionButton = Brain.ins.Controls.FindAction("Everything");
        Brain.ins.EventHandler._blockDiedEvent.AddListener(SpawnBlock);
        
        SpawnBlock();
        StartMovement();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var positionX = transform.position.x;
        if (positionX + _objectWidth / 2 >= _screenRightBorder || positionX - _objectWidth / 2 <= _screenLeftBorder)
        {
            BounceDirection();
        }
    }

    void Update()
    {
        if (actionButton.WasPressedThisFrame())
        {
           Brain.ins.EventHandler._dropBlockEvent.Invoke();
           LeanTween.cancel(gameObject);
           LeanTween.moveX(gameObject, 0f, 0.2f);
        }
    }

    private void BounceDirection()
    {
        LeanTween.cancel(gameObject);
        _isMovingRight = !_isMovingRight;

        LeanTween.moveX(gameObject, _isMovingRight ? _screenRightBorder : _screenLeftBorder, speed * 2);
    }
    
    private void SpawnBlock()
    {
        var block = blocks[Random.Range(0, blocks.Count - 1)];

        var t = transform;
        var p = t.position;
        Instantiate(block, 
            new Vector3(p.x, p.y - 1f, p.z), 
            Quaternion.identity,
            t);
        
        StartMovement();
    }

    private void StartMovement()
    {
        LeanTween.moveX(gameObject, _isMovingRight ? _screenRightBorder : _screenLeftBorder, speed);
    }
}