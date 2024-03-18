using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MovingText : MonoBehaviour
{
    public bool moving = true;
    public float speed = 10f;
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveText());
    }

    public IEnumerator MoveText()
    {
        while (moving)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            yield return null;
        }
    }
}
