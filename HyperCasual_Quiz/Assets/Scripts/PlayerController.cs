using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    Animator anim;
    public int sorularIndis;

    public float soruGecisSuresi = 0.5f;

    public GameObject finishPanel;

    public GameObject nextButtonObj;

    public static PlayerController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Soru", 0);
        sorularIndis = PlayerPrefs.GetInt("Soru");
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (GameManager.instance.isStarted == true)
        {
            transform.Translate(0, 0, 5f * Time.deltaTime);
            anim.SetBool("Run", true);


            float newX = 0;
            //float x = Input.GetAxis("Horizontal");
            float touchDelta = 0f;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
            }
            else if(Input.GetMouseButton(0))
            {
                touchDelta = Input.GetAxis("Mouse X");
            }

            newX = transform.position.x + 300f * touchDelta * Time.deltaTime;
            newX = Mathf.Clamp(newX, -3, 3);
            Vector3 newPos = new Vector3(newX, transform.position.y, transform.position.z + 7.5f * Time.deltaTime);
            transform.position = newPos;
        }
        else
        {
            //anim.SetBool("Run", false);
        }

        if (soruGecisSuresi > 0)
        {
            soruGecisSuresi -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dogru") && soruGecisSuresi != 0.5f)
        {
            Debug.Log("Dogru + puan");
            score++;
            scoreText.text = score.ToString();
            sorularIndis++;
            PlayerPrefs.SetInt("Soru", PlayerPrefs.GetInt("Soru") + 1);
            GameManager.instance.Sorular(sorularIndis);
            soruGecisSuresi = 0.5f;
            other.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
        }
        if (other.gameObject.CompareTag("Yanlis") && soruGecisSuresi != 0.5f)
        {
            Debug.Log("Yanlýþ - puan");
            score--;
            scoreText.text = score.ToString();
            sorularIndis++;
            PlayerPrefs.SetInt("Soru", PlayerPrefs.GetInt("Soru") + 1);
            GameManager.instance.Sorular(sorularIndis);
            soruGecisSuresi = 0.5f;
            other.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
        }

        if (other.gameObject.CompareTag("Finish"))
        {

            GameManager.instance.isStarted = false;

            if (score >= 1)
            {
                anim.SetTrigger("Win");
            }
            else if (score < 1)
            {
                anim.SetTrigger("Lose");
            }

            finishPanel.SetActive(true);
            nextButtonObj.transform.DOScale(new Vector3(1, 1, 1), 2f);
        }
    }


}
