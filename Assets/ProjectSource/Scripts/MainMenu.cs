using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Sounds
{

    private void FixedUpdate()
    {
        if (!AudioIsActive())
            PlaySound(sounds[0]);
    }

    public void PlayGame()
    {
        PlaySound(sounds[1]);
        StartCoroutine(StartGame());
    }
    public void ExitGame()
    {
        PlaySound(sounds[1]);
        StartCoroutine(Exit());
    }


    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainScene");
    }
    public IEnumerator Exit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
