using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class MapTile : MonoBehaviour
  {
    public bool IsBroken;
    private MeshRenderer _renderer;

    public void Break()
    {
      IsBroken = true;
      _renderer.material.color = Color.red;
      // animation?
    }

    public void Repair()
    {
      IsBroken = false;
      _renderer.material.color = Color.green;
      // animation?
    }
  }
}