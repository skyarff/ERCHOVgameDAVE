using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Apple : Bonuses
{
    public int value = 25;

   
    private void OnTriggerEnter(Collider other)
    {
        
        other.gameObject.GetComponent<Player>().GetHealth(value);
        Destroy(gameObject);
    }

}
