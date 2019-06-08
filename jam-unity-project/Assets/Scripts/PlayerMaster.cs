using System.Collections;
using UnityEngine;
using UnityTemplateProjects;
using UnityTemplateProjects.Maps;

public class PlayerMaster : MonoBehaviour
{
  public Side Side;

  public Player BigPlayer;

  public GameObject SplitEffect;
  public GameObject FixPlayerEndEffect;
  public GameObject FightPlayerEndEffect;

  public GameObject FixPlayerStartEffect;
  public GameObject FightPlayerStartEffect;

  public Player FixPlayer;
  public Player FightPlayer;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F2))
    {
      Split();
    }

    if (Input.GetKeyDown(KeyCode.F3))
    {
      GetTogether();
    }
  }

  public void Split()
  {
    StartCoroutine(SplitCo());
  }

  public IEnumerator SplitCo()
  {
    BigPlayer.BlockMovement();
    BigPlayer.gameObject.SetActive(false);

    var splitEffect = Instantiate(SplitEffect, BigPlayer.transform.position, Quaternion.identity);
    splitEffect.SetActive(true);
    yield return new WaitForSeconds(0.4f);

    var tiles = Map.Instance.GetMyTiles(BigPlayer._playerIndex);
    var fixPlayerPos = tiles[Random.Range(0, tiles.Count)].transform.position;
    fixPlayerPos.y = FixPlayer.transform.position.y;
    var fightPlayerPos = tiles[Random.Range(0, tiles.Count)].transform.position;
    fightPlayerPos.y = FightPlayer.transform.position.y;

    var fixPlayerStartEffect = Instantiate(FixPlayerStartEffect, Vector3.zero, Quaternion.identity);
    var fixPlayerLineR = fixPlayerStartEffect.GetComponent<LineRenderer>();
    fixPlayerLineR.SetPositions(new[]
    {
      fixPlayerPos,
      BigPlayer.transform.position,
    });

    var fightPlayerStartEffect = Instantiate(FightPlayerStartEffect, Vector3.zero, Quaternion.identity);
    var fightPlayerLineR = fightPlayerStartEffect.GetComponent<LineRenderer>();
    fightPlayerLineR.SetPositions(new[]
    {
      fightPlayerPos,
      BigPlayer.transform.position
    });

    yield return new WaitForSeconds(0.25f);

    var fixPlayerEndEffect = Instantiate(FixPlayerEndEffect, fixPlayerPos, Quaternion.identity);
    var fightPlayerEndEffect = Instantiate(FightPlayerEndEffect, fightPlayerPos, Quaternion.identity);

    yield return new WaitForSeconds(0.2f);

    FixPlayer.transform.position = fixPlayerPos;
    FightPlayer.transform.position = fightPlayerPos;

    FixPlayer.gameObject.SetActive(true);
    FightPlayer.gameObject.SetActive(true);
    yield return new WaitForSeconds(0.1f);
    Destroy(fixPlayerStartEffect);
    Destroy(fightPlayerStartEffect);
    
    yield return new WaitForSeconds(2.1f);
    Destroy(fixPlayerEndEffect);
    Destroy(fightPlayerEndEffect);
    Destroy(splitEffect);

    Debug.LogError("SPLIT!");
    BigPlayer.ReleaseMovement();
  }

  public void GetTogether()
  {
    BigPlayer.gameObject.SetActive(true);

    FixPlayer.gameObject.SetActive(false);
    FightPlayer.gameObject.SetActive(false);

    Debug.LogError("GET TOGETHER!");
  }
}