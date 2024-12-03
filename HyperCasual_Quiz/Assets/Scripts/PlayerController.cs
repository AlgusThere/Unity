using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    Animator anim;
    int sorularIndis;

    public float soruGecisSuresi = 0.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.instance.Sorular(sorularIndis);
    }

    void Update()
    {
        if (GameManager.instance.isStarted == true)
        {
            transform.Translate(0, 0, 10 * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (soruGecisSuresi > 0)
        {
            soruGecisSuresi -= Time.deltaTime;
        }

        float newX = 0;
        float x = Input.GetAxis("Horizontal");

        newX = transform.position.x + 10f * x * Time.deltaTime;
        newX = Mathf.Clamp(newX, -3, 3);
        Vector3 newPos = new Vector3(newX, transform.position.y, transform.position.z + 10 * Time.deltaTime);
        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dogru") && soruGecisSuresi != 0.5f)
        {
            Debug.Log("Dogru + puan");
            score++;
            scoreText.text = score.ToString();
            sorularIndis++;
            GameManager.instance.Sorular(sorularIndis);
            soruGecisSuresi = 0.5f;
        }
        if (other.gameObject.CompareTag("Yanlis") && soruGecisSuresi != 0.5f)
        {
            Debug.Log("Yanlýþ - puan");
            score--;
            scoreText.text = score.ToString();
            sorularIndis++;
            GameManager.instance.Sorular(sorularIndis);
            soruGecisSuresi = 0.5f;
        }
    }


}
