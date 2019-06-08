
using UnityEngine;

namespace UnityTemplateProjects
{
  public class Helper
  {
    public static float Distance(Vector3 from, Vector3 position)
    {
      return Vector2.Distance(new Vector2(position.x, position.z), new Vector2(from.x, from.z));
    }
  }
}