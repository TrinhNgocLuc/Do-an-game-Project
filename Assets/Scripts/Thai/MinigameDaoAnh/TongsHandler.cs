using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongsHandler : MonoBehaviour
{
    public static TongsHandler Instance;
    [SerializeField] private float maxRotateAngle;
    [SerializeField] private float maxPickUpTime = 2f;
    [SerializeField] private float basePickUpSpeed = 2f;
    [SerializeField] private Vector2 tongsOriginalLocalPos;

    public float rotateSpeed;
    public bool canRotate = true;

    private Transform tongs;
    private float angle;
    private bool rotateRight = true;
    public bool isPickingUp { get; private set; } = false;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        GameManagerDaoAnh.Instance.onPlayerTouchingAction += StartPickUp;
        tongs = transform.Find("tongs");
        GameManagerDaoAnh.Instance.onResetGameState += ResetTongs;
        tongsOriginalLocalPos.y = tongs.localPosition.y;
        GameManagerDaoAnh.Instance.onResetGameState += BackToBaseSpeedValue;
    }
    private void ResetTongs()
    {
        tongs.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        tongs.localPosition = new Vector2(tongs.localPosition.x, -.75f);
        if(tongs.childCount > 1)
        {
            if(tongs.GetChild(1).GetComponent<RacoonImage>() != null)
                RacoonSpawner.Instance.Despawn(tongs.GetChild(1));
            else if (tongs.GetChild(1).GetComponent<PowerUpController>() != null)
                PowerUpSpawner.Instance.Despawn(tongs.GetChild(1));
        }
    }
    void FixedUpdate()
    {
        RotateTongs();
        if (!isPickingUp)
            tongs.localPosition = new Vector2(tongs.localPosition.x, tongsOriginalLocalPos.y);
    }
    private void StartPickUp()
    {
        if (isPickingUp)
            return;
        tongs.GetComponent<Tongs>().canGoBack = false;
        isPickingUp = true;
        canRotate = !canRotate;
        StartCoroutine("StartMoveToPickUp");
    }
    private IEnumerator StartMoveToPickUp()
    {
        float pickUpTimer = maxPickUpTime;
        Rigidbody2D tongsRb = tongs.GetComponent<Rigidbody2D>();
        tongsOriginalLocalPos.x = tongs.localPosition.x;
        while (pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongsRb.velocity = -transform.up * basePickUpSpeed;
            pickUpTimer -= Time.fixedDeltaTime;
            if (tongs.GetComponent<Tongs>().canGoBack)
            {
                Debug.Log("Bat dau keo len!");
                tongsRb.velocity = Vector3.zero;
                StartCoroutine(StartMoveBackAfterCollided());
                break;
            }
        }
        if (!tongs.GetComponent<Tongs>().canGoBack)
            StartCoroutine("StartMoveBackAfterPickUp");
    }
    private IEnumerator StartMoveBackAfterPickUp()
    {
        float pickUpTimer = maxPickUpTime / 2f;
        Rigidbody2D tongsRb = tongs.GetComponent<Rigidbody2D>();
        while (pickUpTimer > 0f)
        {
            yield return new WaitForFixedUpdate();
            tongsRb.velocity = transform.up * basePickUpSpeed * 2f;
            pickUpTimer -= Time.fixedDeltaTime;
        }
        tongs.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        tongs.localPosition = tongsOriginalLocalPos;
        canRotate = true;
        isPickingUp = false;
    }
    private IEnumerator StartMoveBackAfterCollided()
    {
        float speedModifier = tongs.GetComponent<Tongs>().GetPickingUpItemSpeedModifier();
        Rigidbody2D tongsRb = tongs.GetComponent<Rigidbody2D>();
        while (Vector2.Distance(tongsOriginalLocalPos, tongs.localPosition) > .1f)
        {
            yield return new WaitForFixedUpdate();
            tongsRb.velocity = transform.up * basePickUpSpeed * speedModifier;
        }
        Debug.Log("Stop Pull!");
        tongsRb.velocity = Vector3.zero;
        tongs.localPosition = tongsOriginalLocalPos;
        canRotate = true;
        isPickingUp = false;
        tongs.GetComponent<Tongs>().PulledUpImageAndCheck();
    }
    private void RotateTongs()
    {
        if (!canRotate)
            return;
        if (angle > maxRotateAngle - 1f && rotateRight)
        {
            rotateRight = false;
            maxRotateAngle = -maxRotateAngle;
            Debug.Log("Change Direction");
        }
        else if (angle < maxRotateAngle + 1f && !rotateRight)
        {
            rotateRight = true;
            maxRotateAngle = -maxRotateAngle;
            Debug.Log("Change Direction");
        }
        angle = Mathf.MoveTowards(angle, maxRotateAngle, rotateSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public void IncreaseMoveSpeedTemp()
    {
        if(IsInvoking("BackToBaseSpeed"))
        {
            CancelInvoke("BackToBaseSpeed");
            Invoke("BackToBaseSpeed", 15f);
            GameManagerDaoAnh.Instance.ShowThunderFx();
            return;
        }
        basePickUpSpeed *= 2.5f;
        maxPickUpTime /= 2.5f;
        Invoke("BackToBaseSpeed", 15f);
        GameManagerDaoAnh.Instance.ShowThunderFx();
    }
    private void BackToBaseSpeed()
    {
        if(Vector2.Distance(new Vector2(tongs.localPosition.x, tongsOriginalLocalPos.y), tongs.localPosition) > .1f)
        {
            StartCoroutine(StopPowerUp());
            return;
        }
        basePickUpSpeed /= 2.5f;
        maxPickUpTime *= 2.5f;
    }
    public void BackToBaseSpeedValue()
    {
        if (IsInvoking("BackToBaseSpeed"))
            CancelInvoke("BackToBaseSpeed");
        basePickUpSpeed = 6;
        maxPickUpTime = 2;
    }
    private IEnumerator StopPowerUp() 
    { 
        while(Vector2.Distance(new Vector2(tongs.localPosition.x, tongsOriginalLocalPos.y), tongs.localPosition) > .1f)
        {
            yield return null;
        }
        basePickUpSpeed /= 2.5f;
        maxPickUpTime *= 2.5f;
    }
}

