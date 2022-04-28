using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTINGSCRIPT : MonoBehaviour
{
    DialogueSystem dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogueSystem.instance;
    }

    public string[] s = new string[] //This is all the testing dialogue for now
    {
        "Hey Peter, it's me Joe Swanson.:Joe",
        "This wheelchair is my life",
        "I wish I could walk on two legs~"
    };

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if (index >= s.Length)
                {
                    return;
                }

                Say(s[index]);
                index++;
            }
        }
    }

    void Say(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];  // If there is text after a : in a string, the text after the : will be the speaker's name
        string speaker = (parts.Length >= 2) ? parts[1] : ""; // Otherwise maintain previous set speakername

        if (s.Contains("~")) {   // Add a ~ to the end of the line if you want the speech to be additive
            char mychar = '~';
            dialogue.SayAdd(speech.Trim(mychar), speaker);
        }
        else { // Otherwise the dialogue will overwrite
            dialogue.Say(speech, speaker);
        }
    }
}
