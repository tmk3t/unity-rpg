using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HPのテキスト追加
using UnityEngine.UI;

public class SlimeRabbitController : MonoBehaviour
{
    Animator animator;
    //GetComponentせずアタッチする事も可能
    public Rigidbody rb;
    //UnityEngine.UIがなければ使えない。HPのテキストを操作するために定義
    public Text hitPointText;



    //動くスピード
    public float moveSpeed = 0.1f;
    //HP
    public float hitPoint = 100f;
    //動く向き
    private float moveDirection = 0f;
    //ジャンプ力
    public float jumpPower = 10f;

    private Vector3 move;


    //地面接触判定
    private bool isGrounded = false;
    //地面の位置
    public Transform groundPoint;
    //地面のLayer
    public LayerMask groundLayers;


    //死んだかどうか判定
    private bool isDead = false;

    //カメラの位置をviewPointというObjectのTransformから取得
    //スライムラビットが死んだ（Destroyされた）後、カメラが消えてしまうのを防ぐ（Destroyされないのでカメラは消えない）
    public Transform viewPoint;
    //カメラを取得
    private Camera playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        //AnimatorをGetComponentする
        animator = GetComponent<Animator>();
        //AnimatorのHitPointにHPを設定
        animator.SetFloat("HitPoint", hitPoint);
        //カメラを取得
        playerCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        LateUpdate();
        //デフォルトのアニメーションを設定
        animator.SetBool("Move", false);
        //アニメーターのパラメーターがHPに一致するように
        animator.SetFloat("HitPoint", hitPoint);
        //テキストのHPの数値を更新
        hitPointText.text = "HP: " + animator.GetFloat("HitPoint");

        //死んでいたら動けなくする
        if (isDead != true)
        {
            Move();
        }

        //HPが0以下になったら死ぬ
        if (animator.GetFloat("HitPoint") <= 0f)
        {
            animator.SetTrigger("Death");
            isDead = true;
        }

    }
    //カメラの位置と角度をアップデートする
    private void LateUpdate()
    {
        playerCamera.transform.position = viewPoint.position;
        playerCamera.transform.rotation = viewPoint.rotation;
    }

    //動きをまとめた関数
    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Move", true);
            transform.position = transform.position + transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("Move", true);
            //マイナス1をかけて後ろに進む
            transform.position = transform.position + transform.forward * moveSpeed * -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Move", true);
            moveDirection = 2f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Move", true);
            moveDirection = -2f;
        }
        else
        {
            moveDirection = 0f;
        }
        //XとZの向きは変えず、YだけをMoveDirection分動かす。
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + moveDirection,
            transform.rotation.eulerAngles.z
            );

        isGrounded = Physics.Raycast(groundPoint.position, Vector3.down, .25f, groundLayers);
        if (isGrounded == true)
        {
            move.y = transform.position.y;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Jump", true);
                move.y = jumpPower;
            }
        }
        else
        {
            move.y += Physics.gravity.y * Time.deltaTime * 10;
        }
        transform.position = move;

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Damageを与える物体に衝突したら
        if (collision.gameObject.tag == "DamageObject")
        {
            animator.SetBool("Damage", true);
            //HPを減らす。なお50という数値は適当なので、Bulletなど個別に設定した数値を取得してHPを減らすのも良いかもしれない
            hitPoint -= 50f;
            //今回のBulletはぶつかった後はDestroyしておく
            Destroy(collision.gameObject);
        }
    }

}
