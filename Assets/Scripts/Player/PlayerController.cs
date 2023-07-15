using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rb;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Vector2 _movementInput;

    SpriteRenderer _spriteRenderer;

    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
 
    [SerializeField] private float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    { //If movement input is not 0, try to move
        if (_movementInput != Vector2.zero) {
            bool success = TryMove(_movementInput);
            if (!success && _movementInput.x > 0) {
                success = TryMove(new Vector2(_movementInput.x, 0));
                Debug.LogWarning(success);
            }

            if (!success && _movementInput.y > 0) {
                success = TryMove(new Vector2(0, _movementInput.y));
                Debug.LogWarning(success);
            }
        }

        //Set direction of sprite to movement direction
        if (_movementInput.x < 0)
        {
            _spriteRenderer.flipX = true;
        }else if(_movementInput.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = _rb.Cast(
                direction,
                movementFilter,
                _castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                _rb.MovePosition(_rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
        }

        return false;
    }
    
    void OnMove(InputValue movementValue) {
        _movementInput = movementValue.Get<Vector2>();
    }
}
