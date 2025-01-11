
using System.Collections;
using System.Configuration;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{
    [SerializeField] private AudioSource SfxButton;
    public GameObject Instruct;
    public GameObject Login;
    public GameObject Sighup;
    public GameObject Noti_Sighup_success;
    public GameObject Noti_Sighup_fail;
    public GameObject Noti_Login_fail;
    public bool statusSighupNoti;
    public bool status_LoginSightup = true;
    public GameObject Main_menu;

    private bool statusInstruct = false;

    void Start()
    {
     Instruct.SetActive(false);   
     Main_menu.SetActive(false);
     Noti_Sighup_success.SetActive(false);
     Noti_Sighup_fail.SetActive(false);
     Sighup.SetActive(false);
     Noti_Login_fail.gameObject.SetActive(false);

    }

    public void show_SighupLogin()
    {
        status_LoginSightup = !status_LoginSightup;
        Login.SetActive(status_LoginSightup);
        Sighup.SetActive(!status_LoginSightup);
    }
    public void ShowSighupNoti()
    {
        if (statusSighupNoti)
        {
            Noti_Sighup_success.gameObject.SetActive(true);
            StartCoroutine(HideNotiSighupSuccessAfterDelay());
        }
        else 
        {
            Noti_Sighup_fail.gameObject.SetActive(true);
            StartCoroutine(HideNotiSighupFailAfterDelay());
        }
    }
    public void ShowLoginNoti()
    {

            Noti_Login_fail.gameObject.SetActive(true);
            StartCoroutine(HideNotiLoginFailAfterDelay());
    }
    public IEnumerator HideNotiSighupFailAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Noti_Sighup_fail.gameObject.SetActive(false);
    }
    public IEnumerator HideNotiSighupSuccessAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Noti_Sighup_success.gameObject.SetActive(false);
    }
    public IEnumerator HideNotiLoginFailAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Noti_Login_fail.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        SfxButton.Play();
        SceneManager.LoadScene("Screen Main");
    }
    public void InstructGame()
    {
        SfxButton.Play();
        statusInstruct = !statusInstruct;
        if (statusInstruct)
        {
            Instruct.SetActive(true);
        }
        else
        {
            Instruct.SetActive(false);
        }
        
    }
    public void Exit()
    {
        SfxButton.Play();
        Application.Quit();
    }

}