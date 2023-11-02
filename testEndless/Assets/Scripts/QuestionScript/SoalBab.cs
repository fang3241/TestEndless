using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Bab baru", menuName = "Buat Bab Baru")]
public class SoalBab : ScriptableObject
{
    public enum BabMateri
    {
        
        KitabAllah,
        BersikapJujur,
        PengurusanJenazah,
        Wudhu,
        Sholat,
    }

    public BabMateri bab;
    public LevelQuestion[] levelQuestions;
}
