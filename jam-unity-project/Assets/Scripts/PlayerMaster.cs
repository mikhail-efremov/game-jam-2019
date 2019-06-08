using UnityEngine;
using UnityTemplateProjects;

public class PlayerMaster : MonoBehaviour
{
  public Player BigPlayer;

  public Player FixPlayer;
  public Player FightPlayer;

  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      Split();
    }
  }

  public void Split()
  {
    BigPlayer.gameObject.SetActive(false);

    FixPlayer.gameObject.SetActive(true);
    FightPlayer.gameObject.SetActive(true);
    
    Debug.LogError("SPLIT!");
  }

  public void GetTogether()
  {
      
  }
}