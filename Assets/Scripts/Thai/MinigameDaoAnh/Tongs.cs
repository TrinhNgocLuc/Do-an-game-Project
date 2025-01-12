using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tongs : MonoBehaviour
{
    public bool canGoBack = false;
    public float pickUpSpeedModifier { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponent<Rigidbody2D>().velocity.y > .1f || canGoBack)
            return;
        if (collision.GetComponent<IItemType>() != null)
        {
            canGoBack = true;
            collision.GetComponent<IItemType>().SetBePickedUp(transform);
            pickUpSpeedModifier = collision.GetComponent<IItemType>().GetItemPickUpSpeedModifier();
        }
    }
    public float GetPickingUpItemSpeedModifier() => pickUpSpeedModifier;
    public void PulledUpImageAndCheck()
    {
        if (transform.childCount < 2)
            return;
        Transform transf = transform.GetChild(1);
        if(transf.GetComponent<PowerUpController>() != null)
        {
            transf.GetComponent<PowerUpController>().GetRandomPowerUp();
            return;
        }
        if(transf.gameObject.activeInHierarchy)
        {
            if(GameManagerDaoAnh.Instance.CheckImagePulled(transf.GetComponent<RacoonImage>().GetImage()))
            {
                RacoonSpawner.Instance.Despawn(transf);
                GameManagerDaoAnh.Instance.SetNextVocaAfterPulledSucess();
                GameManagerDaoAnh.Instance.SetNewImageAfterPulledSucess(transf.GetComponent<RacoonImage>().GetImage());
                GameManagerDaoAnh.Instance.SetAllRacoonBackToTheirImage();
            } else
            {
                transf.GetComponent<RacoonMovement>().RunAwayAfterPullWrong();
            }
        }
    }
}
