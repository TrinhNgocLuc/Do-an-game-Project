
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectArea : MonoBehaviour
{
    [SerializeField] private AudioSource SfxButton;
    [SerializeField] private checkConnect _checkConnect;
    [SerializeField] private playerData _playerData;
    public void ConnectLibrary()
    {

            SfxButton.Play();
            SceneManager.LoadScene("Library");

    }
    public void ConnectGame1()
    {

            SfxButton.Play();
            SceneManager.LoadScene("StartScene");
    }
    public void ConnectGame2()
    {

            SfxButton.Play();
            SceneManager.LoadScene("Minigame_SapXepChuCai");

    }
    public void ConnectGame3()
    {

            SfxButton.Play();
            SceneManager.LoadScene("Minigame_DaoAnh");

    }
    public void ConnectGame4()
    {

            SfxButton.Play();
            SceneManager.LoadScene("Library");
    }
    public void ConnectGame5()
    {

        SfxButton.Play();
        SceneManager.LoadScene("Library");
    }

    public void Leave()
    {
        SfxButton.Play();
        SceneManager.LoadScene("Screen Main");
    }

}