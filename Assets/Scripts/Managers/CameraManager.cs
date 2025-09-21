using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CameraController cameraController;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void SetFocus(Transform newFocus)
    {
        cameraController.SetTarget(newFocus);
    }
}
