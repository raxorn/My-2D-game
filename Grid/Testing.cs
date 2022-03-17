using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Testing : MonoBehaviour {

    public int lvl;
    public List<GameObject> groundTiles = new List<GameObject>();

    private GameObject player;
    private Dictionary<Vector2, GameObject> newtilemap;

    public int roomsize;
    private int halvRoomsize;
    private Vector2 playerPos;
    private Vector2 prevPosition;
    private Vector2 direction;

    private void Start() {
       
        newtilemap = new Dictionary<Vector2, GameObject>();
        player = GameObject.Find("Player");
        prevPosition = RoundDown(player.transform.position);
        halvRoomsize = roomsize/2;
        
        createStartZone();        
    }

    void Update(){
        playerPos = RoundDown(player.transform.position);
        direction = updateDirection();

        if(get_dirX() == 1 || get_dirX() == -1){
            createHorizontalTiles();
            destroyHorizontalTiles();
        }
        if(get_dirY() == 1 || get_dirY() == -1){
            createVerticalTiles();
            destroyVerticaliles();            
        }

    }

    private Vector2 updateDirection(){
        Vector2 currentPlayerPos = RoundDown(player.transform.position);
        direction = currentPlayerPos - prevPosition;
        prevPosition = currentPlayerPos;  

        return direction;
    }

    private void createStartZone(){
        for (int x = -halvRoomsize; x < halvRoomsize; x++)
        {
            for (int y = -halvRoomsize; y < halvRoomsize; y++)
            {
                Vector2 pos = new Vector2(x, y);
                newtilemap.Add(pos, createGroundTile(pos));
            }
        }        
    }

    private void createHorizontalTiles(){
         
        int x = get_prevX() + (halvRoomsize -1);
        if (get_dirX() == -1){
            x -= roomsize -1 ;
        }

        int start = get_prevY() - halvRoomsize;
        int stop = start + roomsize;
            
        for(int y = start; y < stop; y++ ){
            Vector2 pos = new Vector2(x, y);
            if(!newtilemap.ContainsKey(pos)){
                newtilemap.Add(pos, createGroundTile(pos));
            }    
        }
    }

    private void createVerticalTiles(){        
        int start, stop;  
        int y = get_prevY() + halvRoomsize -1;
        
        if(get_dirY() == -1){
            y -= roomsize-1;
        }

        start = get_prevX() - halvRoomsize;
        stop = start + roomsize;
        for(int x = start; x < stop; x++ ){
            Vector2 pos = new Vector2(x, y);
            if(!newtilemap.ContainsKey(pos)){
                newtilemap.Add(pos, createGroundTile(pos));
            }
            
        }
    }

    private void destroyHorizontalTiles(){
        int start, stop;   
        int x = get_prevX() - halvRoomsize-1;
        if (get_dirX() == -1){
            x += roomsize +1 ;
        }
        start = get_prevY() - halvRoomsize;
        if(get_dirX() == get_dirY())  {
            start -= get_dirX();
        }if(get_dirX() == 1 && get_dirY() == -1 || get_dirX() == -1 && get_dirY() == 1  ){
            start += get_dirX();
        }

        stop = start + roomsize;

        for(int y = start; y < stop; y++ ){
            Vector2 pos = new Vector2(x,y);
            if(newtilemap.ContainsKey(pos)){
                Destroy(newtilemap[pos]);
                newtilemap.Remove(pos);
            }
        }
    }


    private void destroyVerticaliles(){
        int start, stop;   
        int y = get_prevY() - halvRoomsize-1;
        if (get_dirY() == -1){
            y += roomsize +1 ;
        }
        start = get_prevX() - halvRoomsize;
        stop = start + roomsize;

        for(int x = start; x < stop; x++ ){
            Vector2 pos = new Vector2(x,y);
            if(newtilemap.ContainsKey(pos)){
                Destroy(newtilemap[pos]);
                newtilemap.Remove(pos);
            }
        }
    }


    private GameObject createGroundTile(Vector2 position)
    {
        groundTiles[lvl].transform.position = position;
        var newTile = Instantiate(groundTiles[lvl]);
        newTile.transform.parent = this.transform;
        return newTile;
    }

    private Vector2 RoundDown(Vector2 pos)
    {
        pos.x -= pos.x % 1;//roomsize;
        pos.y -= pos.y % 1;//roomsize;
        return pos; 
    }

    private int get_prevY(){
        return (int)prevPosition.y;
    }
    private int get_prevX(){
        return (int)prevPosition.x;
    }
    private int get_dirX(){
        return (int)direction.x;
    }
    private int get_dirY(){
        return (int) direction.y;
    }
}
