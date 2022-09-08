using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Camera _cam;
    [SerializeField]private float _moveSpeed;
    private Rigidbody2D _playerRb;

    private Vector2 _direction;

    [HideInInspector] public Vector2 lookDirection;
    [HideInInspector] public Vector2 mousePos;
    
    private float _angle;
    

    void Start ()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        //Getting the mouse position
        mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        _playerRb.MovePosition(_playerRb.position + _direction * _moveSpeed * Time.fixedDeltaTime);

        lookDirection = mousePos - _playerRb.position;
        _angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _playerRb.rotation = _angle;
    }

}
