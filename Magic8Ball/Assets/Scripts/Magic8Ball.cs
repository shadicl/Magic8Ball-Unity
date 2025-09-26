using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Magic8Ball : MonoBehaviour
{
    private string[] goodAnswers = {
        "Yes",
        "Outlook good",
        "Most certainly",
        "For sure",
        "Definitely"
    };

     private string[] badAnswers = {
        "No",
        "Outlook bad",
        "Less likely",
        "Not a chance",
        "Nope"
    };

    private Vector3 originalPosition;

    public TMP_Text answerText;
    private bool debounce = true;

    public Slider optimismBar;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && debounce == true) {
            debounce = false;
            StartCoroutine(Shake(1f, 0.1f));
        }
    }

    string GetRandomAnswer() {
        
        float optimism = optimismBar.value;
        float roll = Random.Range(0f,1f);

        if (roll <= optimism) { // positive
            int index = Random.Range(0, goodAnswers.Length);
            return goodAnswers[index];
        }
        else { // negative
            int index = Random.Range(0, badAnswers.Length);
            return badAnswers[index];
        }
    }

    private IEnumerator Shake(float duration, float magnitude) {
        originalPosition = transform.localPosition;
        float elasped = 0f;

        while (elasped < duration) {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elasped += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;

        string randomAnswer = GetRandomAnswer();
        answerText.text = randomAnswer;
        debounce = true;
    }
}
