using UnityEngine;
using Lean.Touch;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float sensitivity = 5.0f;
    [SerializeField] private float smoothing = 2.0f;

    public GameObject character;
    private Vector2 mouseLook;
    private Vector2 smoothV;

    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

    void Start()
    {
        character = this.transform.parent.gameObject;
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    }

    void OnDestroy()
    {
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }

    private void HandleFingerUpdate(LeanFinger finger)
    {
        if (finger.ScreenPosition.x > Screen.width * 0.3f)
        {
            var md = new Vector2(finger.ScaledDelta.x, finger.ScaledDelta.y);
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, minVerticalAngle, maxVerticalAngle);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
    }
}
