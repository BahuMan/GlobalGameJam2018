using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteScript : MonoBehaviour {

    public Sprite HoverSprite;
    public Sprite ClickSprite;
    public Sprite BaseSprite;

    private Sprite _view;

    private void Start()
    {
        _view = GetComponent<Image>().sprite;
    }

    public void OnHover()
    {
        _view = HoverSprite;
    }

    public void OnClick()
    {
        _view = ClickSprite;
    }

    public void OnExit()
    {
        _view = BaseSprite;
    }
}
