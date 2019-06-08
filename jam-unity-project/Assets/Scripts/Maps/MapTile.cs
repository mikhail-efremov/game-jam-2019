using System;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class MapTile : MonoBehaviour
  {
    public bool IsBroken;
    private MeshRenderer _renderer;

    private void Awake()
    {
      _renderer = GetComponentInChildren<MeshRenderer>();
    }

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

    public float DistanceToPlayer(Vector3 position)
    {
      var tilePos = transform.position;
      return Vector2.Distance(new Vector2(position.x, position.z), new Vector2(tilePos.x, tilePos.z));
    }
  }
}