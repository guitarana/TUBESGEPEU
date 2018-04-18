using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private TextMesh label;
    public string value;
    public GameObject[] obj;
    
    public enum ItemType
    {
        Number,
        Operation,
        Buff,
        TowerPoint,
        Others
    }

    public ItemType itemType = ItemType.Number;

	// Use this for initialization
	void Start () {
        UpdateValue();
    }


    void ChangeMesh(int index)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
        }
        obj[index].SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}

   public void UpdateValue()
    {
        if (itemType != ItemType.TowerPoint)
            itemType = (ItemType)Random.Range(0, 2);
        else
        {
            int iVal = Random.Range(1, 100);
            value = iVal.ToString();
            ChangeMesh(iVal);
        }

        if (itemType == ItemType.Number)
        {
            int iVal = Random.Range(0, 9);
            value = iVal.ToString();
            ChangeMesh(iVal);
        }

        if (itemType == ItemType.Operation)
        {
            int iVal = Random.Range(10, 13);
            ChangeMesh(iVal);
            switch (iVal)
            {
                case 10:
                    value = "+";
                    break;
                case 11:
                    value = "-";
                    break;
                case 12:
                    value = "*";
                    break;
                case 13:
                    value = "/";
                    break;
            }
        }
    }
}
