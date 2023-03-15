using StarterAssets;
using System.Collections;
using UnityEngine;
using UI;

public class InteractSystem : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private Animator _doorAnimator;
    private GameObject door;
    public float rayLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForColliders();
    }

    void CheckForColliders()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            switch (hit.collider.tag)
            {
                case "Chest":
                    GameObject chest = hit.collider.gameObject;
                    Animator chestAnimator = chest.GetComponentInChildren<Animator>();

                    InteractUI.ActivateInfoText(true);

                    if (_input.interact)
                    {
                        chestAnimator.SetTrigger("Open");
                        InteractUI.ActivateInfoText(false);
                        chest.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
                    }

                    break;

                case "Key":
                    InteractUI.ActivateInfoText(true);
                    if (_input.interact)
                    {
                        Destroy(hit.collider.gameObject);
                        InteractUI.ActivateInfoText(false);
                        GameManager.s_hasKey = true;
                    }
                    break;

                case "Door":
                    InteractUI.ActivateInfoText(true);
                    if (_input.interact)
                    {
                        if (GameManager.s_hasKey)
                        {
                            door = hit.collider.gameObject;
                            _doorAnimator = door.GetComponent<Animator>();
                            if (_doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
                            {
                                _doorAnimator.SetTrigger("Open");
                                _doorAnimator.ResetTrigger("Close");
                            }
                            else if (
                                _doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen")
                            )
                            {
                                _doorAnimator.SetTrigger("Close");
                                _doorAnimator.ResetTrigger("Open");
                            }
                        }
                        else
                        {
                            GameManager.Instance.closedText.SetActive(true);
                        }
                    }

                    break;

                default:
                    InteractUI.ActivateInfoText(false);
                    GameManager.Instance.closedText.SetActive(false);
                    break;
            }
        }
        else
        {
            InteractUI.ActivateInfoText(false);
            GameManager.Instance.closedText.SetActive(false);
            _input.interact = false;
        }
    }
}
