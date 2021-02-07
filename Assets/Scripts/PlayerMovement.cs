using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : PlayerStats
{
    private Rigidbody2D PlayerRigidbody;

    private Vector3 velocity = Vector3.zero;
    public Animator animator;

    private float horizontalMovement;
    private float verticalMovement;
    public bool IsMoving;
    public bool isChopping;
    public bool isAttacking;

    public Text interactUI;

    public static PlayerMovement instance;

    public GameObject enemy;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMouvement dans la scène");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        animator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // direction et vitesse
        horizontalMovement = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime; // "Horizontal" = fleches horiz par defaut ! (..Raw = 0 or 1)
        verticalMovement = Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime; // "Vertical" = fleches verticales par defaut !

        UpdateAnimationAndMove();
        MovePlayer(horizontalMovement, verticalMovement);

        animator.SetBool("isChopping", isChopping);

        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
        if(enemy == null)
        {
            enemy = GameObject.FindWithTag("Enemy");
        }
    }



    void UpdateAnimationAndMove()
    {
        if (verticalMovement != 0 || horizontalMovement != 0)
        {
            animator.SetFloat("moveX", horizontalMovement);
            animator.SetFloat("moveY", verticalMovement);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        // on laisse valeur par défaut axe y : PlayerRigidbody.velocity.y

        Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
        PlayerRigidbody.velocity = Vector3.SmoothDamp(PlayerRigidbody.velocity, targetVelocity, ref velocity, .05f);
    }

    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        //if (Vector3.Distance(enemy.transform.position, transform.position) < attackRange)
        //{
            animator.SetBool("Attack", true);
            isAttacking = true;
            //yield return null;
            //enemy.GetComponent<Log>().TakeDamage(strength);
            yield return new WaitForSeconds(1f);
            animator.SetBool("Attack", false);
            isAttacking = false;
        /*}
        else
            {
                transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed* Time.deltaTime);
            }*/
    }
}
