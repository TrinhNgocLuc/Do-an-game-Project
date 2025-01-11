
using UnityEngine;

public class home : MonoBehaviour
{
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject rank;
    [SerializeField] private GameObject exit;
    [SerializeField] private AudioSource SfxButton;
    private pause _pause;
    private bool statusHome = false;
    private bool statusMap = false;
    private bool statusSetting = false;
    private bool statusRank = false;
    private bool statusExit = false;

    void Start()
    {
        option.SetActive(false);
        map.SetActive(false);
        setting.SetActive(false);
       rank.SetActive(false);
        exit.SetActive(false);
        _pause = GetComponent<pause>();
    }

    public void controllHome()
    {
        statusHome = !statusHome;
        SfxButton.Play();
        if (statusHome == true)
        {
      
            option.SetActive(true);
        }
        else {
  
            option.SetActive(false);
        
        }

    }
     public void controllMap()
    {
        statusMap = !statusMap;
        SfxButton.Play();
        if (statusMap == true)
        {
            map.SetActive(true);
            option.SetActive(false);

        }
        else if (statusMap == false) {
            map.SetActive(false);
            option.SetActive(true);

        }
    }
    public void controllSetting()
    {
        SfxButton.Play();
        statusSetting = !statusSetting;
        if (statusSetting == true)
        {
            setting.SetActive(true);
            option.SetActive(false);

        }
        else if (statusSetting == false)
        {
            setting.SetActive(false);

            option.SetActive(true);

        }
    }
    public void controllRank()
    {
        SfxButton.Play();
        statusRank = !statusRank;
        if (statusRank == true)
        {
            rank.SetActive(true);
            option.SetActive(false);

        }
        else if (statusRank == false)
        {
            rank.SetActive(false);
            option.SetActive(true);

        }
    }
    public void controllExit()
    {
        SfxButton.Play();
        statusExit = !statusExit;
        if (statusExit == true)
        {
            exit.SetActive(true);
            option.SetActive(false);

        }
        else { 
            exit.SetActive(false);
            option.SetActive(true);

        }
    }
    public void Exit()
    {
        SfxButton.Play();
        Application.Quit();
    }
}
