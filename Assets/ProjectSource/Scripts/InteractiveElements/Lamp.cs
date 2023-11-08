using UnityEngine;
using UnityEngine.AI;

public class Lamp : Sounds, IInteractable
{
    public Light m_Light;
    public bool isOn;
    public bool day;
    public bool scream;
    public GameObject[] enemies;
    public Light sun;

    void Start()
    {
        m_Light.enabled = isOn;
    }

    public string GetDescription()
    {
        if (isOn)
        {
            return "Press [E] to turn <color=red>off</color> the light.";
        }

        return "Press [E] to turn <color=green>on</color> the light.";
    }
    public void Interact()
    {

        PlaySound(sounds[0], destroyed: true);

        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
            
        }

        if (sounds.Length > 1 && scream)
        {
            scream = false;
            PlaySound(sounds[1]);
        }

        if (day)
        {
            day = false;
            Quaternion quaternion = Quaternion.Euler(70f, 0f, 0f);
            Vector3 vector3 = new Vector3(0f, 100f, 0f);
            sun.transform.SetPositionAndRotation(vector3, quaternion);
        }

        isOn = !isOn;
        m_Light.enabled = isOn;
    }

    
}
