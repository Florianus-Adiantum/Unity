using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    //Bu kod 2D oyunlarda buton ile sag sol hareketi saglar
    //Kodu oyun karakterinize baglayin
    //Sag ve sol butonlari olusturun
    //Event trigger ekleyip her iki butona da pointer down ve pointer up komutu ekleyin
    //Pointer down kimina sag veya sol metodlarini ekleyin
    //Pointer up kismina da stop metodunu ekleyin
    Rigidbody2D rb;
    float speed = 15f;
    bool sol = false, sag = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (sol)
        {
            rb.velocity = new Vector2(-speed, 0f);
        }
        if (sag)
        {
            rb.velocity = new Vector2(speed, 0f);
        }
    }
    public void Sol()
    {
        sol = true;
    }
    public void Sag()
    {
        sag = true;
    }
    public void Stop()
    {
        sag = false;
        sol = false;
        rb.velocity = Vector2.zero;
    }
}
