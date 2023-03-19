using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnimator : MonoBehaviour
{
    List<Animator> _animators;
    public float waitBetween = 0.15f;
    public float waitEnd = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _animators = new List<Animator>(GetComponentsInChildren<Animator>());

        StartCoroutine(DoAnimation());
    }

    void OnEnable()
    {
        _animators = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine(DoAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoAnimation()
    {
        while(true)
        {
            foreach (var animator in _animators)
            {
                animator.SetTrigger("DoAnimation");
                yield return new WaitForSeconds(waitBetween);
            }
            yield return new WaitForSeconds(waitEnd);
        }
    }
}
