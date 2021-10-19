using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneContoller contoller;
    [SerializeField] private AudioClip openCardSound;
    [SerializeField] private AudioClip closeCardSound;
    private Animator animator;
    private bool isOpened = false;
    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    private void OnMouseDown()
    {
        if (!isOpened && contoller.CanReveal)
        {
            animator.SetBool("OpenCard", true);
            GetComponent<AudioSource>().PlayOneShot(openCardSound);
            isOpened = true;
            contoller.CardRevealed(this);
        }
    }

    private void OnMouseEnter()
    {
        if (!isOpened)
            animator.SetBool("IncreaseCard", true);
    }

    private void OnMouseExit()
    {
        animator.SetBool("IncreaseCard", false);
    }

    public void Unreveal()
    {
        animator.SetBool("OpenCard", false);
        GetComponent<AudioSource>().PlayOneShot(closeCardSound);
        isOpened = false;
    }

    public void DestroyCard()
    {
        cardBack.SetActive(false);
        animator.SetBool("DestroyCard", true);
        StartCoroutine(Destruction());
    }

    private IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IncreaseCard", false);
    }
}
