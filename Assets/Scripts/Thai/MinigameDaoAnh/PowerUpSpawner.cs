using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : Spawner
{
    public static PowerUpSpawner Instance;
    public string PowerUpName = "PowerUp";
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void SpawnPowerUpOnInitializeLv()
    {
        ClearPowerUp();
        int rdNum = Random.Range(1, 4);
        switch (rdNum)
        {
            case 1:
                SpawnRandomPos(1);
                break;
            case 2:
                SpawnRandomPos(2);
                break;
            case 3:
                SpawnRandomPos(3);
                break;
        }
    }
    private void SpawnRandomPos(int num)
    {
        for(int i=0; i<num; i++)
        {
            float rdPosX = Random.Range(-9f, 9f);
            float rdPosY = Random.Range(-1f, -4.5f);
            Transform powerUp = Spawn(PowerUpName, new Vector2(rdPosX, rdPosY), Quaternion.identity);
        }
    }
    private void ClearPowerUp()
    {
        foreach (Transform p in holder)
        {
            Despawn(p);
        }
    }
}
