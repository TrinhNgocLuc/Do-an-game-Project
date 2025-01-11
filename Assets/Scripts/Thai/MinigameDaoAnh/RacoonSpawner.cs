using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonSpawner : Spawner
{
    public static RacoonSpawner Instance { get; private set; }
    public string RacoonName { get; private set; } = "Racoon";
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        //GameManagerDaoAnh.Instance.onInitializeLv += SpawnRacoonOnInitializeLv;      
    }
    public void SpawnRacoonOnInitializeLv()
    {
        switch(DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                SpawnRacoon(5);
                break;
            case Difficulty.normal:
                SpawnRacoon(6);
                break;
            case Difficulty.hard:
                SpawnRacoon(7);
                break;
        }
    }
    private void SpawnRacoon(int racoonNum)
    {
        ClearRacoon();
        for (int i = 1; i <= racoonNum; i++)
        {
            float rdPosX = Random.Range(-9f, 9f);
            float rdPosY = Random.Range(-1f, -4.5f);
            Transform racoon = Spawn(RacoonName, new Vector2(rdPosX, rdPosY), Quaternion.identity);
            int rdDir = Random.Range(0, 2);
            if (rdDir == 1)
                racoon.GetComponent<RacoonMovement>().Flip();
            racoon.GetComponent<RacoonImage>().SetImage(GameManagerDaoAnh.Instance.GetImageForRacoon(i-1));
            Debug.Log("Spawned " + racoon.name);
            //racoon.gameObject.SetActive(true);
        }
    }
    private void ClearRacoon()
    {
        foreach(Transform racoon in holder)
        {
            Despawn(racoon);
        }
    }
}
