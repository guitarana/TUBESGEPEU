using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Stack<string> items;
    public Stack<string> Items
    {
        get { return items; }
        set { items = value; }
    }
    public Text stackTxt;
    public Sprite slotEmpty;
    public Sprite slotHighlight;

    public Sprite[] itemNeutral;
    public Sprite[] itemHighlight;

    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }

    public bool IsAvailable()
    {
        //   get { return CurrentItem.maxSize > items.Count; }
        return false;
    }

 //   public Item CurrentItem
//    {
//        get { return items.Peek(); }
//    }

	void Start ()
    {
        items = new Stack<string>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.y);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItem(string itemValue) //untuk 1 item saja
    {
        items.Push(itemValue); //add to items
        
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }
        int index = int.Parse(itemValue);
        ChangeSprite(itemNeutral[index], itemHighlight[index],
            (itemValue=="10")? "+" : ((itemValue=="11")?"-" : (itemValue=="12") ? "x" :
            (itemValue=="13") ? "/":""));
    }

    /*
   public void AddItems(Stack<string> items) //untuk multiple items dalam 1 stack (kalau ada)
    {
        this.items = new Stack<string>(items);
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        int index = int.Parse(itemValue);
        ChangeSprite(itemNeutral[index], itemHighlight[index],
           (itemValue == "10") ? "+" : ((itemValue == "11") ? "-" : (itemValue == "12") ? "x" :
           (itemValue == "13") ? "/" : ""));
    }
   */

    private void ChangeSprite(Sprite neutral, Sprite highlight,string label)
    {
        GetComponent<Image>().sprite = neutral;

        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState = st;
    }

    private void UseItem() //saat item dipakai
    {
        if (!IsEmpty) //jika di slot masih ada item (untuk item stackable)
        {
        //    items.Pop().Use(); //pakai (remove) item yg ada di slot

            //update informasi stack.
            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (IsEmpty) //jika slot kosong
            {
                ChangeSprite(slotEmpty, slotHighlight,""); //ganti sprite slot jadi sprite awal
                Inventory.EmptySlot++; //tambah jumlah slot
            }
        }
    }

    public void ClearSlot() //kosongkan slot
    {
        items.Clear();
        ChangeSprite(slotEmpty, slotHighlight,"");
        stackTxt.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)//kalau klik kiri masuk ke 'UseItem'
        {
            UseItem();
        }
    }

}
