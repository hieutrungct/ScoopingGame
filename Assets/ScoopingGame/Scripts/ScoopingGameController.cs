using System.Collections;
using UnityEngine;
// namespace Rubik.ScoopingGame
// {
    public class ScoopingGameController : MonoBehaviour
    {
        public static ScoopingGameController instance { get; private set; }
        public Claw claw;
        public ArcadeLeverRotate joystick;
        public BlindBag blindBagPrefab;
        [SerializeField] private Transform blindBagSpawnPoint;
        [SerializeField] private CatchZone catchZone;
        public float clawMoveSpeed;
        public float powerTime;
        public float gameTime;
        public float mulTime;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                // DontDestroyOnLoad(gameObject);
            }
        }
        void Start()
        {
            StartCoroutine(ActiveCatchZoneTemporarily());
        }
        public void StartScooping()
        {
            claw.StartScooping();
        }
        IEnumerator SpawnBlindBag()
        {
            for (int i = blindBagSpawnPoint.childCount - 1; i >= 0; i--)
            {
                // Dùng DestroyImmediate để đảm bảo nó biến mất trước khi Instantiate cái mới
                DestroyImmediate(blindBagSpawnPoint.GetChild(i).gameObject);
            }
            yield return null;
            for (int i = 0; i < 30; i++)
            {
                float randomOffsetX = Random.Range(-0.5f, 0.5f);    
                Vector3 spawnPos = blindBagSpawnPoint.position + new Vector3(randomOffsetX, 0, 0);

                
                BlindBag g = Instantiate(blindBagPrefab, spawnPos, Quaternion.identity, blindBagSpawnPoint);
                g.SetUp();
                g.id = "BlindBag_" + i;
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
        IEnumerator ActiveCatchZoneTemporarily()
        {
            yield return StartCoroutine(SpawnBlindBag());
            yield return new WaitForSeconds(1f); // Giữ nguyên trạng thái trong 10 giây
            catchZone.gameObject.SetActive(true);
        }
    }
// }
