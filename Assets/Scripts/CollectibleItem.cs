using UnityEngine;
using DG.Tweening;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;
    public Sprite itemIcon;

    private bool collected = false;

    private void OnMouseDown()
    {
        if (collected) return;

        collected = true;

        transform.DOPunchScale(Vector3.one * 0.3f, 0.2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                GameManager.instance.CollectItem(this);
            });
    }
}