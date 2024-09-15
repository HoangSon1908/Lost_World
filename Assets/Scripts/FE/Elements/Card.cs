﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    #region Test
    [Header("Test")]
    [SerializeField] private Transform buffParent;
    #endregion

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI topAnswer;
    [SerializeField] private TextMeshProUGUI bottomAnswer;

    private float yPos;
    private RectTransform rect;
    private Vector2 offset;

    [SerializeField] private List<RectTransform> cardList = new List<RectTransform>();

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AnimationCardIn();
        //ResetCard();
    }

    private void AnimationCardIn()
    {
        // Khoảng delay giữa các thẻ bài
        float delayBetweenCards = 0.3f;

        // Di chuyển từng thẻ bài vào màn hình với delay
        for (int i = 0; i < cardList.Count; i++)
        {
            RectTransform card = cardList[i];

            // Đặt vị trí ban đầu của thẻ bài nằm ngoài màn hình
            card.anchoredPosition = new Vector2(-Screen.width, 0);

            // Thực hiện hoạt ảnh di chuyển thẻ bài vào giữa màn hình
            card.DOAnchorPosX(0, 1f)
                .SetEase(Ease.OutBack)
                .SetDelay(i * delayBetweenCards); // Mỗi thẻ bay vào chậm hơn so với thẻ trước đó
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect.root as RectTransform, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos);
        offset = rect.localPosition + globalMousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPosition = eventData.position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.root as RectTransform, pointerPosition, eventData.pressEventCamera, out Vector2 localPoint);

        rect.anchoredPosition = new Vector2(0, localPoint.y + offset.y);
        yPos = localPoint.y;
        CalculateFadeText();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (yPos > 150)
        {
            rect.DOAnchorPosY( 900, .5f).OnComplete(() => { ResetCard(); });
            CreateBuff();
        }
        else if (yPos < -150)
        {
            rect.DOAnchorPosY(-800, .5f).OnComplete(() => { ResetCard(); });
            CreateBuff();
        }
        else
        {
            ResetCard();
        }
    }

    //Tinh toan va ap dung do trong suot cua text
    private void CalculateFadeText()
    {
        float alpha = Mathf.Abs(yPos / 150f);
        alpha = Mathf.Clamp01(alpha);

        if (yPos > 0f)
            FadeAnswerText(topAnswer, alpha);

        if (yPos < 0f)        
            FadeAnswerText(bottomAnswer, alpha);
        
    }

    //Dua card ve cac gia gia tri ban dau
    private void ResetCard()
    {
        rect.anchoredPosition = Vector2.zero;
        FadeAnswerText(topAnswer, 0);
        FadeAnswerText(bottomAnswer, 0);
    }

    //Thay doi do trong suot cua text
    private void FadeAnswerText(TextMeshProUGUI text, float alpha)
    {
        Color currentColor = topAnswer.color;
        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }

    #region Test
    private void CreateBuff()
    {
        if (GameManager.Instance.amountOfBuff >= 5) return;

        GameManager.Instance.AddBuff();

        GameObject buffSpawn = Resources.Load<GameObject>($"Prefabs/Buff/Buff");
        GameObject newBuff = Instantiate(buffSpawn, buffParent);

        Color randomColor = Random.ColorHSV();
        newBuff.GetComponentInChildren<Image>().color = randomColor;
    }
    #endregion
}
