using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Map : MonoBehaviour
  {
    public GameObject Bomb;
    
    public List<Tile> FirstPlayer;
    public List<Tile> SecondPlayer;

    public static Map Instance;

    public void Awake()
    {
      Instance = this;
    }
  }
}