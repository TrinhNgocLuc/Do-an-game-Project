
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource SfxButton;
    [SerializeField] private GameObject Instruct;
    private bool statusInstruct = false;

    void Start()
    {
     Instruct.SetActive(false);   
    }
    public void StartGame()
    {
        SfxButton.Play();
        SceneManager.LoadScene("Main");
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