using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public bool isTriggered;
    public GameObject otherObject; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            otherObject = other.gameObject;
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        otherObject = other.gameObject;
        isTriggered = false;
    }
}
