using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Bab baru", menuName = "Buat Bab Baru")]
public class SoalBab : ScriptableObject
{
    public enum BabMateri
    {
        Rukun_Iman,
        Rukun_Islam,
        Surah_Pendek,
        Wudhu,
        Sholat,
    }

    public BabMateri bab;
    public LevelQuestion[] levelQuestions;
}
