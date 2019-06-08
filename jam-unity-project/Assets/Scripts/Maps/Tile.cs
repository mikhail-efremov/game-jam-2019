using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Tile : MonoBehaviour
  {
    public bool IsBroken;

    public void Break()
    {
      IsBroken = true;
      // animation?
    }

    public void Repair()
    {
      IsBroken = false;
      // animation?
    }
  }
}