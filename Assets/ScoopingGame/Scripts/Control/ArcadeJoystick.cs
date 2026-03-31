// using Rubik.ScoopingGame;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArcadeLeverRotate : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Cấu hình Visual")]
    public RectTransform leverStick; // Kéo phần thanh gạt vào đây
    public float maxTiltAngle = 35f;  // Độ nghiêng tối đa sang trái/phải
    public float sensitivity = 0.5f;  // Độ nhạy khi kéo

    private float horizontalInput; // Giá trị từ -1 (trái) đến 1 (phải)
    public float HorizontalInput => horizontalInput;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out localPos);

        // Tính toán độ lệch ngang so với tâm vùng cảm ứng
        float width = GetComponent<RectTransform>().rect.width;
        horizontalInput = Mathf.Clamp(localPos.x / (width * sensitivity), -1f, 1f);

        // Chỉ thực hiện xoay (Rotation), không thay đổi vị trí (Position)
        // Xoay quanh trục Z để nghiêng trái/phải trong không gian 2D
        leverStick.localRotation = Quaternion.Euler(0, 0, -horizontalInput * maxTiltAngle);
        
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Trả về trạng thái thẳng đứng khi buông tay
        horizontalInput = 0;
        leverStick.localRotation = Quaternion.identity;
    }
}