using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorCheckingCustom : MonoBehaviour
{
    public TMP_Text errorText;
    
    public void OpenError(string e)
    {
        if (e == "Qr")
        {
            errorText.text = "Isi Kumpulan Soal tidak terdeteksi. \nCek kembali format Kumpulan Soal Pilihan Ganda";
        }
        else if (e == "Dr")
        {
            errorText.text = "Isi Kumpulan Soal tidak sesuai (Mungkin nama/jumlah soal berbeda ?). \nCek kembali format Kumpulan Soal Cocok Gambar";
        }

        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CloseError()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
