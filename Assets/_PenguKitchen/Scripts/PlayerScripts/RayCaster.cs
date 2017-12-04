using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour {

    public GameObject targetGameObject;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        float distance;

        Vector3 forward = transform.TransformDirection(Vector3.forward)*2;
        Debug.Log("Update from raycast");
        Debug.DrawRay(transform.position, forward, Color.black);

        if (Physics.Raycast(transform.position, (forward), out hit,2.0f))
        {
            distance = hit.distance;
            targetGameObject = hit.collider.gameObject;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetGameObject != null)
            {
                Iinteractable targetScript = (Iinteractable)targetGameObject.GetComponent(typeof(Iinteractable));
                targetScript.PerformInteraction();
            }
        }
    }
}
