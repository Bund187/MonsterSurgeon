using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : EnemyController
{
    public Transform player;
    public float followSpeed;
    public float detectionRange = 2f;
    //public GameObject exclamation;
    //public AudioClip warningClip;

    private Vector2 initialPosition;
    private bool isFollowing;
    private Coroutine coroutine;
    private bool isHalfFounded=false;
    

    public bool IsHalfFounded { get => isHalfFounded; set => isHalfFounded = value; }

    private void Start()
    {
        initialPosition = this.transform.position;
    }

    void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                isFollowing = true;
                this.transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * followSpeed);
            }
            else
            {
                isFollowing = false;
                this.transform.position = Vector2.MoveTowards(transform.position, initialPosition, Time.deltaTime * followSpeed);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall") && !isFollowing)
        {
            if (coroutine == null)
            {
                print(this.gameObject.name + " se inicia la coroutine " + collision.transform.tag);
                coroutine = StartCoroutine(Utils.TimeAction(5, ActionReady =>
                {
                    if (ActionReady)
                    {
                        StopCoroutine(coroutine);
                        print("se para la coroutine dentro");
                    }
                }));
            }
        }
        else
        {
            StopCurrentCoroutine();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            StopCurrentCoroutine();
        }
    }

    public void StopCurrentCoroutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            print("se para la coroutine");
        }
    }

    //public void FadeEnemy()
    //{
    //    if (anim!=null && !isHalfFounded && (Vector2)this.transform.position != initialPosition)
    //    {
    //        anim.SetTrigger("fade");            
    //    }
    //}

    //Se llama desde la animacion de fade del enemigo
    //public void RestartPosition()
    //{
    //    this.transform.position = initialPosition;
    //}
    public void StopMovement()
    {
        player = null;
    }
}
