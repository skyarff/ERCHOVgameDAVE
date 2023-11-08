using TMPro;
using UnityEngine;

public class PlayerInteraction : Sounds
{
    public Camera mainCam;
    public float interactionDistance = 10f;
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    void Update()
    {
        InteractionRay();
    }

    private void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
    }
}
