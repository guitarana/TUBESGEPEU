using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private RectTransform inventoryRect;
    private float inventoryWidth, inventoryHeight;
    public int slots;
    public int rows;
    public float slotPadLeft, slotPadTop;
    public float slotSize;
    public GameObject slotPrefab;
    private static Slot from, to;
    private List<GameObject> allSlots;
    public GameObject iconPrefab;

    private static int emptySlot;//field untuk cek slot kosong
    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

    public Canvas canvas;
    private float hoverYOffset;
    public EventSystem eventSystem;
    private static GameObject hoverObject;
    
	void Start ()
    {
        CreateLayout();
	}
	
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            if (!eventSystem.IsPointerOverGameObject(-1) && from != null)//cek if point ga mengarah ke game object
            {
                from.GetComponent<Image>().color = Color.white;
                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));
                to = null;
                from = null;
                hoverObject = null;
            }
        }
        if (hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }
	}

    private void CreateLayout() //layout untuk inventory
    {
        //panel or background inventory (besar menyesuaikan slot)
        allSlots = new List<GameObject>();

        hoverYOffset = slotSize * 0.01f;

        emptySlot = slots; //slot kosong saat game starts

        inventoryWidth = (slots / rows) * (slotSize + slotPadLeft) + slotPadLeft;
        inventoryHeight = rows * (slotSize + slotPadTop) + slotPadTop;

        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        //instatiate prefab slot
        int columns = slots / rows;
         for (int y = 0; y<rows; y++)
         {
            for (int x = 0; x <columns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";
                newSlot.transform.SetParent(this.transform.parent); //set the parent to canvas

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPadLeft * (x + 1) + (slotSize * x), -slotPadTop * (y + 1) + (-slotSize * y));
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }
         }
    }

    public bool AddItem(UIItem item)
    {
        if (item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        else
        {
            foreach(GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();
                if (!tmp.IsEmpty)
                {
                    if (tmp.CurrentItem.type == item.type && tmp.IsAavailable)
                    {
                        tmp.AddItem(item);
                        return true;
                    }
                }
            }

            if (emptySlot > 0)
            {
                PlaceEmpty(item);
            }
        }
        return false;
    }

    private bool PlaceEmpty(UIItem item)
    {
        //cek slot kosong, tambah item ke slot tsb
        if (emptySlot > 0)
        {
            foreach(GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (tmp.IsEmpty)
                {
                    tmp.AddItem(item);
                    emptySlot--;
                    return true;
                }
            }
        }
        return false;
    }

    public void MoveItem(GameObject clicked)
    {
        if (from == null) //from = slot item yg mau dipindah
        {
            if (!clicked.GetComponent<Slot>().IsEmpty) //jika slot yang diklik tidak kosong
            {
                from = clicked.GetComponent<Slot>();
                from.GetComponent<Image>().color = Color.gray;

                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                hoverObject.name = "Hover";

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
                hoverObject.transform.localScale = from.gameObject.transform.localScale;
            }
        }
        else if(to == null) //to = slot yang dituju 
        {
            to = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }

        if (to != null && from != null) //kalau slot from dan to ada isinya, item diswap
        {
            Stack<UIItem> tmpTo = new Stack<UIItem>(to.Items);
            to.AddItems(from.Items);

            if(tmpTo.Count == 0) 
            {
                from.ClearSlot();
            }
            else //hanya untuk item stackable (swaping items between slots)
            {
                from.AddItems(tmpTo);
            }

            from.GetComponent<Image>().color = Color.white;
            to = null;
            from = null;
            hoverObject = null;
        }
    }
}
