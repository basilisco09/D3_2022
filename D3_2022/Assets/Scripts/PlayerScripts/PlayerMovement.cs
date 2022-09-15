using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Var for control

        private Camera _mainCam;
        private Transform _playerTransform;

    #endregion

    #region Var for movement

        private Rigidbody2D _playerRb;
        [SerializeField] private float _moveSpeed;
        private Vector2 _direction;

    #endregion

    #region Var for aim

        [HideInInspector] public Vector2 lookDirection;
        private float _angle;
        private Vector2 _mousePos;

    #endregion 

    #region Var for dash

        [SerializeField] private float _dashPower;
        [SerializeField] private float _dashTime;
        [SerializeField] private float _dashCooldown;
        private bool _canDash = true;
        private bool _isDashing = false;
        private TrailRenderer _trailRenderer;

    #endregion
    
    void Awake()
    {
        _mainCam = Camera.main;
        _playerRb = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if(_isDashing) return;
        
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }

        //Getting the mouse position
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if(_isDashing) return;

        //if(!_inventoryMenu.InventoryIsOpen)
        {
            _playerRb.MovePosition(_playerRb.position + _direction * _moveSpeed * Time.fixedDeltaTime);

            lookDirection = (_mousePos - _playerRb.position).normalized;
            _angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 88.5f;
            _playerRb.rotation = _angle;
        }    
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        Debug.Log("Is dashing!");
        _playerRb.velocity = lookDirection * _dashPower;
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        _canDash = true;
    }
}
