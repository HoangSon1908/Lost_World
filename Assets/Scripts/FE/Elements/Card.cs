using DG.Tweening;
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

    [Header("AnimationCardIn")]
    // Khai báo list thẻ phụ
    [SerializeField] private List<RectTransform> cardList = new List<RectTransform>();
    // Khoảng delay giữa các thẻ bài
    [SerializeField] float delayBetweenCards = 0.3f;
    [SerializeField] private GameObject ListCard;

    [Header("Stats")]
    public RulingDays rulingDays;
    private float yPos;
    private RectTransform rect;
    private Vector2 offset;
    private bool isDraggingUp = false;
    private bool isDraggingDown = false;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AnimationCardIn();
    }

    private void AnimationCardIn()
    {
        ResetCard();
        // Di chuyển từng thẻ bài vào màn hình với delay
        for (int i = 0; i < cardList.Count; i++)
        {
            RectTransform card = cardList[i];

            // Đặt vị trí ban đầu của thẻ bài nằm ngoài màn hình
            card.anchoredPosition = new Vector2(-Screen.width, 0);

            // Tạo chuỗi hoạt ảnh
            Sequence sequence = DOTween.Sequence();

            // Di chuyển thẻ bài vượt qua vị trí giữa một chút (ví dụ 50 pixels sang phải)
            sequence.Append(card.DOAnchorPosX(25, 0.5f).SetEase(Ease.OutSine));

            // Sau đó di chuyển thẻ bài về vị trí X = 0
            sequence.Append(card.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutBack));

            // Đặt delay cho mỗi thẻ bài
            sequence.SetDelay(i * delayBetweenCards);

            // Gọi ResetCard() sau khi hoạt ảnh của thẻ cuối cùng kết thúc
            if (i == cardList.Count - 1)
            {
                sequence.OnComplete(() =>
                {
                    ListCard.SetActive(false);
                    Data.instance.MakeDecision();
                });
            }
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        // Convert screen position to local position within the rect
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.root as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        // Calculate offset from the local point within the rect
        offset = rect.anchoredPosition - localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert screen position to local position within the rect
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect.root as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        rect.anchoredPosition = new Vector2(0, localPoint.y + offset.y);
        yPos = localPoint.y + offset.y;
        CalculateFadeText();
        
        if (yPos > 0) 
        {
            if (!isDraggingUp) 
            {
                isDraggingUp = true;
                isDraggingDown = false;

                Choice choice = Data.instance.CurrentChoice;
                StatManager.instance.PreviewStatChange(
                    choice.militaryEffect1,
                    choice.publicEsteem1,
                    choice.economy1,
                    choice.spiritualityEffect1
                );
            }
        }
        else if (yPos < 0) 
        {
            if (!isDraggingDown)
            {
                isDraggingUp = false;
                isDraggingDown = true;

                Choice choice = Data.instance.CurrentChoice;
                StatManager.instance.PreviewStatChange(
                    choice.militaryEffect2,
                    choice.publicEsteem2,
                    choice.economy2,
                    choice.spiritualityEffect2
                );
            }
        }
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    if (yPos > 150)
    {
        rect.DOAnchorPosY(900, .5f).OnComplete(() => 
        {
            ResetCard(); 
            CreateBuff();
            if (!Data.instance.CurrentCharacter.isIntro) 
            {
                Choice choice = Data.instance.CurrentChoice;
                GameManager.Instance.ApplySingleEffect(
                    choice.militaryEffect1,
                    choice.publicEsteem1,
                    choice.economy1,
                    choice.spiritualityEffect1
                );
                StatManager.instance.ApplyStatChanges();
                GameManager.Instance.AddDaysAfterDecision(choice.rulingDays1);
                rulingDays.UpdateDaysUI();
            }
            Data.instance.MakeDecision();
        });
    }
    else if (yPos < -150)
    {
        rect.DOAnchorPosY(-800, .5f).OnComplete(() => 
        {
            ResetCard(); 
            CreateBuff();
            if (!Data.instance.CurrentCharacter.isIntro) 
            {
                Choice choice = Data.instance.CurrentChoice;
                GameManager.Instance.ApplySingleEffect(
                    choice.militaryEffect2,
                    choice.publicEsteem2,
                    choice.economy2,
                    choice.spiritualityEffect2
                );
                StatManager.instance.ApplyStatChanges();
                GameManager.Instance.AddDaysAfterDecision(choice.rulingDays2);
                rulingDays.UpdateDaysUI();
            }
            Data.instance.MakeDecision();
        });
    }
        else
        {
            ResetCard();
        }

        StatManager.instance.HideAllDots();
        isDraggingUp = false;
        isDraggingDown = false;
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
