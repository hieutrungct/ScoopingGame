using UnityEngine;
using UnityEngine.UI;

public class ItemFly : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private ParticleSystem trailEffect; 

    // Hàm để code chính gọi khi bắt đầu bay
    public void PlayTrail()
    {
        if (trailEffect != null)
        {
            trailEffect.Play();
        }
    }

    // Hàm để code chính gọi khi vừa bay tới đích (trước khi Destroy)
    public void StopTrailAndDisconnect()
    {
        if (trailEffect != null)
        {
            trailEffect.Stop();
            // Tách Particle khỏi đồng xu để hạt không bị xóa đột ngột
            trailEffect.transform.SetParent(null); 
            // Tự xóa Particle System sau khi các hạt cuối cùng biến mất
            Destroy(trailEffect.gameObject, trailEffect.main.duration + trailEffect.main.startLifetime.constantMax);
        }
    }
    public void SetUpImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
