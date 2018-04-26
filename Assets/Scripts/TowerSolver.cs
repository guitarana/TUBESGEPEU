using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSolver : MonoBehaviour {


    public int value;
    public Tower currentTower;
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

   public void InputItem(int index)
    {
         if (index == 1)
        { 
           string s = owner.GetComponent<PlayerInventory>().inventory[0];
            if (s != "+" && s != "-" && s != "*" && s != "/")
            {
                currentTower.currentStrNumber = s;
                InventoryLogic(0);
            }
            else
            {
                currentTower.currentStrOperator = s;
                InventoryLogic(0);
            }
        }

        if (index == 2)
        {
            string s = owner.GetComponent<PlayerInventory>().inventory[1];
            if (s != "+" && s != "-" && s != "*" && s != "/")
            {
                currentTower.currentStrNumber = s;
                InventoryLogic(1);
            }
            else
            {
                currentTower.currentStrOperator = s;
                InventoryLogic(1);
            }
        }
        if (index == 3)
        {
            string s = owner.GetComponent<PlayerInventory>().inventory[2];
            if (s != "+" && s != "-" && s != "*" && s != "/")
            {
                currentTower.currentStrNumber = s;
                InventoryLogic(2);
            }
            else
            {
                currentTower.currentStrOperator = s;
                InventoryLogic(2);
            }
        }
    }


	// Update is called once per frame
	void Update () {

        if (!currentTower) return;

        textTowerValue.text = currentTower.towerValue.ToString();
        textCurrentValueRed.text = currentTower.currentValueRed.ToString();
        textOperator.text = currentTower.currentStrOperator;
        textNumber.text = currentTower.currentStrNumber;
          Debug.Log(currentTower.gameObject.name);

       

        if (currentTower.currentStrOperator != "" && currentTower.currentStrNumber != "")
        {
         

            switch (currentTower.currentStrOperator)
            {
                case "+":
                     if(currentTower)
                        currentTower.currentValueRed += int.Parse(currentTower.currentStrNumber);
                    ResetStr();           
                    break;
                case "-":
                    if (currentTower)
                        currentTower.currentValueRed -= int.Parse(currentTower.currentStrNumber);
                    ResetStr();
                    break;
                case "*":
                    if (currentTower)
                        currentTower.currentValueRed *= int.Parse(currentTower.currentStrNumber);
                    ResetStr();
                    break;
                case "/":
                    if (currentTower)
                        currentTower.currentValueRed /= int.Parse(currentTower.currentStrNumber);
                    ResetStr();
                    break;
            }
        }	
	}

    void ResetStr()
    {
        currentTower.currentStrNumber = "";
        currentTower.currentStrOperator = "";
    }

    void InventoryLogic(int index)
    {
        owner.GetComponent<PlayerInventory>().inventory[index] = "";
        owner.GetComponent<PlayerInventory>().UpdateInventory();
    }
}
