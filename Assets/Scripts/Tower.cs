using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public Renderer renderComponent;
    private Material mat;
    private float matValue;
    public float glowSpeed = 1.0f;
    public Trigger trigger;
    public Renderer flagRenderer;

    public int towerValue;
    public int currentValueRed = 0;
    public int currentValueBlue;
    public string currentStrOperator;
    public string currentStrNumber;

    public enum BelongsTo
    {
        Neutral,
        Red,
        Blue
    }

    public BelongsTo status = BelongsTo.Neutral;

    // Use this for initialization
	void Start () {
        renderComponent = GetComponent<Renderer>();
        mat = renderComponent.materials[0];
        SetupNewValue();

    }

  // public bool panelShown;
	// Update is called once per frame
	void Update () {



        if (currentValueRed == towerValue)
        {

            status = BelongsTo.Red;
            SetupNewValue();
        }

        if (currentValueBlue == towerValue)
        {
            status = BelongsTo.Blue;
            SetupNewValue();

        }

        if (trigger)
        {
            if (trigger.isTriggered)
            {

                trigger.otherObject.GetComponent<PlayerController>().ShowSolver(true, towerValue,this);
                GlowMaterialWithEmmisive();

            }
            else
            {
                if (trigger.otherObject)
                {
                    trigger.otherObject.GetComponent<PlayerController>().ShowSolver(false, towerValue, null);
                  
                    trigger.otherObject = null;
                }
                


                mat.SetColor("_EmissionColor", new Color(0, 0, 0));
            }

        }

    }

    void SetupNewValue()
    {
        towerValue = Random.Range(10, 100);
        if (status == BelongsTo.Red)
            flagRenderer.materials[0].SetColor("_Color",new Color(1f,0.1f,0.1f));
        if (status == BelongsTo.Blue)
            flagRenderer.materials[0].SetColor("_Color", new Color(0.1f, 0.1f, 1f));
        if (status == BelongsTo.Neutral)
            flagRenderer.materials[0].SetColor("_Color", new Color(1f, 1f, 1f));

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
