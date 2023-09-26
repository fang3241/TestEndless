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
        SelectBelajar,
        GamemodeSelect,
        ChapterSelect,
        LevelSelect,
        LevelLand,
        LevelAir,
        LevelSea,
        Settings,

        SelectLevelCustom,
        CustomQuestion,
        CustomDragnDrop,
        ShowBelajar,

        Dialogue,
    }

    public void toDialogue()
    {
        LoadScene(SceneList.Dialogue.ToString());
    }

    public void toShowBelajar(int i)
    {
        GameManager.instance.selectedBab = i;

        switch (i)
        {
            case 0:
                GameManager.instance.customTitle = "Materi Rukun Iman";
                break;
            case 1:
                GameManager.instance.customTitle = "Materi Rukun Islam";
                break;
            case 2:
                GameManager.instance.customTitle = "Materi Surah Pendek";
                break;
            case 3:
                GameManager.instance.customTitle = "Materi Wudhu";
                break;
            case 4:
                GameManager.instance.customTitle = "Materi Sholat";
                break;
            default:
                GameManager.instance.customTitle = "Materi Pembelajaran";
                break;
        }

        LoadScene(SceneList.ShowBelajar.ToString());
    }

    public void TDragImage()
    {
        LoadScene(SceneList.CustomDragnDrop.ToString());
    }

    public void BackToT_Grid()
    {
        TGrid(GameManager.instance.customType);
        Time.timeScale = 1;
    }

    public void TGrid(int i)
    {
        GameManager.instance.customType = i;
        LoadScene(SceneList.SelectLevelCustom.ToString());
    }

    public void TQUI()
    {
        LoadScene(SceneList.CustomQuestion.ToString());
    }


    public void toMenu()
    {
        LoadScene(SceneList.MainMenu.ToString());
    }

    public void toBelajar()
    {
        LoadScene(SceneList.SelectBelajar.ToString());
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
        Time.timeScale = 1;
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        if(sceneLoader != null)
        {
            sceneLoader.PlayAnimation(s);
        }
        else
        {
            Debug.Log("SCENE LOADER KOSONG");
            SceneManager.LoadScene(s);
        }
    }
    
    public string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public int getCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public bool checkIfSceneActive(SceneList s)
    {
        return getCurrentSceneName() == s.ToString();
    }
}
