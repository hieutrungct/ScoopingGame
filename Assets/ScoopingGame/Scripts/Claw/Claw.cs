using System.Collections;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class Claw : MonoBehaviour
    {
        public float angle;
        public float openSpeed = 60f; // Tốc độ mở claw (độ/giây)
        public GameObject clawLeft;
        public GameObject clawRight;
        private ScoopingGameController gameCtrl;
        [SerializeField] private bool isScooping = false; // Kiểm tra đang gắp hay không
        void Start()
        {
            gameCtrl = ScoopingGameController.instance;
            gameCtrl.clawMoveSpeed = 5f;
        }
        void Update()
        {
            if (gameCtrl.powerTime > 0)
            {
                gameCtrl.clawMoveSpeed = 10f;
                gameCtrl.powerTime -= Time.deltaTime;
            }
            else
            {
                gameCtrl.clawMoveSpeed = 5f;
            }
        }
        void FixedUpdate()
        {
            if (isScooping) return; // Nếu đang gắp thì không cho di chuyển bằng joystick
            if (gameCtrl.joystick.HorizontalInput > 0.1f)
            {
                MoveRight();
            }
            else if (gameCtrl.joystick.HorizontalInput < -0.1f)
            {
                MoveLeft();
            }
        }
        public void StartScooping()
        {
            if (!isScooping)
            {
                StartCoroutine(ScoopRoutine());
            }
        }
        private IEnumerator ScoopRoutine()
        {
            isScooping = true;

            // 1. Hạ xuống cho đến khi chạm mốc 12f
            while (transform.localPosition.y > 11f || angle < 30f)
            {
                float newY = transform.localPosition.y - gameCtrl.clawMoveSpeed * Time.deltaTime;
                newY = Mathf.Max(newY, 11f); // Không cho xuống quá 12
                transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
                
                angle += openSpeed * Time.deltaTime;
                angle = Mathf.Clamp(angle, 0, 30f);
                UpdateClawRotation();

                yield return null; 
            }

            
            
            // Tạm dừng một chút cho cảm giác thật hơn
            yield return new WaitForSeconds(0.5f);

            // 2. Kéo lên lại 
            while (transform.localPosition.y < 23f || angle > 0f)
            {
                float newY = transform.localPosition.y + gameCtrl.clawMoveSpeed * Time.deltaTime;
                newY = Mathf.Min(newY, 23f); 
                transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

                angle -= openSpeed * Time.deltaTime;
                angle = Mathf.Clamp(angle, 0, 30f);
                UpdateClawRotation();
                yield return null;
            }

            isScooping = false;
        }
        public void MoveLeft()
        {
            Vector3 currentPos = transform.localPosition;
            
            // Tính toán vị trí X mới
            float newX = currentPos.x - gameCtrl.clawMoveSpeed * Time.deltaTime;

            // Khóa giá trị X trong khoảng từ -3.05 đến 3.05
            newX = Mathf.Clamp(newX, -3.05f, 3.05f);

            // Gán lại vị trí mới (giữ nguyên Y và Z)
            transform.localPosition = new Vector3(newX, currentPos.y, currentPos.z);
        }

        public void MoveRight()
        {
            Vector3 currentPos = transform.localPosition;
            
            float newX = currentPos.x + gameCtrl.clawMoveSpeed * Time.deltaTime;

            newX = Mathf.Clamp(newX, -3.05f, 3.05f);

            transform.localPosition = new Vector3(newX, currentPos.y, currentPos.z);
        }

        private void UpdateClawRotation()
        {
            clawLeft.transform.localRotation = Quaternion.Euler(0, 0, -angle);
            clawRight.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        

    }
}
