using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNav : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public enum SceneList
    {
        MainMenu,
        Test_SelectBelajar,
        GamemodeSelect,
        ChapterSelect,
        LevelSelect,
        LevelLand,
        LevelAir,
        LevelSea,
        Settings,

        Test_Grid,
        Test_CustomQUI,
        Test_Drag,
    }

    public void TDragImage()
    {
        LoadScene(SceneList.Test_Drag.ToString());
    }

    public void BackToT_Grid()
    {
        TGrid(GameManager.instance.customType);
        Time.timeScale = 1;
    }

    public void TGrid(int i)
    {
        GameManager.instance.customType = i;
        LoadScene(SceneList.Test_Grid.ToString());
    }

    public void TQUI()
    {
        LoadScene(SceneList.Test_CustomQUI.ToString());
    }


    public void toMenu()
    {
        LoadScene(SceneList.MainMenu.ToString());
    }

    public void toBelajar()
    {
        LoadScene(SceneList.Test_SelectBelajar.ToString());
    }

    public void toGamemodeSelect()
    {
        LoadScene(SceneList.GamemodeSelect.ToString());
    }

    public void toChapterSelect()
    {
        LoadScene(SceneList.ChapterSelect.ToString());
        
        
    }

    public void toLevelSelect(int ch)
    {
        LoadScene(SceneList.LevelSelect.ToString());
        GameManager.instance.selectedChapter = ch;
    }

    public void toLevelLand()
    {
        LoadScene(SceneList.LevelLand.ToString());
    }

    public void toLevelAir()
    {
        LoadScene(SceneList.LevelAir.ToString());
    }

    public void toLevelWater()
    {
        LoadScene(SceneList.LevelSea.ToString());
    }

    public void toSettings()
    {
        LoadScene(SceneList.Settings.ToString());
    }

    public void exit()
    {
        Application.Quit();
    }

    public void LoadScene(string s)
    {
        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        if(sceneLoader != null)
        {
            sceneLoader.PlayAnimation(s);
        }
        else
        {
            SceneManager.LoadScene(s);
        }
    }
}
