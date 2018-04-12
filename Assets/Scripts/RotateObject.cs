using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public float speed = 50.0f;

	// Use this for initialization
	void Start () {
        transform.Rotate(Vector3.up, Random.Range(0, 360));

    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
	}
}
