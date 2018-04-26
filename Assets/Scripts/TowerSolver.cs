using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSolver : MonoBehaviour {


    public int value;

    public int currentValueRed;
    public int currentValueBlue;

    public string strOperator;
    public string strNumber;

    public GameObject owner;


    public UnityEngine.UI.Text textTowerValue;
    public UnityEngine.UI.Text textCurrentValueRed;
    public UnityEngine.UI.Text textOperator;
    public UnityEngine.UI.Text textNumber;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        textTowerValue.text = value.ToString();
        textCurrentValueRed.text = currentValueRed.ToString();
        textOperator.text = strOperator;
        textNumber.text = strNumber;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
           string s = owner.GetComponent<PlayerInventory>().inventory[0];
            if (s != "+" && s != "-" && s != "*" && s != "/")
            {
                strNumber = s;
            }
            else
            {
                strOperator = s;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string s = owner.GetComponent<PlayerInventory>().inventory[1];
            if (s != "+" && s != "-" && s != "*" && s != "/")
            {
                strNumber = s;
            }
            else
            {
                strOperator = s;
            }
        }

        if (strOperator != "" && strNumber != "")
        {
            Debug.Log("TEst");

            switch (strOperator)
            {
                case "+":
                    currentValueRed += int.Parse(strNumber);                   
                    break;
                case "-":
                    currentValueRed -= int.Parse(strNumber);
                    break;
                case "*":
                    currentValueRed *= int.Parse(strNumber);
                    break;
                case "/":
                    currentValueRed /= int.Parse(strNumber);
                    break;
            }
        }	
	}
}
