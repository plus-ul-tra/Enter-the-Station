using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemDataSO itemData;
    private SpriteRenderer spriteRenderer;
    public void SetUp(ItemDataSO data)
    {
        itemData = data;
        spriteRenderer.sprite = data.icon;
    }

    public void Picked()
    {
        //Debug.Log("ащащ");
        gameObject.SetActive(false);
    }
}
