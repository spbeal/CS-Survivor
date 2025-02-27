using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script requires the component CharacterController to work
[RequireComponent(typeof(CharacterController))]

// Dynamic binding stuff, prints messages when the player moves from 'sub'
public class sup {
	public virtual void printDir(){
		if(Input.GetAxis("Vertical") != 0){
			Debug.Log("Player moved vertically (sup)");
		}
			
		if(Input.GetAxis("Horizontal") != 0){
			Debug.Log("Player moved horizontally (sup)");
		}
	}
}

public class sub : sup {
	public override void printDir(){
		if(Input.GetAxis("Vertical") != 0){
			Debug.Log("Player moved vertically (sub)");
		}
		
		if(Input.GetAxis("Horizontal") != 0){
			Debug.Log("Player moved horizontally (sub)");
		}
	}
}



public class Movement : MonoBehaviour
{
	// Dynamic binding object
	sup object1 = new sub();
	
	// Customizable the player's camera, their walk speed, their look speed,
	// and their look Y limit
	[SerializeField]
	private Camera playerCamera;

	private PlayerStats playerStats;
	//Me change this to public  walkSpeed
	
	[SerializeField]
	private float lookSpeed = 7f;
	
	[SerializeField]
	private float lookYLimit = 100f;
	
	// Vector tracking movement direction
	[SerializeField]
	public Vector3 moveDirection = Vector3.zero;
    //Me change this to public

    private float rotationX = 0;
	
	// The required CharacterController object
	CharacterController characterController;
	
	[SerializeField]
	private float vSpeed = 0; // Vertical velocity
	
	[SerializeField]
	private float gravity = 9.81f;
	
    // Start is called before the first frame update
    void Start()
    {
		// Stops the player from seeing their cursor in the game
        characterController = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {		
		// Sets up directional movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);

		float walkSpeed = playerStats.GetSpeed();
		
		float speedX = walkSpeed * Input.GetAxis("Vertical");
		float speedY = walkSpeed * Input.GetAxis("Horizontal");
		
		moveDirection = (forward * speedX) + (right * speedY);
		
		// Sets up camera movement
		rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
		rotationX = Mathf.Clamp(rotationX, -lookYLimit, lookYLimit);
		
		playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
		transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
		
		// Sets up gravity
		if (characterController.isGrounded){
			vSpeed = 0;
		}
		vSpeed -= gravity * Time.deltaTime;
		moveDirection.y = vSpeed;
		characterController.Move(moveDirection * Time.deltaTime);
		
		//Dynamic binding
		//object1.printDir();
		
		
	}
}