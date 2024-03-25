using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Money10, Money50, Money100, SpeedFast, SpeedSuperFast, JoinShop, EndEnum }

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private GameObject mask;
    [SerializeField] private RectTransform slotGroup;
    private Vector2 startPos;
    private bool isPickingItem;

    public bool IsPickingItem => isPickingItem;

    private void Awake()
    {
        startPos = slotGroup.anchoredPosition;
    }

    public void GetRandomItem(System.Action<ItemType> action)
    {
        StartCoroutine(ItemPickRoutine(action));
    }

    private IEnumerator ItemPickRoutine(System.Action<ItemType> action)
    {
        isPickingItem = true;
        mask.SetActive(true);
        var item = Random.Range(0, (int)ItemType.EndEnum);
        slotGroup.DOAnchorMove(new Vector2(0, 150 * item), 1.5f, Ease.OutQuart);
        yield return new WaitForSeconds(1.5f);
        action?.Invoke((ItemType)item);

        yield return new WaitForSeconds(1);
        slotGroup.anchoredPosition = startPos;
        mask.SetActive(false);
        isPickingItem = false;
    }
}
