using System;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class MapTile : MonoBehaviour
  {
    public bool IsBroken;
    public GameObject BrokenGo;
    public GameObject FixingGo;

    private void Awake()
    {
      BrokenGo.SetActive(false);
    }

    public void Break()
    {
      IsBroken = true;
      BrokenGo.SetActive(true);
      // animation?
    }

    public void Repair()
    {
      IsBroken = false;
      BrokenGo.SetActive(false);
      StopFixing();
      // animation?
    }

    public void StartFixing()
    {
      FixingGo.SetActive(true);
    }

    public void StopFixing()
    {
      FixingGo.SetActive(false);
    }
  }
}