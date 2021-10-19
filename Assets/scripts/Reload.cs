using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    private Animator anim;
    private const float increaseScale = 0.19f;
    private const float commonScale = 0.1478f;
    public void OnMouseUp()
    {
        SceneManager.LoadScene(0);
    }

    public void TurnOnAnimation(bool value)
    {
        anim.SetBool("On", value);
    }

    public void OnMouseEnter()
    {
        TurnOnAnimation(true);
    }

    public void OnMouseExit()
    {
        TurnOnAnimation(false);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}