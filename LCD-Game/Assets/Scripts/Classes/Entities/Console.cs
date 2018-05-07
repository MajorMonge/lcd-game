using System;
using UnityEngine;

public class Console : MonoBehaviour
{
    private Animator animator;
    public ControllerMap[] Inputs;

    private string[] directions = new string[2] { "Left", "Right" };

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void PressButton(string button)
    {
        animator.SetFloat("Btn" + button + "Press", 1f);
    }

    private void ReleaseButton(string button)
    {
        animator.SetFloat("Btn" + button + "Press", 0f);
    }

    private void Update()
    {
        ManageInputs();
    }

    private void ManageInputs()
    {
        foreach (var input in Inputs)
        {
            if (Array.Exists(directions, x => x == input.consoleButton))
            {
                float axisH = 0;

                if (Input.GetButton("Horizontal") || Input.GetAxisRaw("Horizontal") != 0)
                {
                    axisH = Input.GetAxisRaw("Horizontal");
                }

                if (axisH > 0)
                {
                    PressButton("Right");
                    ReleaseButton("Left");
                }

                if (axisH < 0)
                {
                    PressButton("Left");
                    ReleaseButton("Right");
                }

                if (axisH == 0)
                {
                    ReleaseButton("Right");
                    ReleaseButton("Left");
                }

                continue;
            }

            if (Input.GetButtonDown(input.inputName))
            {
                PressButton(input.consoleButton);
            }

            if (Input.GetButtonUp(input.inputName))
            {
                ReleaseButton(input.consoleButton);
            }
        }
    }
}