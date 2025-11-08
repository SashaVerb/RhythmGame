using UnityEngine;
using Zenject;

public class CameraAreaRect : ITickable
{
    private Camera camera;
    private Rect rect;

    private int lastScreenWidth;
    private int lastScreenHeight;

    public CameraAreaRect(Camera camera)
    {
        this.camera = camera;
        UpdateRect();
    }

    public Rect Get => rect;

    private void UpdateRect()
    {
        var leftDownPos = camera.ViewportToWorldPoint(Vector3.zero);
        var rightUpPos = camera.ViewportToWorldPoint(Vector3.one);
        rect = Rect.MinMaxRect(leftDownPos.x, leftDownPos.y, rightUpPos.x, rightUpPos.y);
    }

    public void Tick()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            UpdateRect();
        }
    }
}
