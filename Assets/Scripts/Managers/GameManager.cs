using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private SoliderPool soliderPool;
    [SerializeField] private CubeLvlManager cubeLvlManager;
    [Header("Scriptable objs")]
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private PlayerSO playerSO;
    [Header("____")]
    [SerializeField] private UiManager uiManager;
    [SerializeField] private GameObject player;


    private int matchTime = 0;

    private void Awake()
    {
        gameSetupSo.IsPlay = false;
        gameSetupSo.IsPause = false;
        cubeLvlManager.MakeLvl();
    }

    private void MatchManager(bool isPlay)
    {
        if (isPlay)
        {
            StartCoroutine(InitLvl());
        }
        else
        {
            StartCoroutine(CleanLvl());
        }
    }

    private IEnumerator InitLvl()
    {
        gameSetupSo.DifficultyLvl = 1;
        playerSO.InitPlayer();

        cubeLvlManager.InitNav();

        yield return new WaitForSeconds(1f);

        cubeLvlManager.FallingCubes();
        StartCoroutine(MatchTimer());
        soliderPool.StartSpawn();
    }

    private IEnumerator CleanLvl()
    {
        yield return new WaitForSeconds(1f);
        soliderPool.KillAll();
        cubeLvlManager.RestartLvl();
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(0f, 3f, 0f);
        player.transform.rotation = Quaternion.identity;
    }

    private IEnumerator MatchTimer()
    {
        while (gameSetupSo.IsPlay)
        {
            matchTime++;
            if (matchTime % 10 == 0) gameSetupSo.DifficultyLvl++;
            yield return new WaitForSeconds(1f);
        }
    }

    private void Pause(int lvl)
    {
        //if (lvl == 1) return;
        // gameSetupSo.IsPause = true;
    }

    private void OnEnable()
    {
        //playerSO.OnLevelChange += Pause;
        gameSetupSo.OnIsPlayChange += MatchManager;
    }

    private void OnDisable()
    {
        //playerSO.OnLevelChange += Pause;
        gameSetupSo.OnIsPlayChange -= MatchManager;
    }
}
