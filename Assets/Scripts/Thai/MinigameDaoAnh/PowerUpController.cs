using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerUpType
{
    BonusTime,
    Strength,
    Lucky
}
public class PowerUpController : MonoBehaviour, IItemType
{
    [SerializeField][Range(0, 10)] private float pickUpSpeedModifier;
    [SerializeField] private GameObject bonusTimeFx;
    public float GetItemPickUpSpeedModifier()
    {
        return pickUpSpeedModifier;
    }
    public void SetBePickedUp(Transform parent) 
    { 
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }
    public void GetRandomPowerUp()
    {
        Player.Instance.SetAnim("Buff");
        int rd = Random.Range(0, 3);
        if (rd == 0)
        {
            bonusTimeFx.SetActive(true);
        }
        else if (rd == 1)
        {
            TongsHandler.Instance.IncreaseMoveSpeedTemp();
        }
        else
            GameManagerDaoAnh.Instance.SetAllRacoonToAnswerImageOnPowerUp();
        AudioManager.instance.PlaySfx(2);
        PowerUpSpawner.Instance.Despawn(transform);
    }
}
