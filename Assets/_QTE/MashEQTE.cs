using UnityEngine;

public class MashE : MonoBehaviour
{
    public SimpleQTE qte;
    public KeyCode key = KeyCode.E;
    public int pressesRequired = 10;
    private int count;

    public int CurrentCount => count; // <-- Add this line

    void Update()
    {
        if (qte == null || !qte.IsActive) return;

        if (Input.GetKeyDown(key))
        {
            count++;
            qte.PlayerInput();

            if (count >= pressesRequired)
            {
                count = 0;
                qte.Succeed();
            }
        }
    }
}

