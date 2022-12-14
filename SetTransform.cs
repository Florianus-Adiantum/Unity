using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    //bu metod istedi�iniz objenin 3 eksendeki yerini kontrol etmenizi saglar
    //metodu s�rekli olarak veya belli durumlarda cok amacli kullanabilirsiniz
    void Start()
    {
        
    }
    void Update()
    {
        SetTransformXYZ(5, 5, 5);//3 deger icin ornek kullanim
        SetTransformY(5);//tek deger icin ornek kullan�m
    }
    void SetTransformXYZ(float x, float y, float z)//x y z eksenleri icin 3 float degeri alir
    {
        transform.position = new Vector3(x,y,z);
    }
    void SetTransformY(float y)//sadece y eksenini kontrol eder
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        //degistirmek istemediginiz degerlere "transform.position" ifadesi girin
    }
}
