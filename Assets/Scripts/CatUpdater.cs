using UnityEngine;

public class CatUpdater : MonoBehaviour {
	
	private CatController catController;
	
	// Use this for initialization
	void Start () {
		catController = transform.parent.GetComponent<CatController>();  
	}

	void OnBecameInvisible(){
		catController.OnBecameInvisible ();
	}

	void GrantCatTheSweetReleaseOfDeath()
	{
		catController.GrantCatTheSweetReleaseOfDeath ();
	}
	
	void UpdateTargetPosition()
	{
		catController.UpdateTargetPosition();
	}
}