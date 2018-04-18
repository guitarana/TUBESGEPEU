using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Stack<UIItem> items;
    public Stack<UIItem> Items
    {
        get { return items; }
        set { items = value; }
    }
    public Text stackTxt;
    public Sprite slotEmpty;
    public Sprite slotHighlight;

    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }

    public bool IsAavailable
    {
        get { return CurrentItem.maxSize > items.Count; }
    }

    public UIItem CurrentItem
    {
        get { return items.Peek(); }
    }

	void Start ()
    {
        items = new Stack<UIItem>();
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

    public void AddItem(UIItem item) //untuk 1 item saja
    {
        items.Push(item); //add to items
        
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
    }

    public void AddItems(Stack<UIItem> items) //untuk multiple items dalam 1 stack (kalau ada)
    {
        this.items = new Stack<UIItem>(items);
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
    }

    private void ChangeSprite(Sprite neutral, Sprite highlight)
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
            items.Pop().Use(); //pakai (remove) item yg ada di slot

            //update informasi stack.
            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (IsEmpty) //jika slot kosong
            {
                ChangeSprite(slotEmpty, slotHighlight); //ganti sprite slot jadi sprite awal
                Inventory.EmptySlot++; //tambah jumlah slot
            }
        }
    }

    public void ClearSlot() //kosongkan slot
    {
        items.Clear();
        ChangeSprite(slotEmpty, slotHighlight);
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
