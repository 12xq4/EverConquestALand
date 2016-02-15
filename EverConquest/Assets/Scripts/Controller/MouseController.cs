using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
    Vector3 lastClickPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Get the click position from 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {
            // ray hit something
            // we would now detect what we hit.
            GameObject hitObject = hitInfo.collider.transform.gameObject;
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
}
