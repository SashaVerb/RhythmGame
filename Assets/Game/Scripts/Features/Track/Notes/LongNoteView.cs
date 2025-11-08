using UnityEngine;

public class LongNoteView : MonoBehaviour, I2DSize
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteMask mask;

    public Vector2 Size => spriteRenderer.size;

    private void Awake()
    {
        SetMaskSizeAsSprite();
        mask.enabled = false;
    }

    public void SetHeight(float height)
    {
        var size = spriteRenderer.size;
        size.y = height;
        spriteRenderer.size = size;

        var pos = spriteRenderer.transform.localPosition;
        pos.y = height * 0.5f;
        spriteRenderer.transform.localPosition = pos;

        SetMaskSizeAsSprite();
    }

    public void StartActivateEffect(Vector3 borderPosition)
    {
        mask.enabled = true;
        UpdateMaskPosition(borderPosition);
    }

    public void UpdateMaskPosition(Vector3 borderPosition)
    {
        float minY = spriteRenderer.bounds.min.y;
        float scaleY = Mathf.Max(borderPosition.y - minY, 0f);

        Vector3 scale = mask.transform.localScale;
        scale.y = scaleY;

        mask.transform.localScale = scale;
        mask.transform.position = borderPosition + Vector3.down * mask.transform.localScale.y * 0.5f;
    }

    public void StopEffect()
    {
        //mask.enabled = false;
    }

    private void SetMaskSizeAsSprite()
    {
        var scale = mask.transform.localScale;
        scale.y = spriteRenderer.bounds.size.y;
        mask.transform.localScale = scale;
    }
}
