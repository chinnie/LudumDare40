using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour {

    public GameObject targetGameObject;
    public Camera Camera;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        float distance;

        //Vector3 forward = transform.TransformDirection(Vector3.forward)*2;
        //Vector3 up  = transform.TransformDirection(Vector3.up) * 2;

        //Vector3 upRay = (Quaternion.AngleAxis(0.3f, up) * forward)*3;

        //Debug.Log("Update from raycast");
        //Debug.DrawRay(transform.position, forward, Color.black);
        //Debug.DrawRay(transform.position, upRay, Color.black);

        //if (Physics.Raycast(transform.position, (forward), out hit, 2.0f))
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Raycast")
        {
            distance = hit.distance;
            targetGameObject = hit.collider.gameObject;
        }
        else
        {
            targetGameObject = null;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetGameObject != null)
            {
                Iinteractable targetScript = (Iinteractable)targetGameObject.GetComponent(typeof(Iinteractable));

                if (targetScript != null)
                {
                    targetScript.PerformInteraction();
                }
                else
                {
                    Debug.Log("No target");
                }
            }
        }
    }
}
