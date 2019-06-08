﻿using UnityEngine;
using UnityTemplateProjects;

public class PlayerMaster : MonoBehaviour
{
  public Side Side;
  
  public Player BigPlayer;

  public Player FixPlayer;
  public Player FightPlayer;

  private void Awake()
  {
    BigPlayer.Role = PlayerRole.Big;

    FixPlayer.Role = PlayerRole.Fix;
    FightPlayer.Role = PlayerRole.Shoot;
  }

  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      Split();
    }
    
    if (Input.GetKeyDown("1"))
    {
      GetTogether();
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
    BigPlayer.gameObject.SetActive(true);

    FixPlayer.gameObject.SetActive(false);
    FightPlayer.gameObject.SetActive(false);
    
    Debug.LogError("GET TOGETHER!");
  }
}