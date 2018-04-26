using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public Renderer renderComponent;
    private Material mat;
    private float matValue;
    public float glowSpeed = 1.0f;
    public Trigger trigger;

    public int towerValue;
    public int currentValueRed = 0;
    public int currentValueBlue;
 
    // Use this for initialization
	void Start () {
        renderComponent = GetComponent<Renderer>();
        mat = renderComponent.materials[0];
        SetupNewValue();
    }
	
	// Update is called once per frame
	void Update () {

        if (trigger)
        {
            if (trigger.isTriggered)
            {
                trigger.otherObject.GetComponent<PlayerController>().ShowSolver(true, ref currentValueRed, towerValue);
                GlowMaterialWithEmmisive();
            }
            else
            {
                if(trigger.otherObject)
                    trigger.otherObject.GetComponent<PlayerController>().ShowSolver(false, ref currentValueRed, towerValue);
                mat.SetColor("_EmissionColor", new Color(0, 0, 0));
            }

        }

    }

    void SetupNewValue()
    {
        towerValue = Random.Range(10, 100);
    }

    bool flipFlop;
    void GlowMaterialWithEmmisive()
    {
        if (flipFlop)
        {
            Mathf.Clamp(matValue += glowSpeed * Time.deltaTime, 0, 5);
            if (matValue >= 2)
                flipFlop = false;
        }
        else
        {
            Mathf.Clamp(matValue -= glowSpeed * Time.deltaTime, 0, 1);
            if (matValue <= 0)
                flipFlop = true;
        }

        

        mat.SetColor("_EmissionColor", new Color(matValue, matValue, matValue));
    }
}
