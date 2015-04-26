using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ZombieController : MonoBehaviour {

	public AudioClip enemyContactSound;
	public AudioClip catContactSound;

	public float moveSpeed;
	public float turnSpeed;

	private bool isInvinsible = false;
	private float timeSpentInvinsible;

	private Vector3 moveDirection;
	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int currentColliderIndex = 0;

	public List<Transform> congaLine = new List<Transform>();

	public static int lives = 3;


	// Use this for initialization
	void Start () {
	
		moveDirection = Vector3.right;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (congaLine.Count >= 5) {
			Application.LoadLevel ("WinScene");
		}

		if (other.CompareTag ("cat")) {
			Transform followTarget = congaLine.Count == 0 ? transform : congaLine [congaLine.Count - 1];
			other.transform.parent.GetComponent<CatController> ().JoinConga (followTarget, moveSpeed, turnSpeed);
			//other.GetComponent<CatController>().JoinConga( followTarget, moveSpeed, turnSpeed);
			congaLine.Add (other.transform);

			GetComponent<AudioSource>().PlayOneShot(catContactSound);
		} 
		else if (!isInvinsible && other.CompareTag ("enemy")) {
			isInvinsible = true; timeSpentInvinsible = 0;
			for (int i = 0; i <2 && congaLine.Count > 0; i++) {
				int lastIdx = congaLine.Count - 1;
				Transform cat = congaLine [lastIdx];
				congaLine.RemoveAt (lastIdx);
				cat.parent.GetComponent<CatController> ().ExitConga ();

			}
			GetComponent<AudioSource>().PlayOneShot(enemyContactSound);

			if (--lives <= 0) {
				Application.LoadLevel ("LoseScene");
			}

		}
	}


	public void SetColliderForSprite(int spriteNum)
	{
		colliders [currentColliderIndex].enabled = false;
		currentColliderIndex = spriteNum;
		colliders [currentColliderIndex].enabled = true;
	}

	private void EnfornceBounds()
	{
		Vector3 newPosition = transform.position;
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		float xDist = mainCamera.aspect * mainCamera.orthographicSize;
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;
		float yMax = mainCamera.orthographicSize;

		if (newPosition.x <xMin || newPosition.x > xMax) {
			newPosition.x = Mathf.Clamp (newPosition.x, xMin, xMax);
			moveDirection.x = -moveDirection.x; 
		}

		if (newPosition.y < -yMax || newPosition.y > yMax) {
			newPosition.y = Mathf.Clamp (newPosition.y, -yMax, yMax);
			moveDirection.y = -moveDirection.y;
		}
		transform.position = newPosition;

	}

	// Update is called once per frame
	void Update () {
	
		Vector3 currentPosition = transform.position;

		if (Input.GetButton("Fire1")) {

			Vector3 moveToward = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0;
			moveDirection.Normalize();
		}
			Vector3 target = moveDirection * moveSpeed + currentPosition;
			transform.position = Vector3.Lerp (currentPosition,target,Time.deltaTime);


		float targetAngle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation =
			Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);

		EnfornceBounds ();

		if (isInvinsible)
		{
			timeSpentInvinsible += Time.deltaTime;

			if (timeSpentInvinsible < 3f) {
				float remainder = timeSpentInvinsible % .3f;
				GetComponent<Renderer>().enabled = remainder >0.15f;
			}

			else {
				GetComponent<Renderer>().enabled = true;
				isInvinsible = false;
			}
		}
	}

}