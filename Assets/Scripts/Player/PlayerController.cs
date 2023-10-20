using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    private CharacterController controller;
    private Vector3 dir;
    private int currentLane = 1;               //Chia đường ra 3 làn : 0 = trái, 1 = giữa, 2 = phải   (lấy làn giữa làm mặc định)
    public float laneDistance = 2.5f;          //Khoảng cách giữa 2 làn
    public float jumpForce = 10;               //Lực nhảy
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding = false;     //Biến check player trượt

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        //Check đã ấn tap to play chưa nếu chưa ko cho di chuyển
        if (!PlayerManager.isGameStarted)
            return;

        //Tăng tốc độ player
        if (speed < maxSpeed)
            speed += 0.1f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);
        //Điều khiển di chuyển player
        dir.z = speed;

        if (controller.isGrounded)    //Kiểm tra player chạm đất mới được nhảy
        {
            if (SwipeManager.swipeUp)
                Jump();

            if (SwipeManager.swipeDown && !isSliding)           
                StartCoroutine(Slide());           
        }
        else
        {
            dir.y += Gravity * Time.deltaTime;
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
                dir.y = -10;
            }
        }
        

        if (SwipeManager.swipeLeft)  //Trái
        {
            currentLane--;

            if (currentLane == -1)
                currentLane = 0;
        }
        if (SwipeManager.swipeRight)  //Phải
        {
            currentLane++;

            if (currentLane == 3)
                currentLane = 2;
        }

        //Tính toán vị trí mới của player
        Vector3 newPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLane == 0)       
            newPosition += Vector3.left * laneDistance;      
        else if (currentLane == 2)       
            newPosition += Vector3.right * laneDistance;

        if (transform.position != newPosition)
        {
            Vector3 diff = newPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
            

        //di chuyển người chơi
        controller.Move(dir * Time.deltaTime);
    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    //Xử lý va chạm với chướng ngại vật(gameover)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            FindAnyObjectByType<AudioManager>().PlaySound("GameOver");
            PlayerManager.gameOver = true;
        }
    }

    //Trượt
    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);     //thay đổi trong collider
        controller.height = 1;                            //

        //Trượt trong 0.6s
        yield return new WaitForSeconds(0.6f);

        controller.center = new Vector3(0, 0, 0);         //thay đổi trong collider trả lại gtri cũ
        controller.height = 2;                            //
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}