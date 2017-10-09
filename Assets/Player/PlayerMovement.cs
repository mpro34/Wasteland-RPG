using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

	[SerializeField] float walkMoveStopRadius = 0.2f;
    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

	bool isInDirectMode = false; 
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		if (Input.GetKeyDown(KeyCode.G)) //G for gamepad. TODO add to menu
		{
			isInDirectMode = !isInDirectMode; // toggle mode
            currentClickTarget = transform.position; //Clear click target
        }

		if (isInDirectMode)
		{
			ProcessDirectMovement();
		}
		else
		{
        	ProcessMouseMovement ();        
		}
	}

	void ProcessDirectMovement()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// calculate camera relative direction to move:
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 movement = v*cameraForward + h * Camera.main.transform.right;

		thirdPersonCharacter.Move (movement, false, false);

	}

	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit = " + cameraRaycaster.hit.collider.gameObject.name.ToString ());
			switch (cameraRaycaster.currentLayerHit) {
			case Layer.Walkable:
				//Only move on Walkable layer
				currentClickTarget = cameraRaycaster.hit.point;
				// So not set in default case
				break;
			case Layer.Enemy:
				print ("Not moving to enemy");
				break;
			default:
				print ("Unexpected layer found");
				return;
			}
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
			thirdPersonCharacter.Move (playerToClickPoint, false, false);
		} 
		else 
		{
			thirdPersonCharacter.Move (Vector3.zero, false, false);
		}
	}
}

