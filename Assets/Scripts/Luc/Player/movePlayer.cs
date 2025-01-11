using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraTransform;
    private Vector3 moveVector;
    private float gravity;
    private Vector3 move;
    private Vector3 moveDirection;
    public playerData _playerData;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private CharacterController _characterController;
    private Vector3 _velocity;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        transform.position = _playerData.GetPosition();
        Debug.Log(transform.position);
    }

    void Update()
    {
        Move();
        Gravity();
       savePosition();
    }
    public void Gravity()
    {
        if (_characterController.isGrounded)
        {
            gravity -= 0;
        }
        else
        {
            gravity -= 1;
        }
        moveVector = new Vector3(0, gravity * .2f * Time.deltaTime, 0);
        _characterController.Move(moveVector);
    }
    public void Move()
    {
        move = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        if (move.magnitude > 0.1f)
        {
            Vector3 cameraForward = _cameraTransform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
            moveDirection = cameraForward * move.z + _cameraTransform.right * move.x;
            _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }
    public void savePosition()
    {

       _playerData.position = transform.position;
    }

}