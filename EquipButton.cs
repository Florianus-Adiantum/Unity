using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    private int gunNo;//silah numarasi
    private AudioSource audioSource;

    public Transform currentTransform;//su anki slot
    public AudioClip dropGun, equipGun;
    private void Start()
    {
        gunNo = GetComponent<GunNo>().gunNo;
        audioSource = GetComponent<AudioSource>();
    }
    public void EquipItem()
    {
        //objenin durumunu kontrol et
        bool isInGunsPanel = InventoryControl.gunListNumbers.Contains(gunNo);
        Transform[] targetTransforms = isInGunsPanel ? InventoryControl.instance.inventoryTransforms : InventoryControl.instance.gunsTransforms;
        List<int> sourceList = isInGunsPanel ? InventoryControl.gunListNumbers : InventoryControl.inventoryNumbers;
        List<int> targetList = isInGunsPanel ? InventoryControl.inventoryNumbers : InventoryControl.gunListNumbers;
        GameObject targetPanel = isInGunsPanel ? InventoryControl.instance.inventoryPanel : InventoryControl.instance.gunsPanel;

        //bos slot bul
        Transform emptyTransform = FindEmptyTransform(targetTransforms);
        
        if(emptyTransform != null)//silah alinabilir ise
        {           
            if (isInGunsPanel)//silah kusanilacak
            {
                audioSource.PlayOneShot(equipGun, 1f);
            }
            else//silah birakilacak
            {
                audioSource.PlayOneShot(dropGun, 1f);
            }
        }
        else//silah alinamaz
        {
            audioSource.PlayOneShot(dropGun, 1f);
        }
        if (emptyTransform != null)
        {
            //tasi
            transform.SetParent(targetPanel.transform, false);
            transform.position = emptyTransform.position;

            //listeleri guncelle
            sourceList.Remove(gunNo);
            targetList.Add(gunNo);

            //yeni transformu dolu yap
            emptyTransform.GetComponent<TransformState>().isOccupied = true;

            //eski transformu bos yap ve yeni transform olarak guncelle
            currentTransform.GetComponent<TransformState>().isOccupied = false;
            currentTransform = emptyTransform;

        }

        UpdateInventory();
    }
    void PrintInventory()//envanteri yazdirir
    {
        for (int i = 0; i < PlayerPrefs.GetInt("InventoryListCount") ; i++)
        {
            Debug.Log(PlayerPrefs.GetInt("InventoryList_" + i));
        }
    }
    void UpdateInventory()//envanteri kaydeder
    {
        PlayerPrefs.SetInt("InventoryListCount", InventoryControl.inventoryNumbers.Count);

        for (int i = 0; i < InventoryControl.inventoryNumbers.Count ; i++)
        {
            PlayerPrefs.SetInt("InventoryList_" + i, InventoryControl.inventoryNumbers[i]);
        }

        PlayerPrefs.Save();
    }
    Transform FindEmptyTransform(Transform[] transforms)//bos slot bulucu
    {
        foreach (Transform t in transforms)
        {
            TransformState state = t.GetComponent<TransformState>();
            if (state != null && !state.isOccupied)
            {
                return t;
            }
        }
        return null;
    }
}
