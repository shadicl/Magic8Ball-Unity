using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Magic8Ball : MonoBehaviour
{
    private string[] answers = {
        "Yes",
        "No",
        "Ask again later",
        "Outlook good",
        "Very doubtful"
    };

    private Vector3 originalPosition;

    public TMP_Text answerText;
    private bool debounce = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && debounce == true) {
            debounce = false;
            StartCoroutine(Shake(1f, 0.1f));
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

        int index = Random.Range(0, answers.Length);
        answerText.text = answers[index];
        debounce = true;
    }
}
