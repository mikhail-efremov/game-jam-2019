using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Bomb : MonoBehaviour
  {
    public int Radius;
    public float Timeout;

    public void StartTicking()
    {
      // animation?
      
    }

    public IEnumerator Ticking()
    {
      yield return new WaitForSeconds(1);
      // animation?
      yield return new WaitForSeconds(1);
      // animation?

      Explode();
    }

    public void Explode()
    {
      foreach (var tile in Map.Instance.FirstPlayer)
      {
        float distanceSqr = (transform.position - tile.transform.position).sqrMagnitude;
        if (distanceSqr < Radius * Radius)
        {
          tile.Break();
        }
      }
    }
  }
}