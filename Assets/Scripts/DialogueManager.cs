using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Speaker { Goshzilla, Moffy };
public enum SpeakerEmotion { Happy, Angry, Love, Dead };

public class DialogueManager : MonoBehaviour
{
    float cooldown = 0.5f;

    public static DialogueManager dialogueManager;

    public GameObject messageStuff, responseStuff;


    public float messageSpeed = 0.1f;
    public Sprite goshHappy, goshDead, goshLove, goshDoh, moffyAngry, moffyDead, moffyLove, moffyNormal;

    public Node currentNode;

    public Text speakerText, messageText, choiceText;
    public Image portrait;

    private string currentMessageText = "", targetMessageText = "";

    private Speaker currentSpeaker = Speaker.Goshzilla;

    private int choiceIndex = 0, lastChoice = 0, choiceLength = 0;

    private bool messageMode = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = this;
        show();
        nextMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0) cooldown -= Time.deltaTime;
        if (messageMode && !Input.GetKeyDown("m") && Input.anyKeyDown)
        {
            if(currentMessageText == targetMessageText)
            {
                GameManager.gameManager.PlaySound("pop",0.8f);
                nextMessage();
            }else
            {
                currentMessageText = targetMessageText;
                messageText.text = currentMessageText;
            }
        }
        else if(!messageMode)
        {
            int deltaChoice = (int)(Input.GetAxis("Vertical"));
            if(deltaChoice != lastChoice)
            {
                if(deltaChoice != 0) GameManager.gameManager.PlaySound("pop");
                lastChoice = deltaChoice;
                print(deltaChoice);
                if(deltaChoice == 1f) choiceIndex--;
                else if(deltaChoice == -1f) choiceIndex++;

                choiceIndex = Mathf.Max(0, choiceIndex);
                choiceIndex = Mathf.Min(choiceLength-1,choiceIndex);

                UpdateChoiceText();
            }
            else if(Input.GetButtonDown("Fire1") && cooldown <= 0)
            {
                GameManager.gameManager.PlaySound("pop",0.8f);
                Response response = (Response)currentNode;
                Choice[] choices = response.choices;
                GameManager.gameManager.attraction += choices[choiceIndex].attractionBonus;
                currentNode = choices[choiceIndex].next;
                nextMessage();
            }
        }
    }

    public void SetNode(Node node)
    {
        currentNode = node;
        show();
        nextMessage();
    }

    public void nextMessage()
    {
        if(currentNode == null)
        {
                hide();
                return;
        }
        if(NextIsMessage())
        {
            messageMode = true;
            messageStuff.SetActive(true);
            responseStuff.SetActive(false);

            GetComponent<Animator>().SetTrigger("Popup");

            currentMessageText = "";
            
            messageText.text = currentMessageText;

            Message message = (Message)currentNode;
            currentNode = currentNode.next;

            speakerText.text = message.speaker == Speaker.Goshzilla ? "GOSHZILLA" : "MOFFY";

            targetMessageText = message.text;

            //set portrait
            portrait.sprite = GetImage(message.speaker, message.emotion);

            currentSpeaker = message.speaker;

            //play emotion sound
            GameManager.gameManager.PlaySound( GetSound(message.speaker, message.emotion) );

            StartCoroutine(nextCharacter());
        }
        else if(NextIsBranch()) 
        {
            Branch branch = (Branch)currentNode;
            int attraction = GameManager.gameManager.attraction;

            if(attraction <= -1)
                currentNode = branch.enemies;
            else if(attraction > -1 && attraction <= 1)
                currentNode = branch.friends;
            else
                currentNode = branch.lovers;
            
            nextMessage();
        }
        else
        { //If next message is asking you to respond
            cooldown = 0.5f;
            messageMode = false;
            messageStuff.SetActive(false);
            responseStuff.SetActive(true);
            choiceIndex = 0;
            Response response = (Response)currentNode;
            choiceLength = response.choices.Length;
            UpdateChoiceText();
        }
        
    }

    void UpdateChoiceText()
    {
        Response response = (Response)currentNode;
        Choice[] choices = response.choices;
        string finalText = "";
        int index = 0;
        foreach(Choice choice in choices)
        {
            if(choiceIndex == index) finalText += "<color=black><b>- ";
            finalText += choice.text;
            if(choiceIndex == index) finalText += "</b></color> -";
            index++;
            if(index < choiceLength) finalText += "\n";
        }

        choiceText.text = finalText;
    }

    IEnumerator nextCharacter()
    {
        if(currentMessageText == targetMessageText) yield break ;
        if(Random.Range(0,100) > 50)
            GameManager.gameManager.PlaySound("talkingSound" + Random.Range(0,4), 1f + Random.Range(-.2f,.2f));
        currentMessageText += targetMessageText[currentMessageText.Length];
        messageText.text = currentMessageText;
        if(currentMessageText != targetMessageText)
        {
            yield return new WaitForSeconds(messageSpeed);
            StartCoroutine(nextCharacter());
        }
    }

    public void hide()
    {
        gameObject.SetActive(false);
        GameManager.gameManager.inUI = false;
        if(GameManager.gameManager.todoLevel == 4) GameManager.gameManager.EndGame();
    }

    public void show()
    {
        GameManager.gameManager.player.obj.SetActive(false);
        gameObject.SetActive(true);
        GameManager.gameManager.inUI = true;
    }


    public Sprite GetImage(Speaker speaker, SpeakerEmotion emotion)
    {
        if(speaker == Speaker.Goshzilla)
        {
            if(emotion == SpeakerEmotion.Happy) return goshHappy;
            if(emotion == SpeakerEmotion.Angry) return goshDoh;
            if(emotion == SpeakerEmotion.Love) return goshLove;
            if(emotion == SpeakerEmotion.Dead) return goshDead;
        }
        else 
        if(speaker == Speaker.Moffy)
        {
            if(emotion == SpeakerEmotion.Happy) return moffyNormal;
            if(emotion == SpeakerEmotion.Angry) return moffyAngry;
            if(emotion == SpeakerEmotion.Love) return moffyLove;
            if(emotion == SpeakerEmotion.Dead) return moffyDead;
        }

        print("invalid image enum");
        return null;
    }

    public string GetSound(Speaker speaker, SpeakerEmotion emotion)
    {
        if(speaker == Speaker.Goshzilla)
        {
            if(emotion == SpeakerEmotion.Happy) return "goshHappy";
            if(emotion == SpeakerEmotion.Angry) return "goshAngry";
            if(emotion == SpeakerEmotion.Love) return "goshLove";
            if(emotion == SpeakerEmotion.Dead) return "goshDead";
        }
        else 
        if(speaker == Speaker.Moffy)
        {
            if(emotion == SpeakerEmotion.Happy) return "moffyHappy";
            if(emotion == SpeakerEmotion.Angry) return "moffyAngry";
            if(emotion == SpeakerEmotion.Love) return "moffyLove";
            if(emotion == SpeakerEmotion.Dead) return "moffyDead";
        }

        print("invalid sound enum");
        return "";
    }

    bool NextIsMessage()
    {
        return currentNode.GetType() == typeof(Message);
    }

    bool NextIsBranch()
    {
        return currentNode.GetType() == typeof(Branch);
    }


}
