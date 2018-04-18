using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {ANGKA, OPERATOR}

public class UIItem : MonoBehaviour
{
    public ItemType type;
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;
    public int maxSize; //variable untuk menentukan stacking item

    public void Use()
    {
        switch (type)
        {
            case ItemType.ANGKA:
                Debug.Log("Angka dipakai");
                break;
            case ItemType.OPERATOR:
                Debug.Log("Operator dipakai");
                break;
        }
    }
}
