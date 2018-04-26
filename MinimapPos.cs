using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapPos : MonoBehaviour {

    float playerX, playerZ;
    float towerX, towerZ;
    float mapWidth, mapHeight;
    float arenaWidth, arenaHeight;
    public Transform player, tower;
    public GameObject playerIcon, towerIcon;    
    private static GameObject iconPlayer, iconTower;
    private float iconHalfSize;
    public List<GameObject> towers;

    void Start()
    {
        GetMapPosition();
        PlayerIcon();
        TowerIcon();
    }

    public void GetMapPosition()
    {
        RectTransform mapSize = GameObject.Find("MinimapPos").GetComponent<RectTransform>(); //ambil ukuran map
        mapWidth = mapSize.rect.width; //ukuran panjang map
        mapHeight = mapSize.rect.height; //ukuran lebar map
        player = GameObject.Find("PlayerController").transform; //ambil posisi player di arena

        arenaWidth = GameObject.Find("Plane").GetComponent<Renderer>().bounds.size.x; //ambil ukuran arena panjang
        arenaHeight = GameObject.Find("Plane").GetComponent<Renderer>().bounds.size.z; //ambil ukuran arena lebar
    }

    public void TowerIcon()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            iconTower = Instantiate(towerIcon);
            iconTower.transform.SetParent(GameObject.Find("MinimapPos").transform, false);

            tower = towers[i].transform; //ambil posisi tower di arena
            towerX = tower.position.x; //posisi tower X
            towerZ = tower.position.z; //posisi tower Z convert ke Y

            float ticonX = -1* towerX * mapWidth/arenaWidth;
            float ticonY = -1* towerZ * mapHeight/arenaHeight;
            iconTower.transform.localPosition = new Vector3(ticonX, ticonY);
        }

    }

    public void PlayerIcon()
    {
        iconPlayer = Instantiate(playerIcon); //panggil prefab playerIcon
        iconPlayer.transform.SetParent(GameObject.Find("MinimapPos").transform, false);//set parent ke MinimapPos
        RectTransform iconSize = iconPlayer.GetComponent<RectTransform>();
        iconHalfSize = iconSize.rect.width / 2;
    }

    void Update()
    {
        PlayerMapMove();
    }

    public void PlayerMapMove()
    {      
        playerX = player.position.x; //posisi player X
        playerZ = player.position.z; //posisi player Z convert ke sumbu Y

        float piconX = (-1* playerX * mapWidth/arenaWidth)-iconHalfSize; //posisi icon player di minimap (sumbu X)
        float piconY = (-1* playerZ * mapHeight/arenaHeight); //posisi icon player di minimap (sumbu Y)
        iconPlayer.transform.localPosition = new Vector3(piconX, piconY);
    }

}
