using System.Collections;
using UnityEngine;

public class EvilMan : MonoBehaviour
{
  public Animator _animator;
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
    
    yield return new WaitForSeconds(1);
    
//    Debug.LogError("end");
    
    _animator.SetBool("Action", false);    
    _animator.SetBool("End", true);
    
    yield return new WaitForSeconds(2f);
    
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
  }
}