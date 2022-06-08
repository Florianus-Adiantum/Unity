using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeceGunduzDongusu : MonoBehaviour
{
    //Gunes ve ay olmak uzere iki directional light olusturun
    //Biri terrainin altinda digeri ustunde ve ikisi de terraine bakacak sekilde yerlestirin
    //Gunes y ekseninde 500 degerde ise ay -500 olmali
    //Yorungenin daha saglam olmasi icin gunes ve ayi hiyerarside bos bir objenin icine koyun
    //Bu kodu iki objeye de baglayin
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.RotateAround (new Vector3(500f,0,500f),Vector3.right,1f*Time.deltaTime);
    }
}
