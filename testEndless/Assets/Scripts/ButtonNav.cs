using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNav : MonoBehaviour
{
    public enum SceneList
    {
        MainMenu,
        Belajar,
        GamemodeSelect,
        ChapterSelect,
        LevelSelect,
        LevelLand,
        LevelAir,
        LevelSea,
        Settings,

        Test_Grid,
        Test_CustomQUI,
    }

    public void TGrid()
    {
        SceneManager.LoadScene(SceneList.Test_Grid.ToString());
    }

    public void TQUI()
    {
        SceneManager.LoadScene(SceneList.Test_CustomQUI.ToString());
    }


    public void toMenu()
    {
        SceneManager.LoadScene(SceneList.MainMenu.ToString());
    }

    public void toBelajar()
    {
        SceneManager.LoadScene(SceneList.Belajar.ToString());
    }

    public void toGamemodeSelect()
    {
        SceneManager.LoadScene(SceneList.GamemodeSelect.ToString());
    }

    public void toChapterSelect()
    {
        SceneManager.LoadScene(SceneList.ChapterSelect.ToString());
        
    }

    public void toLevelSelect(int ch)
    {
        SceneManager.LoadScene(SceneList.LevelSelect.ToString());
        GameManager.instance.selectedChapter = ch;
    }

    public void toLevelLand()
    {
        SceneManager.LoadScene(SceneList.LevelLand.ToString());
    }

    public void toLevelAir()
    {
        SceneManager.LoadScene(SceneList.LevelAir.ToString());
    }

    public void toLevelWater()
    {
        SceneManager.LoadScene(SceneList.LevelSea.ToString());
    }

    public void toSettings()
    {
        SceneManager.LoadScene(SceneList.Settings.ToString());
    }

    public void exit()
    {
        Application.Quit();
    }
}
