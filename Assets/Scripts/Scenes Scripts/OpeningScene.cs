using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
public class OpeningScene : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    [Header("Scene Script Object: Title Scene")]
    public AssetReference titleScene;
    private Queue<GameObject> queue = new Queue<GameObject>();
    public uint circleCount = 20;
    public float duration = 1f;
    public float radius = 2f;
    public TextMeshPro openingText;
    void Awake()
    {
        openingText = GetComponent<TextMeshPro>();
        openingText.rectTransform.rotation = Quaternion.Euler(0, 0, 10);
        for (int i = 0; i < circleCount; i++)
        {
            GameObject circle = new GameObject($"FilledCircle{i}");
            circle.AddComponent<FilledCircle>();
            circle.transform.SetParent(transform);
            circle.SetActive(false);
            queue.Enqueue(circle);
        }
        StartCoroutine(CircleAnimation());
        StartCoroutine(NextScene());
    }
    public GameObject GetCircle()
    {
        GameObject obj = null;
        if (queue.Count > 0 && !queue.Peek().activeSelf)
        {
            obj = queue.Dequeue();
        }
        else
        {
            obj = new GameObject($"FilledCircle{queue.Count}");
            obj.AddComponent<FilledCircle>();
            obj.transform.SetParent(transform);
            obj.SetActive(false);
        }
        queue.Enqueue(obj);
        return obj;
    }
    private IEnumerator CircleAnimation()
    {
        float round = 0f;
        float timer = Time.time;
        while (true)
        {
            Vector3 pos = Vector3.up * Mathf.Sin(round * 2f * Mathf.PI) * radius + Vector3.right * Mathf.Cos(round * 2f * Mathf.PI) * radius;
            GameObject obj = GetCircle();
            obj.transform.position = pos;
            obj.SetActive(true);
            yield return new WaitForSeconds(duration / circleCount);
            round -= 1f / circleCount;
            round = round - (int)round;
        }
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            loadEventSO.RaiseEvent(titleScene, true);
        }
    }
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(duration * 5);
        loadEventSO.RaiseEvent(titleScene, true);
    }
}
