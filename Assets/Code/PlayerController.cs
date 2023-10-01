using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public Transform moveTarget;

    public LayerMask barrierObjects;

    public Animator playerAnim;
    [SerializeField] RoundManager2 roundManager2;

    private void Start() {
        moveTarget.parent = null;
        roundManager2 = FindObjectOfType<RoundManager2>();
    }

    public IEnumerator PlayerWalk() {
        if (roundManager2.state == RoundState.PLAYERTURN) {
            //playerAnim.SetBool("moving", true);
            while (Vector3.Distance(transform.position, moveTarget.position) > .000005f) {

                //Debug.Log("Player currently walking");
                transform.position = Vector3.Lerp(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, moveTarget.position) <= .000005f) {
                    //Hier Zeug für Player Runden Ende:
                    transform.position = moveTarget.position;
                    //playerAnim.SetBool("moving", false);
                    roundManager2.state = RoundState.ENEMYTURN;
                    Debug.Log("Ende wurde ausgeführt.");
                    roundManager2.EnemyTurn();
                }
                yield return null;
            }


            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                // Check if colliding horizontally
                if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, barrierObjects)) {
                    moveTarget.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                // Check if colliding vertically
                if (!Physics2D.OverlapCircle(moveTarget.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, barrierObjects)) {
                    moveTarget.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }


        }
    }


    void ReachedGoal() {

    }

}
