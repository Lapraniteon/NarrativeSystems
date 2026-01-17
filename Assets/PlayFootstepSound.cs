using StarterAssets;
using UnityEngine;

public class PlayFootstepSound : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;
    
    public void PlayFootstep()
    {
        thirdPersonController?.OnFootstepSimple();
    }
}
