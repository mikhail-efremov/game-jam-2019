using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Map : MonoBehaviour
  {
    public GameObject Bomb;
    public float FixTime;
    
    public List<MapTile> LeftPlayer;
    public List<MapTile> RightPlayer;

    public GameObject FirstPlayerRoot;
    public GameObject SecondPlayerRoot;

    public static Map Instance;

    public List<MapTile> BySide(Side side)
    {
      if (side == Side.Left)
        return LeftPlayer;
      return RightPlayer;
    }

    public void Awake()
    {
      Instance = this;

      LeftPlayer = FirstPlayerRoot.GetComponentsInChildren<MapTile>().ToList();
      RightPlayer = SecondPlayerRoot.GetComponentsInChildren<MapTile>().ToList();
    }

    public List<MapTile> GetOpponentTiles(PlayerIndex index)
    {
      return index == PlayerIndex.One || index == PlayerIndex.Two
        ? RightPlayer
        : LeftPlayer;
    }
    
    public List<MapTile> GetMyTiles(PlayerIndex index)
    {
      return index == PlayerIndex.One || index == PlayerIndex.Two
        ? LeftPlayer
        : RightPlayer;
    }
  }
}