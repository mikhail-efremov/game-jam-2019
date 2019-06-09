using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityTemplateProjects.Maps
{
  public class FinishGod : MonoBehaviour
  {
    public GameObject PlayerLeft;
    public GameObject PlayerRight;
    
    public GameObject LeftFinish;
    public GameObject RightFinish;

    public GameObject LeftLose;
    public GameObject RightLose;
    
    private Vector3 _leftTarget = new Vector3(-3.38f,4.5f,-2.52f);
    private Vector3 _rightTarget = new Vector3(3.39f,3.37f,-1.54f);


    private bool _isStarted = false;
    
    public void Update()
    {
      if (GameGod.Instance.IsGameOver)
      {
        StartCoroutine(WinnerLosingCo());
       
      }
    }

    private IEnumerator WinnerLosingCo()
    {
      yield return null;
      
      var leftPlayer = GameGod.Instance.GetHealthBySide(Side.Left);
      var rightPlayer = GameGod.Instance.GetHealthBySide(Side.Right);

      var isLeftWob = leftPlayer > rightPlayer;

      if (isLeftWob)
      {
        LeftFinish.SetActive(true);
        RightFinish.SetActive(false);
        LeftLose.SetActive(false);
        RightLose.SetActive(true);
          
        RightLose.transform.DOMove(_rightTarget, 0.4f);

        yield return new WaitForSeconds(5);
        
        DOTween.To(() => RenderSettings.fogEndDistance, x => RenderSettings.fogEndDistance = x, 22, 6);

        LeftFinish.GetComponentInChildren<ParticleSystem>().Stop();
      }
      else
      {
        LeftFinish.SetActive(false);
        RightFinish.SetActive(true);
        LeftLose.SetActive(true);
        RightLose.SetActive(false);
          
        LeftLose.transform.DOMove(_leftTarget, 0.4f);
        // LeftLose.transform.DOScale(1.1f, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
         
        yield return new WaitForSeconds(5);
        
        RightFinish.GetComponentInChildren<ParticleSystem>().Stop();
        DOTween.To(() => RenderSettings.fogEndDistance, x => RenderSettings.fogEndDistance = x, 22, 2);

        //LeftLose.transform.DOScale(new Vector3(1.1f,1.1f,1), )
      }

      var allBoms = FindObjectsOfType<Bomb>();
      foreach (var bom in allBoms)
      {
        bom.Explode();
      }

      var players = FindObjectsOfType<Player>();
      foreach (var player in players)
      {
        Destroy(player);
      }
      
      
      RightLose.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetEase(Ease.Flash);
      LeftLose.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetEase(Ease.Flash);
      
      var renderers1 = PlayerLeft.GetComponentsInChildren<SpriteRenderer>();
      var renderers2 = PlayerRight.GetComponentsInChildren<SpriteRenderer>();
      

      foreach (var renderer1 in renderers1)
      {
        renderer1.DOFade(0, 1f).SetEase(Ease.Flash);
      }
      
      foreach (var renderer2 in renderers2)
      {
        renderer2.DOFade(0, 1f).SetEase(Ease.Flash);
      }
      
      yield return new WaitForSeconds(5);

      SceneManager.LoadScene("MenuScene");
    }
  }
}