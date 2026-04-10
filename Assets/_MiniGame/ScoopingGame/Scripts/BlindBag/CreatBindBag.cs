using System.Collections;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class CreatBindBag : MonoBehaviour
    {
        // void Start()
        // {
        //     StartCoroutine(SpawnBlindBag());
        // }
        
        public IEnumerator SpawnBlindBag()
        {
            var userScoopTiny = ScoopTinyManager.Instance.GetScoopTinyData();
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                // Dùng DestroyImmediate để đảm bảo nó biến mất trước khi Instantiate cái mới
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            yield return null;
            for (int i = 0; i < userScoopTiny.PoolTinyData[0].TinyNumberData.Count; i++)
            {
                for (int j = 0; j < userScoopTiny.PoolTinyData[0].TinyNumberData[i].Number; j++)
                {
                    float randomOffsetX = Random.Range(-0.5f, 0.5f);    
                    Vector3 spawnPos = transform.position + new Vector3(randomOffsetX, 0, 0);

                    
                    BlindBag g = Instantiate(GameController.instance.blindBagPrefab, spawnPos, Quaternion.identity, transform);
                    g.SetUp();
                    g.id = "BlindBag_" + userScoopTiny.PoolTinyData[0].TinyNumberData[i].TinyType + "_" + j;
                    g.tinyType = userScoopTiny.PoolTinyData[0].TinyNumberData[i].TinyType;
                    Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                    
                    if (rb != null)
                    {
                        // Reset vận tốc về 0 trước khi bắn (đảm bảo lực tác động chính xác)
                        rb.linearVelocity = Vector2.zero;

                        // Tạo lực ngẫu nhiên nhỏ (Ví dụ: X từ -2 đến 2, Y từ 1 đến 4)
                        float randomX = Random.Range(-2f, 2f);
                        float randomY = Random.Range(1f, 4f); 
                        Vector2 smallForce = new Vector2(randomX, randomY);

                        // Áp dụng lực ngẫu nhiên vào Rigidbody2D
                        rb.AddForce(smallForce, ForceMode2D.Impulse);
                        yield return null; // Delay nhỏ giữa các lần spawn để tránh chồng lên nhau quá nhiều    
                    }
                }
            }
            
        }
    }
}
