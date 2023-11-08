using UnityEngine;
using UnityEngine.AI;

public class Enemy : Sounds
{

    public int damage = 15;
    public float viewAngle = 160f;
    public float viewDistance = 38f;
    public float detectionDistance = 10f;
    public Transform enemyEye;
    private Transform target;
    private Transform cameraP;
    private NavMeshAgent agent;
    private Canvas canvas;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 15;
        agent.acceleration = 10;
        FindCanvasInChildrenRecursive(transform);
        cameraP = GameObject.FindGameObjectWithTag("MainCamera").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
        }

    }

    void LateUpdate() => canvas.transform.LookAt(cameraP);
    void Update()
    {

        float distanceToPlayer = Vector3.Distance(target.transform.position, agent.transform.position);

        if (distanceToPlayer <= detectionDistance || IsInView())
        {

            bool flag = AudioIsActive();
            
            if (!flag)
            {
                PlaySound(sounds[0]);
            }

            agent.SetDestination(target.position);

        }

    }


    private bool IsInView()
    {
        float realAngle = Vector3.Angle(enemyEye.up, target.position - enemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyEye.transform.position, target.position - enemyEye.position, out hit, viewDistance))
        {
            if (realAngle < viewAngle / 2f && Vector3.Distance(enemyEye.position, target.position) <= viewDistance && hit.transform == target.transform)
            {
                return true;
            }
        }
        return false;
    }
    void FindCanvasInChildrenRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Canvas childCanvas = child.GetComponent<Canvas>();
            if (childCanvas != null)
            {
                canvas = childCanvas;
                break;
            }
            //FindCanvasInChildrenRecursive(child);
        }
    }
}
