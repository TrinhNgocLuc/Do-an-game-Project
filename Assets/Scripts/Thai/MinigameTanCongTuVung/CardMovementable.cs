using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardMovementable : MonoBehaviour
{

    [SerializeField] private float moveSpeedEasy;
    [SerializeField] private float moveSpeedMedium;
    [SerializeField] private float moveSpeedHard;
    private float speed;
    private void OnEnable()
    {
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                speed = moveSpeedEasy;
                break;
            case Difficulty.normal:
                speed = moveSpeedMedium;
                break;
            case Difficulty.hard:
                speed = moveSpeedHard;
                break;
        }
        CancelInvoke("Despawn");
        Invoke("Despawn", 6f);
        GetComponent<CanvasGroup>().alpha = 1.0f;
        transform.Find("RedFx").gameObject.SetActive(false);
        transform.Find("ExplosionFx").gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(0f, speed);
    }
    private void Despawn()
    {
        ImageGameplaySpawner.Instance.Despawn(transform);
    }
    public void StopMoving() => speed = 0f;
    public void CancelInvokeRunning() => CancelInvoke("Despawn");
}
