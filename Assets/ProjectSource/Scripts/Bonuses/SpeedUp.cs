using UnityEngine;

public class SpeedUp : Bonuses
{
    public int value = 8;
    public int seconds = 8;

    
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().GetSpeed(value, seconds);
        Destroy(gameObject);
    }

}
