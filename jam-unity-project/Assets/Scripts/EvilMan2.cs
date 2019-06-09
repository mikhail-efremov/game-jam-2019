using System.Collections;
using UnityEngine;

namespace UnityTemplateProjects
{
  public class EvilMan2 : MonoBehaviour
  {
    public Animator _animator;

    private void Start()
    {
      //_animator.SetBool("Idle", true);
      _animator.enabled = false;
    }

    public void Action()
    {
      _animator.enabled = true;
      var routine = DoAction();
      StartCoroutine(routine);
    }

    private IEnumerator DoAction()
    {
      yield return new WaitForSeconds(.1f);
    
//    Debug.LogError("call action");

      _animator.SetBool("Reset", false);
      _animator.SetBool("Idle", true);
      
      _animator.SetBool("Action", false);    
      _animator.SetBool("End", true);
    
      yield return new WaitForSeconds(1f);

      _animator.SetBool("Reset", false);
      _animator.SetBool("Idle", false);    
      _animator.SetBool("Action", true);
      
      yield return new WaitForSeconds(.1f);
      
      _animator.SetBool("Action", false);    
      _animator.SetBool("End", true);
      
      yield return new WaitForSeconds(1f);
      
      _animator.SetBool("Reset", false);
      _animator.SetBool("Idle", true);
      
      _animator.SetBool("Action", false);    
      _animator.SetBool("End", true);
    
      yield return new WaitForSeconds(1f);

      _animator.SetBool("Reset", false);
      _animator.SetBool("Idle", false);    
      _animator.SetBool("Action", true);

      yield return new WaitForSeconds(10);
    
//    Debug.LogError("end");
    
      _animator.SetBool("Action", false);    
      _animator.SetBool("End", true);
    
      yield return new WaitForSeconds(1f);
    
//    Debug.LogError("reset");
    
      _animator.SetBool("End", false);
      _animator.SetBool("Reset", true);

      yield return new WaitForSeconds(.1f);

      _animator.SetBool("Reset", false);
      _animator.SetBool("Idle", true);
      
      
    }
  }
}