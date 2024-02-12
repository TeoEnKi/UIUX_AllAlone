using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    public float rotationSpeed = 10f;

    float horizontal;
    float vertical;
    float jump;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from horizontal and vertical axes
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, jump, vertical);

        // Normalize the movement vector to maintain constant speed in all directions
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player
        characterController.Move(movement);
        RotateToCamera();
    }

    private void RotateToCamera()
    {
//Get the Screen positions of the object
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		
		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, mouseOnScreen, speed * Time.deltaTime, 0.0f);

        //Ta Daaa
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}