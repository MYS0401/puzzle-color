using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;　//最初の大きさ
    [SerializeField] private float hoverScale = 1.1f; 
    [SerializeField] private float pressedScale = 0.95f;　//選択したときの拡大比率

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale; // 押し終わったらHover状態に戻す
    }
}
