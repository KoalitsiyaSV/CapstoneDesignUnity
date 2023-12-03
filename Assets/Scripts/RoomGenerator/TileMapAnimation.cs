using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapAnimation : MonoBehaviour {
    public GameObject tilemap;
    public float fadeDuration = 1.0f; // 페이드 인/아웃 지속 시간 (초)

    private Material tileMaterial;
    private Color initialColor;
    private float fadeStartTime;
    private void Awake() {
        tilemap = this.gameObject;
    }
    void Start() {
        // 예시: 타일맵의 Material 가져오기
        tileMaterial = tilemap.GetComponent<TilemapRenderer>().material;

        // 타일맵의 초기 색상 가져오기
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

            yield return null; // 한 프레임 대기
        }

        // 페이드 인이 끝난 후에는 초기 색상으로 설정
        tileMaterial.color = initialColor;
    }

    IEnumerator FadeOut() {
        fadeStartTime = Time.time;

        while (Time.time - fadeStartTime < fadeDuration) {
            float normalizedTime = (Time.time - fadeStartTime) / fadeDuration;
            Color lerpedColor = Color.Lerp(initialColor, Color.clear, normalizedTime);
            tileMaterial.color = lerpedColor;

            yield return null; // 한 프레임 대기
        }

        // 페이드 아웃이 끝난 후에는 완전히 투명으로 설정
        tileMaterial.color = Color.clear;
    }
}
