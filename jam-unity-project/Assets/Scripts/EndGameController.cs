using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityTemplateProjects
{
  public class EndGameController : MonoBehaviour
  {
    public GameObject Ui;

    private void Awake()
    {
      Ui.SetActive(false);
    }

    private void Update()
    {
      var god = GameGod.Instance;

      var leftHealth = god.GetHealthBySide(Side.Left);
      var rightHealth = god.GetHealthBySide(Side.Right);

      if (leftHealth <= 0 || rightHealth <= 0)
      {
        god.IsGameOver = true;
        Ui.SetActive(true);
      }

      if (god.IsGameOver && Input.GetKey("space"))
      {
        SceneManager.LoadScene("ManuScene");
      }
    }
  }
}