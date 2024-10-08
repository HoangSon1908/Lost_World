using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // Nhớ phải thêm DOTween vào project

public class SceneTransition : MonoBehaviour
{
    //singleton
    public static SceneTransition instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            FadeIn();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Image fadeImage; // Image dùng cho hiệu ứng fade
    public float fadeDuration = 0.5f; // Thời gian fade

    public void FadeIn()
    {
        // Fade từ alpha 1 (đen) về 0 (trong suốt)
        fadeImage.DOFade(0, fadeDuration);
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        // Fade từ trong suốt (alpha 0) về đen (alpha 1) rồi chuyển scene
        fadeImage.DOFade(1, fadeDuration).OnComplete(() => {
            SceneManager.LoadScene(sceneName);
        });
    }
}
