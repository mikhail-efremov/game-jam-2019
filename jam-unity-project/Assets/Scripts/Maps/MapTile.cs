using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class MapTile : MonoBehaviour
  {
    public bool IsBroken;
    public GameObject BrokenGo;

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
      // animation?
    }    
  }
}