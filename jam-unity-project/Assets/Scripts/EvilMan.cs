using System.Collections;
using UnityEngine;

public class EvilMan : MonoBehaviour
{
  public Animator _animator;
  public AudioSource Smile;
  public AudioSource Snap;
  
  private bool _alwaysSmile;

  private IEnumerator _routine;

  private void Start()
  {
    _animator.SetBool("Idle", true);
  }

  public void Action()
  {
    if (_alwaysSmile)
      return;
    
    if (_routine != null)
    {
      _animator.SetBool("Reset", true);
      StopCoroutine(_routine);
      _routine = null;
    }

    var routine = DoAction();
    StartCoroutine(routine);
    _routine = routine;
  }

  private IEnumerator DoAction()
  {
//    Debug.LogError("call action");
    
    _animator.SetBool("Reset", false);
    _animator.SetBool("Idle", false);
    _animator.SetBool("Action", true);
    
    Snap.Play();
    yield return new WaitForSeconds(1);
    
//    Debug.LogError("end");
    
    _animator.SetBool("Action", false);    
    _animator.SetBool("End", true);
    
    
    yield return new WaitForSeconds(.5f);
    Smile.Play();
    yield return new WaitForSeconds(1.5f);
    
//    Debug.LogError("reset");
    
    _animator.SetBool("End", false);
    _animator.SetBool("Reset", true);

    yield return new WaitForSeconds(.1f);

    _animator.SetBool("Reset", false);
    _animator.SetBool("Idle", true);

    _routine = null;
  }

  public IEnumerator StartAlwaysSmile()
  {
    if (_alwaysSmile)
      yield break;
    
    if (!_alwaysSmile)
      _alwaysSmile = true;
    
    _animator.SetBool("Idle", false);
    _animator.SetBool("Smile", true);

    while (true)
    {
      yield return new WaitForSeconds(1);
      Smile.Play();
    }
  }

  private bool _isReset = false;
  
  public void Reset()
  {
    if (!_isReset)
    {
      _isReset = true;
      _animator.Rebind();
    }
   
  }
}