using UnityEngine;

public class MinimapToggle : MonoBehaviour
{
    public GameObject minimapUI;
    public GameObject back;
    public Camera minimapCamera;

    private bool isOpen = false;

    void Start()
    {
        minimapUI.SetActive(false);
        back.SetActive(false);
        minimapCamera.enabled = false;
    }

    void Update()
    {
        if (InputManager.Instance.MiniMapInput)
        {
            isOpen = !isOpen;

            StatusState statusState = CharCtrl.Instance.CharStateCtrl.StatusState;
            if (isOpen)
            {
                statusState.ChangeState(statusState.cutScene);
            }
            else
            {
                statusState.ChangeState(statusState.normal);
            }

            minimapUI.SetActive(isOpen);
            back.SetActive(isOpen);
            minimapCamera.enabled = isOpen;
        }
    }
}
