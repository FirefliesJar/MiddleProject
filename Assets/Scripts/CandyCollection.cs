using UnityEngine;
using TMPro;

public class CandyCollection : MonoBehaviour
{
    private int Candy = 0;

    public TextMeshProUGUI candyText;

    private void Start()
    {
        candyText.text = Candy.ToString() + "/3";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            Candy++;

            candyText.text = Candy.ToString() + "/3";

            Debug.Log("Candy! Total: " + Candy);

            Destroy(other.gameObject);

            if (Candy == 3)
            {
                Debug.Log("You Win!");
            }
        }
    }
}