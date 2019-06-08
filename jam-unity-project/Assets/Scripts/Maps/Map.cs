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
    public float PickUpDistance = 0.3f;
    public float FixDistance = 0.3f;
    
    public List<MapTile> FirstPlayer;
    public List<MapTile> SecondPlayer;

    public GameObject FirstPlayerRoot;
    public GameObject SecondPlayerRoot;

    public static Map Instance;

    public void Awake()
    {
      Instance = this;

      FirstPlayer = FirstPlayerRoot.GetComponentsInChildren<MapTile>().ToList();
      SecondPlayer = SecondPlayerRoot.GetComponentsInChildren<MapTile>().ToList();
    }

    public List<MapTile> GetOpponentTiles(PlayerIndex index)
    {
      return index == PlayerIndex.One || index == PlayerIndex.Two
        ? SecondPlayer
        : FirstPlayer;
    }
    
    public List<MapTile> GetMyTiles(PlayerIndex index)
    {
      return index == PlayerIndex.One || index == PlayerIndex.Two
        ? FirstPlayer
        : SecondPlayer;
    }
  }
}