using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TestEvent01 : BasicEvent
{
    public TestEvent01() : base("TestEvent01", false) { }
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        UIManager.Instance.testText.text = "Event 01 is started";
        Item item = ItemManager.Instance.ID("00");
        GameObject test_sprite = new GameObject("test_sprite");
        test_sprite.transform.parent = EventManager.Instance.transform;
        test_sprite.AddComponent<SpriteRenderer>();
        Sprite loadedSprite = Resources.Load<Sprite>("test_sprite_00.png");
    }

    public override void OnExit()
    {
        UIManager.Instance.testText.text = "Event 01 is over";
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.Instance.NextEvent();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}
