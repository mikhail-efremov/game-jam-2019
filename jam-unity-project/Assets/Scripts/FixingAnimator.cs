using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class FixingAnimator : MonoBehaviour
{
  public Player Player;

  public GameObject Image;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    Image.SetActive(Player._fixer.IsFixing);
  }
}

