using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    float timer = 0;
    public float maxTimer = 5;
    public GameObject itemToSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            itemToSpawn.gameObject.SetActive(true);
            itemToSpawn.GetComponent<Item>().UpdateValue();
            itemToSpawn = null;
            Destroy(this.gameObject);
        }	
	}
}
