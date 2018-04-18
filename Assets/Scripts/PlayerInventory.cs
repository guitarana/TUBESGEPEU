using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public List<string> inventory;
    public InventorySlot[] slots;

    // Use this for initialization
    void Start () {
        inventory = new List<string>();
        UpdateInventory();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Count)
                slots[i].text.text = inventory[i];
            else
                slots[i].text.text = "";
        }
    }
}
