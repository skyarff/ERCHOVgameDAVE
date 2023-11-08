using UnityEngine;

public abstract class Bonuses : MonoBehaviour
{

    private void FixedUpdate()
    {
        transform.Rotate(0f, 2f, 0f);
    }
}
