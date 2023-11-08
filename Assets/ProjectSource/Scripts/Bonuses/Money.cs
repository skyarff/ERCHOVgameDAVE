using UnityEngine;


public class Money : Bonuses
{
    public int cost = 5;

   
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().GetMoney(cost);
        Destroy(gameObject);
    }

}
