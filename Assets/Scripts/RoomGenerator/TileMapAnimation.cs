using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapAnimation : MonoBehaviour {
    public GameObject tilemap;
    public float fadeDuration = 1.0f; // ���̵� ��/�ƿ� ���� �ð� (��)

    private Material tileMaterial;
    private Color initialColor;
    private float fadeStartTime;
    private void Awake() {
        tilemap = this.gameObject;
    }
    void Start() {
        // ����: Ÿ�ϸ��� Material ��������
        tileMaterial = tilemap.GetComponent<TilemapRenderer>().material;

        // Ÿ�ϸ��� �ʱ� ���� ��������
        initialColor = tileMaterial.color;
    }
    public void StartFadeOut() {
        StartCoroutine(FadeOut());
    }
    public void StartFadeIn() {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() {
        fadeStartTime = Time.time;

        while (Time.time - fadeStartTime < fadeDuration) {
            float normalizedTime = (Time.time - fadeStartTime) / fadeDuration;
            Color lerpedColor = Color.Lerp(Color.clear, initialColor, normalizedTime);
            tileMaterial.color = lerpedColor;

            yield return null; // �� ������ ���
        }

        // ���̵� ���� ���� �Ŀ��� �ʱ� �������� ����
        tileMaterial.color = initialColor;
    }

    IEnumerator FadeOut() {
        fadeStartTime = Time.time;

        while (Time.time - fadeStartTime < fadeDuration) {
            float normalizedTime = (Time.time - fadeStartTime) / fadeDuration;
            Color lerpedColor = Color.Lerp(initialColor, Color.clear, normalizedTime);
            tileMaterial.color = lerpedColor;

            yield return null; // �� ������ ���
        }

        // ���̵� �ƿ��� ���� �Ŀ��� ������ �������� ����
        tileMaterial.color = Color.clear;
    }
}
