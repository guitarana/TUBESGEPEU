using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public Tower[] tower;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RedWin())
            Debug.Log("Red Win!!!!!!!");
	}

    bool RedWin()
    {
        if (tower[0].status == Tower.BelongsTo.Red &&
            tower[1].status == Tower.BelongsTo.Red &&
            tower[2].status == Tower.BelongsTo.Red &&
            tower[3].status == Tower.BelongsTo.Red &&
            tower[4].status == Tower.BelongsTo.Red &&
            tower[5].status == Tower.BelongsTo.Red &&
            tower[6].status == Tower.BelongsTo.Red)
        {
            return true;
        }
        return false;
    }
}
