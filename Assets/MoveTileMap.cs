using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveTileMap : MonoBehaviour
{
    [SerializeField]
    Transform tileMapTop;
    [SerializeField] Transform tileMapMid;
    [SerializeField] Transform tileMapDown;

    [SerializeField] Transform player;

    private  void FixedUpdate()
    {
        if(player.position.y>tileMapMid.position.y+19) 
        {
            Transform flag = tileMapMid;
            tileMapDown.position=new Vector3(0, tileMapTop.position.y+37,0);
            tileMapMid = tileMapTop;
            tileMapTop = tileMapDown;
            tileMapDown = flag;

        }
        else if(player.position.y < tileMapMid.position.y - 19)
        {
            Transform flag = tileMapMid;
            tileMapTop.position = new Vector3(0, tileMapDown.position.y - 37, 0);
            tileMapMid = tileMapDown;
            tileMapDown = tileMapTop;
            tileMapTop= flag;
        }
    }
}
