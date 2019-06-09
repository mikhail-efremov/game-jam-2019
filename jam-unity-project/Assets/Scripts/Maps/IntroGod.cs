using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GamepadInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

namespace UnityTemplateProjects.Maps
{
  public class IntroGod : MonoBehaviour
  {
    public GameObject BombGo;
    public GameObject LogoGo;
    public GameObject PressGo;

    public GameObject Player1;
    public GameObject Player2;
    
    private Vector3 _bombTarget = new Vector3(-1.2f, 7.49f, -5.04f);

    private AudioSource _audioSource;

    public AudioClip LaughAudio;
    public AudioClip ClapAudio;
    
    private void Start()
    {
      var startPos = _bombTarget;
      startPos.y = 10;
      BombGo.transform.position = startPos;

      _audioSource = GetComponent<AudioSource>();
      
      StartCoroutine(IntroSeq());
    }

    private IEnumerator IntroSeq()
    {
      BombGo.transform.DOMove(_bombTarget, 1f).SetEase(Ease.OutBounce);

      yield return new WaitForSeconds(3f);
      
      MusicMaster.Instance.PlayMenuMusic();

      LogoGo.SetActive(true);
      var spriteRenderer = LogoGo.GetComponent<SpriteRenderer>();
      var al = Color.white;
      al.a = 0;
      spriteRenderer.color = al;
      spriteRenderer.DOFade(1, 0.5f)
        .OnComplete(() => { });

      LogoGo.transform.DOScale(new Vector3(1f, 1.02f, 1f), 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);
      yield return LogoGo.transform.DOMoveY(LogoGo.transform.position.y + .1f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);

      yield return new WaitForSeconds(1.5f);

      TweenerCore<Color,Color, ColorOptions> asd = null;
      PressGo.gameObject.SetActive(true);
      PressGo.GetComponent<Text>().DOFade(1f, 3f).SetEase(Ease.Flash)
        .OnComplete(() => { asd = PressGo.GetComponent<Text>().DOFade(0.2f, 2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo); });

      bool isPressed = false;
      while (!isPressed)
      {
        isPressed = (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any)
                     || (GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any))
                     || (GamePad.GetButtonDown(GamePad.Button.Back, GamePad.Index.Any))
                     || Input.GetKeyDown(KeyCode.Space)
                     || Input.GetKeyDown(KeyCode.Return));
        yield return null;
      }
      
      asd.Pause();
      PressGo.GetComponent<Text>().DOPause();
      
      PressGo.transform.DOPause();
      PressGo.GetComponent<Text>().DOFade(0f, 1f).SetEase(Ease.Flash).OnComplete(() =>
      {
        Destroy(PressGo);
      });

      
      LogoGo.transform.DOPause();
      LogoGo.transform.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.Flash)
        .OnComplete(() =>
        {
          Destroy(LogoGo);
        });
      
      yield return new WaitForSeconds(2f);
      RenderSettings.fogColor = Color.black;
      DOTween.To(() => RenderSettings.fogEndDistance, x => RenderSettings.fogEndDistance = x, 300, 6)
        .OnComplete(() =>
        {
          RenderSettings.fogColor = Color.black;
        });

      
//      DOTween.To(asdasd, value => RenderSettings.fogColor = value,Color.black, 1f);
      
      yield return new WaitForSeconds(2f);
      
      FindObjectOfType<EvilMan2>().Action();
      
      yield return new WaitForSeconds(1.4f);
      
      var renderers1 = Player1.GetComponentsInChildren<SpriteRenderer>();
      var renderers2 = Player2.GetComponentsInChildren<SpriteRenderer>();

      Player1.SetActive(true);
      Player2.SetActive(true);
      
      foreach (var renderer1 in renderers1)
      {
        var w = renderer1.color;
        w.a = 0;
        renderer1.color = w;
      }
      
      foreach (var renderer2 in renderers2)
      {
        var w = renderer2.color;
        w.a = 0;
        renderer2.color = w;
      }

      foreach (var renderer1 in renderers1)
      {
        renderer1.DOFade(1, 1f).SetEase(Ease.Flash);
      }
      
      foreach (var renderer2 in renderers2)
      {
        renderer2.DOFade(1, 1f).SetEase(Ease.Flash);
      }

      yield return new WaitForSeconds(.2f);
      
      _audioSource.clip = LaughAudio;
      _audioSource.Play();
      
      yield return new WaitForSeconds(.8f);

      SceneManager.LoadScene("MainScene");
      
      RenderSettings.fogColor = Color.black;
    }
  }
}