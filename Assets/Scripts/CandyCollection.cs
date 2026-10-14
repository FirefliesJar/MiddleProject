using UnityEngine;
using TMPro;

public class CandyCollection : MonoBehaviour
{
    [SerializeField] private CharacterSwitch characterSwitch;
    [SerializeField] private LockFall lockFall;

    private int candy = 0;

    public TextMeshProUGUI candyText;

    private void Start()
    {
        candyText.text = candy.ToString() + "/3";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("PickUp") || characterSwitch.currentCharacterIndex != 2)
        {
            return;
        }

        candy++;

        candyText.text = candy.ToString() + "/3";

        Debug.Log("Candy! Total: " + candy);

        Destroy(other.gameObject);

        if (candy == 3)
        {
            Debug.Log("You Win!");

            lockFall.DropLock();
        }
    }
}