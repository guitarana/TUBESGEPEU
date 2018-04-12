using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private TextMesh label;
    public string value;

    public enum ItemType
    {
        Number,
        Operation,
        Buff,
        Others
    }

    public ItemType itemType = ItemType.Number;

	// Use this for initialization
	void Start () {

        if (itemType == ItemType.Number)
        {
            int iVal = Random.Range(0, 9);
            value = iVal.ToString();
        }

        if (itemType == ItemType.Operation)
        {
         //   Debug.Log("OPPPPP!!!");
            int iVal = Random.Range(0, 3);
            switch (iVal)
            {
                case 0:
                    Debug.Log("OPPPPP!!!");
                    value = "+";
                    break;
                case 1:
                    value = "-";
                    break;
                case 2:
                    value = "*";
                    break;
                case 3:
                    value = "/";
                    break;
                default:
                    break;
            }
            
        }

        label = transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        if (label)
            label.text = value;    


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
