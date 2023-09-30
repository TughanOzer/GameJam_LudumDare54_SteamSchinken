using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform moveTarget;

    public LayerMask barrierObjects;

    public Animator playerAnim;

    public GameRoundManager gameRoundManager;

    private void Start() {
        moveTarget.parent = null;
        gameRoundManager.GetComponent<GameRoundManager>();
    }

    private void FixedUpdate() {
        if (!gameRoundManager.ongoingRoundEnemy && gameRoundManager.ongoingRoundPlayer) {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, moveTarget.position) <= .05f) {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {

                    if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, barrierObjects)) {
                        moveTarget.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {

                    if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, barrierObjects)) {
                        moveTarget.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    }
                }
                //playerAnim.SetBool("moving", false);

                //EnemyTurn
                gameRoundManager.FinishPlayerTurn();
            }

            else {
                //playerAnim.SetBool("moving", true);
                
            }
        }
    }




}
