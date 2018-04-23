using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public string[] inventory;
    public InventorySlot[] slots;

    // Use this for initialization
    void Start () {
        UpdateInventory();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
           slots[i].text.text = inventory[i];
        }
    }
}
