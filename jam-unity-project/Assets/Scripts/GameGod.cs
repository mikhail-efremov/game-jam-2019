using UnityEngine;

namespace UnityTemplateProjects
{
  public class GameGod : MonoBehaviour
  {
    private static GameGod _instance;

    public static GameGod Instance => _instance;
    
    private void Awake()
    {
      _instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }
}