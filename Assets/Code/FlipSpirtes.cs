using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpirtes : MonoBehaviour {

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float interval = 1f;

    void Flip(bool x, bool y) {

        sprite.flipX = x;
        sprite.flipY = y;
    }

    private void Start() {
        StartCoroutine(AnimateSpriteFlip());
    }

    IEnumerator AnimateSpriteFlip() {
        while (true) {
            Flip(true, false);
            yield return new WaitForSeconds(interval);
            Flip(true, true);
            yield return new WaitForSeconds(interval);
            Flip(false, true);
            yield return new WaitForSeconds(interval);
            Flip(false, false);
            yield return new WaitForSeconds(interval);
        }
    }

}
