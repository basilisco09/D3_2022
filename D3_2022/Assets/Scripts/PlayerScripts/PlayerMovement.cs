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
        public float moveSpeed;
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
        private Collider2D _collider;
        private bool _canDash = true;
        private bool _isDashing = false;
        private TrailRenderer _trailRenderer;

    #endregion

    #region  Var for invisibility

        public float invisibilityRadius;
        public LayerMask wallLayer;

    #endregion
    
    void Awake()
    {
        _mainCam = Camera.main;
        _playerRb = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(_isDashing)
        {
            StartCoroutine(GetInvisible());
            return;
        }
        
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }

        //Getting the mouse position
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.5f, 0, 0);
    }

    void FixedUpdate()
    {
        if(_isDashing) return;

        _playerRb.MovePosition(_playerRb.position + _direction * moveSpeed * Time.fixedDeltaTime);

        lookDirection = _mousePos - _playerRb.position;
        _angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _playerRb.rotation = _angle;
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        Debug.Log("Is dashing!");
        _playerRb.velocity = _direction * _dashPower;
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashTime);
        _collider.enabled = true;
        _trailRenderer.emitting = false;
        _isDashing = false;
        _canDash = true;
    }

    public IEnumerator MoveBoost(float boost, float duration)
    {
        float normalSpeed = moveSpeed;
        Color normalColor = _trailRenderer.startColor;
        moveSpeed += boost;
        _trailRenderer.startColor = Color.green;
        yield return new WaitForSeconds(duration);
        _trailRenderer.startColor = normalColor;
        moveSpeed = normalSpeed;
    }

    public IEnumerator GetInvisible()
    {
        Collider2D coll = Physics2D.OverlapCircle(this.transform.position, invisibilityRadius, wallLayer);
        if (coll != null)
        {
            _collider.enabled = true;
            yield return null;
        }
        else
        {
            _collider.enabled = false;
            yield return null;
        }
    }
}
