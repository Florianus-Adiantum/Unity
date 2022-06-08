using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraAnimasyon2D : MonoBehaviour
{
    //Bu kod 2D oyunlar icindir
    //Bolum baslangicini daha estetik hale getirip sahne gecisindeki puruzleri yok eder
    //Kodu kameraya baglayin sahne her yuklendiginde kamera y ekseninde 2 birim asagidan olmasi gereken pozisyona gelir
    private Vector3 ilkPos;
    void Start()
    {
        ilkPos = transform.position;
        transform.position= new Vector3(ilkPos.x, ilkPos.y - 2f, ilkPos.z);
    }

    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ilkPos, 2 * Time.deltaTime);
    }
}
