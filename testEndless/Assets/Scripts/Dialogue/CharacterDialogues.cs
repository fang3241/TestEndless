using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Percakapan baru", menuName = "Buat Percakapan Baru")]
public class CharacterDialogues : ScriptableObject
{
    public int level;
    public Sprite bgImage;
    public Dialogue[] dialogues;
}
