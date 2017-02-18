using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartController : MonoBehaviour {

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public Image heartImage;

    private GameObject heartPanel;
    private List<Image> heartsList = new List<Image>();

    private void Start() {
        PlayerController.HpChangedEvent += HpChanged;

        var hp = PlayerController.Instance.maxHp;
        var maxHearts = (hp + 1) / 2;
        for (int i = 0, offset = 10; i < maxHearts; i++, offset += 60) {
            Image heart = Instantiate(heartImage);
            heart.transform.SetParent(transform);
            RectTransform rt = heart.gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(offset, -10);

            heartsList.Add(heart);
        }
    }

    public void HpChanged(int hp) {
        foreach (Image heart in heartsList) {
            if (hp <= 0) {
                heart.sprite = emptyHeart;
            } else if (hp <= 1) {
                heart.sprite = halfHeart;
            } else {
                heart.sprite = fullHeart;
            }
            hp -= 2;
        }
    }

    //public void MaxHpChanged(int maxhp) {
    //}

}
