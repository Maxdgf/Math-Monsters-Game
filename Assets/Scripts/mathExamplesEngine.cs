using System.Collections;
using System.Text;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mathExamplesEngine : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI exampleView, answerView;

    [SerializeField]
    GameObject tabletScreen, playerShootArea, player, playerCamera;

    [SerializeField]
    Material incorrectScreenMaterial, correctScreenMaterial, defaultScreenMaterial;

    [SerializeField]
    Animator exampleAnimator, tabletAnimator;

    [SerializeField]
    AudioSource soundEffectsSource;

    [SerializeField]
    AudioClip correctAnswerSound, incorrectAnswerSound;

    [SerializeField]
    Image btnApply;

    [SerializeField]
    string btnApplyColor;

    [SerializeField]
    float blinkDelay;

    [SerializeField]
    string exampleShowingAnimation, incorrectAnswerAnimation, correctAnswerAnimation;

    [SerializeField]
    ParticleSystem shootToMonsterSystem;

    [SerializeField]
    private allSpawnedMonsters monstersStorage;

    private string currentDifficulity;
    private string answerValue;
    private int exampleResult;
    private int exampleLength;
    private MeshRenderer tabletScreenRenderer;

    void Start()
    {
        tabletScreenRenderer = tabletScreen.GetComponent<MeshRenderer>();
        currentDifficulity = PlayerPrefs.GetString("current_dificulity");
        exampleLength = setExampleLengthFromDifficulity(currentDifficulity);

        shootToMonsterSystem.Stop();
        GenerateMathExample(currentDifficulity, exampleView);
        exampleAnimator.Play(exampleShowingAnimation, -1, 0f);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && keyCode != KeyCode.Backspace && keyCode != KeyCode.Return)
                {
                    char character = (char)keyCode;

                    if (char.IsDigit(character) || character == '-')
                    {
                        answerValue += character;
                        Debug.Log("Key presssed: " + character);
                        Debug.Log("Answer value: " + answerValue);
                        answerView.text = "Answer: " + answerValue;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            btnApply.color = Color.green;
            StartCoroutine(BtnBlink(blinkDelay, btnApply));

            CheckAnswer(answerValue, exampleResult);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (answerValue.Length > 0)
            {
                answerValue = answerValue.Substring(0, answerValue.Length - 1);
                answerView.text = "Answer: " + answerValue;
            }
        }
    }

    private void GenerateMathExample(string gameMode, TextMeshProUGUI outputView)
    {
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        char[] chars = { '+', '-', '/', '*' };

        if (exampleLength != 0 || exampleLength != 1)
        {
            StringBuilder example = new StringBuilder();

            if (example.Length == 0)
            {
                for (int i = 0; i < exampleLength; i++)
                {
                    char sym = ' ';

                    if (gameMode == "Very easy" || gameMode == "Easy")
                    {
                        sym = (char)Random.Range(0, chars.Length - 3);

                        int num = Random.Range(0, nums.Length);
                        example.Append(nums[num].ToString());
                    } 
                    else if (gameMode == "Normal")
                    {
                        sym = (char)Random.Range(0, chars.Length - 3);

                        int numSize = Random.Range(1, 3);
                        StringBuilder resultNum = new StringBuilder();

                        for (int j = 0; j < numSize; j++)
                        {
                            int num = Random.Range(1, 5);
                            resultNum.Append(nums[num].ToString());
                        }

                        example.Append(resultNum.ToString());
                    } 
                    else if (gameMode == "Hard")
                    {
                        sym = (char)Random.Range(0, chars.Length - 1);

                        int numSize = Random.Range(1, 3);
                        StringBuilder resultNum = new StringBuilder();

                        for (int j = 0; j < numSize; j++)
                        {
                            int num = Random.Range(1, 7);
                            resultNum.Append(nums[num].ToString());
                        }

                        example.Append(resultNum.ToString());
                    } 
                    else if (gameMode == "Extreme")
                    {
                        sym = (char)Random.Range(0, chars.Length);

                        int numSize = Random.Range(1, 4);
                        StringBuilder resultNum = new StringBuilder();

                        for (int j = 0; j < numSize; j++)
                        {
                            int num = Random.Range(1, nums.Length);
                            resultNum.Append(nums[num].ToString());
                        }

                        example.Append(resultNum.ToString());
                    }

                    if (i < exampleLength - 1)
                    {
                        example.Append(chars[sym]);
                    }
                }

                exampleAnimator.Play(exampleShowingAnimation, -1, 0f);

                double result = Eval(example.ToString());
                exampleResult = (int)result;
                Debug.Log($"Answer to current example: {result}(double), {exampleResult}(int)");

                string formattedExample = $"{ChangeExampleFormat(example.ToString())} = ?";

                outputView.text = formattedExample;
            }
            else
            {
                Debug.LogError("Error example generation!");
                outputView.text = "ERROR :(";
            }
        }
    }

    private void CheckAnswer(string input, int answer)
    {
        if (input != "" && input != null)
        {
            double userAnswer = double.Parse(input);

            if (userAnswer == answer)
            {
                soundEffectsSource.clip = correctAnswerSound;

                shootToMonsterSystem.Play();
                tabletAnimator.Play(correctAnswerAnimation, -1, 0f);
                soundEffectsSource.Play();

                playerShootArea.SetActive(true);
                GameObject monster = monstersStorage.getMonsterFromIndex(0);
                changeShootAreaWidthToMonster(playerShootArea, player, monster, 0);

                Debug.Log("correct !)");
                answerView.text = "";
                answerView.text = "Correct !)";
                answerValue = "";
                StartCoroutine(AnswerViewSymbolBlink(blinkDelay, answerView, "Answer: ?"));
                GenerateMathExample(currentDifficulity, exampleView);

                tabletScreenRenderer.material = correctScreenMaterial;
                StartCoroutine(ScreenBlink(blinkDelay, tabletScreenRenderer, defaultScreenMaterial));
                StartCoroutine(SetObjActive(blinkDelay, playerShootArea, false));
            }
            else
            {
                soundEffectsSource.clip = incorrectAnswerSound;

                tabletAnimator.Play(incorrectAnswerAnimation, -1, 0f);
                soundEffectsSource.Play();

                Debug.LogWarning("incorrect :(");
                answerView.text = "";
                answerView.text = "Incorrect :(";
                answerValue = "";
                StartCoroutine(AnswerViewSymbolBlink(blinkDelay, answerView, "Answer: ?"));
                GenerateMathExample(currentDifficulity, exampleView);

                tabletScreenRenderer.material = incorrectScreenMaterial;
                StartCoroutine(ScreenBlink(blinkDelay, tabletScreenRenderer, defaultScreenMaterial));
            }
        }
    }

    private static double Eval(string expression)
    {
        Debug.Log($"Math example engine: generated example: {expression}");

        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("expression", string.Empty.GetType(), expression.ToString());
        System.Data.DataRow row = table.NewRow();
        table.Rows.Add(row);

        return double.Parse((string)row["expression"]);
    }

    private string ChangeExampleFormat(string example)
    {
        char[] charsArray = example.ToCharArray();

        for (int i = 0; i < charsArray.Length; i++)
        {
            switch (charsArray[i])
            {
                case '/':
                    charsArray[i] = '÷';
                    break;

                case '*':
                    charsArray[i] = '×';
                    break;
            }
        }

        string output = new string(charsArray);

        return output;
    }

    private int setExampleLengthFromDifficulity(string difficulity)
    {
        int length = 0;

        switch (difficulity)
        {
            case "Extreme":
                length = 6;
                break;

            case "Hard":
                length = 5;
                break;

            case "Normal":
                length = 4;
                break;

            case "Easy":
                length = 3;
                break;

            case "Very easy":
                length = 2;
                break;
        }

        return length;
    }

    private void changeShootAreaWidthToMonster(GameObject shootArea, GameObject player, GameObject monster, int monsterIndex)
    {
        BoxCollider collider = shootArea.GetComponent<BoxCollider>();

        Vector3 a = player.transform.position;
        Vector3 b = monster.transform.position;

        float distance = Vector3.Distance(a, b);
        float deltaX = (distance - collider.size.x) / 2f;

        Vector3 newWith = collider.size;
        newWith.x = distance;
        collider.size = newWith;

        Vector3 newCenter = collider.center;
        newCenter.x += deltaX; 
        collider.center = newCenter;

        monstersStorage.removeMonsterWithIndexInStorage(monsterIndex);
    }

    private IEnumerator BtnBlink(float time, Image img)
    {
        yield return new WaitForSeconds(time);
        if (ColorUtility.TryParseHtmlString(btnApplyColor, out Color color))
        {
            img.color = color;
        }
    }

    private IEnumerator ScreenBlink(float time, MeshRenderer screenRenderer, Material screenMaterial)
    {
        yield return new WaitForSeconds(time);
        screenRenderer.material = screenMaterial;
    }

    private IEnumerator AnswerViewSymbolBlink(float time, TextMeshProUGUI view, string defaultValue)
    {
        yield return new WaitForSeconds(time);
        view.text = defaultValue;
    }

    private IEnumerator SetObjActive(float time, GameObject obj, bool state)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(state);
    }
}
