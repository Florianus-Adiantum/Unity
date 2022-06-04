using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCameraScript : MonoBehaviour
{
    //Hareket etmesini sağlayan bir scripte bagli olan oyun objesi olusturun
    //Kamerayi hiyerarside objenin içine koyun
    //Karakter icin gerekli duzenlemeleri yapin
    //Bu kodu objeye baglayin camera kismina da objenin icindeki kamerayi baglayin
    //FPS kamerasi hazirdir kullanabilirsiniz
    public GameObject camera;
    float sensibility = 5f;
    float softness = 2f;
    Vector2 gecisPos;
    Vector2 camPos;
    public GameObject player;
    void Start()
    {
        player = transform.parent.gameObject;
    }

    
    void Update()
    {
        Vector2 farepos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        farepos = Vector2.Scale(farepos, new Vector2(sensibility*softness,sensibility*softness));
        gecisPos.x = Mathf.Lerp(gecisPos.x, farepos.x, 1f / softness);
        gecisPos.y = Mathf.Lerp(gecisPos.y, farepos.y, 1f / softness);
        camPos += gecisPos;
        transform.localRotation = Quaternion.AngleAxis(-camPos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(camPos.x, player.transform.up);
    }
}
