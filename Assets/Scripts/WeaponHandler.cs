using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;

    public void EnableWeapong()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapong()
    {
        weaponLogic.SetActive(false);
    }

}
