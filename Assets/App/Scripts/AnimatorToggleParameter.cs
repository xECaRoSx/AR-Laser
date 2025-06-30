using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorToggleParameter : MonoBehaviour
{
    public Animator animator;
    public string parameterName;

    public void ToggleBool()
    {
        if (animator == null || string.IsNullOrEmpty(parameterName))
        {
            Debug.LogWarning("Animator or Parameter name not set.");
            return;
        }

        bool currentValue = animator.GetBool(parameterName);
        animator.SetBool(parameterName, !currentValue);
    }
}
