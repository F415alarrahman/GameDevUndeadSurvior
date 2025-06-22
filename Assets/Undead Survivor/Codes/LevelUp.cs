using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    // Start is called before the first frame update
     public void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
        rect.localScale = Vector3.zero; // sembunyikan di awal
    }

    public void Show()
    {
        rect.localScale = Vector3.one; // munculkan UI
        Next();                        // generate item random
        GameManager.Instance.Stop();   // pause game
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume(); // lanjutkan game
    }

    public void Select(int index)
    {
        items[index].OnClick(); // aktifkan item
        Hide();                 // sembunyikan UI dan resume
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2])
                break;
        }

        for (int i = 0; i < ran.Length; i++)
        {
            Item ranItem = items[ran[i]];
            ranItem.gameObject.SetActive(true);
        }
    }
}
