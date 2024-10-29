using System.Collections;
using System.Collections.Generic;
using Json;
using Models;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // GameData gameData;
    public List<GameData> gamedatas = new List<GameData>();
    /*
    0: Replay Mode: Action Controlled by ReplayController.
    */
    public int Mode = -1;

    #region Models
    public Models.TileMap tileMap = new TileMap();
    public Ghost ghost0 = new Ghost(0, new Vector2(0,0));
    public Ghost ghost1 = new Ghost(1, new Vector2(0,0));
    public Ghost ghost2 = new Ghost(2, new Vector2(0,0));
    public Ghost ghost3 = new Ghost(3, new Vector2(0,0));
    #endregion
    void Start()
    {   
        //Waiting for Communcation Controller Start.
        while(Mode == -1);

        if(Mode == 0){
            Debug.Log("Replay Mode.");
        }
        Debug.Log(Pacman.GetInfo());
        Debug.Log(Ghost.GetInfo());
    }

    // Update is called once per frame
    void Update()
    {
        if(Mode == 0){

        }
    }

}