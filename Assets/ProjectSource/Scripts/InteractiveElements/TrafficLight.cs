using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TrafficLight : Sounds, IInteractable
{
    public Light redLight;
    public Light greenLight;
    public Light yellowLight;


    public MeshRenderer redSphere;
    public MeshRenderer yellowSphere;
    public MeshRenderer greenSphere;


    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public Material offMaterial; // Прозрачный материал


    public bool isOn;
    public bool day;
    public bool scream;
    public GameObject[] enemies;
    public Light sun;


    public float redDuration = 5f;
    public float yellowDuration = 2f;
    public float greenDuration = 5f;

    private Coroutine trafficLightCoroutine;

    private float range = 45f;
    private float intensity = 2.5f;

    void Start()
    {

        redLight.range = range;
        redLight.intensity = intensity;
        redLight.color = Color.red;


        yellowLight.range = range;
        yellowLight.intensity = intensity;
        yellowLight.color = Color.yellow;


        greenLight.range = range;
        greenLight.intensity = intensity;
        greenLight.color = Color.green;

        TurnOffAllLights();

        if (isOn) trafficLightCoroutine = StartCoroutine(TrafficLightCycle());
    }

    public string GetDescription()
    {
        if (isOn)
        {
            return "Press [E] to turn <color=red>off</color> the light.";
        }
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    private void TurnOffAllLights()
    {
        redLight.enabled = false;
        yellowLight.enabled = false;
        greenLight.enabled = false;


        redSphere.material = offMaterial;
        yellowSphere.material = offMaterial;
        greenSphere.material = offMaterial;
    }

    private IEnumerator TrafficLightCycle()
    {
        while (isOn)
        {
            TurnOffAllLights();
            redLight.enabled = true;
            redSphere.material = redMaterial;
            yield return new WaitForSeconds(redDuration);


            TurnOffAllLights();
            yellowLight.enabled = true;
            yellowSphere.material = yellowMaterial;
            yield return new WaitForSeconds(yellowDuration);


            TurnOffAllLights();
            greenLight.enabled = true;
            greenSphere.material = greenMaterial;
            yield return new WaitForSeconds(greenDuration);


            TurnOffAllLights();
            yellowLight.enabled = true;
            yellowSphere.material = yellowMaterial;
            yield return new WaitForSeconds(yellowDuration);
        }

        TurnOffAllLights();
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

        if (isOn)
        {
            trafficLightCoroutine = StartCoroutine(TrafficLightCycle());
        }
        else
        {
            if (trafficLightCoroutine != null)
            {
                StopCoroutine(trafficLightCoroutine);
                trafficLightCoroutine = null;
            }
            TurnOffAllLights();
        }
    }
}