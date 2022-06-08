using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    //Bu kodu kameraya bagladiginizda mouse tekerlegi ile zoom yapabilmenizi saglar
    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 75)
                Camera.main.fieldOfView += 2;
            

        }
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 5)
                Camera.main.fieldOfView -= 2;
            
        }

       
    }
}
