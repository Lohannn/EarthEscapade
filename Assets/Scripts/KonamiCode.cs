using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Necess�rio para usar List

public class KonamiCode : MonoBehaviour
{
    private readonly KeyCode[] konamiCode = {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A
    };

    private int currentCodeIndex = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode requiredKey = konamiCode[currentCodeIndex];

            if (Input.GetKeyDown(requiredKey))
            {
                currentCodeIndex++;

                if (currentCodeIndex == konamiCode.Length)
                {
                    KonamiCodeActivated();

                    currentCodeIndex = 0;
                }
            }
            else
            {
                currentCodeIndex = 0;
            }
        }
    }

    // Este m�todo � chamado quando o c�digo � digitado com sucesso
    private void KonamiCodeActivated()
    {
        print("Konami Code Activated!");
        GetComponent<Player>().KonamiMode();
    }
}