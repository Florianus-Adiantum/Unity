using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPosition2D : MonoBehaviour
{
    //Bu kod 2D oyunlar icindir
    //Bagladiginiz 2d objenin mouse ile es zamanli saga ve sola hareket etmesini saglar
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 farePos=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f));
        transform.position=new Vector3(farePos.x,transform.position.y,transform.position.z);s
    }
}
