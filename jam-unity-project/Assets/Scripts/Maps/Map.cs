using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Map : MonoBehaviour
  {
    public GameObject Bomb;
    
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
  }
}