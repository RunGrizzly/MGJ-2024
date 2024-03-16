using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockSpawner : MonoBehaviour
{
    private float _screenLeftBorder, _screenRightBorder = 0.0f;
    private float _objectWidth;
    private bool _isMovingRight = true;
    private InputAction _actionButton;
    
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

        _actionButton = Brain.ins.Controls.FindAction("Everything");
        EventHandler.BlockSettled.AddListener(SpawnBlock);
        
        SpawnBlock(null);
        StartMovement();
    }
    
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
        if (_actionButton.WasPressedThisFrame())
        {
           EventHandler.DropBlock.Invoke();
           LeanTween.cancel(gameObject);
           
           var pos = transform.position;
           LeanTween.move(gameObject, new Vector3 { x = 0, y = pos.y + 1, z = pos.z}, 0.2f);
        }
    }

    private void BounceDirection()
    {
        LeanTween.cancel(gameObject);
        _isMovingRight = !_isMovingRight;

        LeanTween.moveX(gameObject, _isMovingRight ? _screenRightBorder : _screenLeftBorder, speed * 2);
    }
    
    private void SpawnBlock([CanBeNull] GameObject _)
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