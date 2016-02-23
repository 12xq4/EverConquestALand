using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {
    Vector3 lastClickPosition;
	Vector3 currentClickPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// if the mouse is over some UI element, let us not let our 
		// non-UI gameobject respond to click.
		if (EventSystem.current != null)
			if (EventSystem.current.IsPointerOverGameObject()) {
				return;	
			}

        // Get the click position from main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {

			currentClickPosition = hitInfo.point;
			currentClickPosition.y = 0;
            // ray hit something
            // we would now detect what we hit.
            GameObject hitObject = hitInfo.collider.transform.gameObject;
			// This is a hack and it is dumb, have the hex script on the top level and hard code it to trace all the way back.

			UpdateCameraPosition (hitObject);
			if (hitObject.transform.GetComponent<Hex>() != null)
				OnMouseOverHex (hitObject);

        }

		Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo2;
		if (Physics.Raycast (ray2, out hitInfo2)) {
			lastClickPosition = hitInfo2.point;
			lastClickPosition.y = 0;
		}

	}

	void UpdateCameraPosition(GameObject hitObject){
		if (Input.GetMouseButton (1) || Input.GetMouseButton (2)) {
			Vector3 diff = lastClickPosition - currentClickPosition;
			Debug.Log (diff);
			Camera.main.transform.Translate (diff, Space.World);
		}
	}

	void OnMouseOverHex(GameObject hitObject){

		if (Input.GetMouseButtonDown(0))
		{
			MeshRenderer mr = hitObject.GetComponentInChildren<MeshRenderer>();
			if (mr.material.color == Color.blue)
				mr.material.color = Color.white;
			else
				mr.material.color = Color.blue;
		}
	}
}
