using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude = 1f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float rotationSpeed = 5f;

    private Camera mainCam;
    private Rigidbody rb;

    private Vector3 movementDirection;

    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PrcoessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }
        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void PrcoessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCam.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }

        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = mainCam.WorldToViewportPoint(transform.position);

        if(viewportPosition.x >1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    private void RotateToFaceVelocity()
    {
        if (rb.velocity == Vector3.zero) { return; }

        Quaternion newRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(
            transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
}