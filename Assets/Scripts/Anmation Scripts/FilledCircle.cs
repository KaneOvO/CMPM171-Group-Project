using UnityEngine;
using System.Collections;

public class FilledCircle : MonoBehaviour
{
    public float duration = 1f; // 动画持续时间
    public float originalSizeFactor = 0.1f; // 圆形的大小
    public float maxSizeFactor = 1.5f; // 圆形的大小因子
    private int vertexCount = 80; // 圆形的顶点数
    private float radius = 0.25f; // 圆形的半径
    private Coroutine animationCoroutine; // 倒计时协程
    private void Awake()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        gameObject.SetActive(false);
        meshRenderer.material = new Material(Shader.Find("Sprites/Default")); // 设置填充材质
        meshRenderer.material.color = new Color(Random.value, Random.value, Random.value); // 设置填充材质颜色

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[vertexCount + 1];
        int[] triangles = new int[vertexCount * 3];

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        for (int i = 0; i < vertexCount; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            vertices[i] = new Vector3(x, y, 0f);
            theta += deltaTheta;
        }

        vertices[vertexCount] = Vector3.zero; // 中心点

        for (int i = 0; i < vertexCount; i++)
        {
            triangles[i * 3] = i;
            triangles[i * 3 + 1] = (i + 1) % vertexCount;
            triangles[i * 3 + 2] = vertexCount; // 中心点
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;


    }

    public void OnEnable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        float timer = Time.time;
        Color color = GetComponent<MeshRenderer>().material.color;
        while (Time.time - timer < duration)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(originalSizeFactor, maxSizeFactor, (Time.time - timer) / duration);
            color.a = Mathf.Lerp(1, 0, (Time.time - timer) / duration);
            GetComponent<MeshRenderer>().material.color = color;
            yield return null;
        }
        transform.localScale = Vector3.one * maxSizeFactor;
        GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0f);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = null;
        transform.localScale = Vector3.one * originalSizeFactor;
        GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}
