using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMenuManager : MonoBehaviour
{
    private Animator animator;
    private bool isMenuOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isMenuOpen = true;
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        animator.SetBool("isMenuOpen", isMenuOpen);
    }


}
