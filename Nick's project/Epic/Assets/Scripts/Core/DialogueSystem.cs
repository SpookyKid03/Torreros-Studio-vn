using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public ELEMENTS elements;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Say something and show it on the speech box OVERWRITE
    public void Say(string speech, string speaker = "") 
    {
        StopSpeaking();

        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }
    // Say something and ADD it to the speech box ADDITIVE
    public void SayAdd(string speech, string speaker = "") 
    {
        StopSpeaking();

        speechText.text = targetSpeech;

        speaking = StartCoroutine(Speaking(speech, true, speaker));
    }

    public void StopSpeaking() 
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }


    public bool isSpeaking {get{return speaking != null;}}
    [HideInInspector] public bool isWaitingForUserInput = false;

    string targetSpeech = "";
    Coroutine speaking = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "") 
    {
        speechPanel.SetActive(true);
        targetSpeech = speech;

        if (!additive)
        {
            speechText.text = "";
        }
        else
        {
            targetSpeech = speechText.text + targetSpeech;
        }
        //speakerNameText.text = speaker; // temporary
        speakerNameText.text = DetermineSpeaker(speaker); // temporary
        isWaitingForUserInput = false;

        while (speechText.text != targetSpeech)
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        // Text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
        {
            yield return new WaitForEndOfFrame();
        }
        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = speakerNameText.text; //default return is the current name
        if (s != speakerNameText.text && s != "")
        {
            retVal = (s.ToLower().Contains("narrator")) ? "" : s;
        }
        return retVal;
    }

    [System.Serializable]
    public class ELEMENTS 
    {
        // The main panel containing all diaglogue related elements on the UI
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
    }
    public GameObject speechPanel {get {return elements.speechPanel;}}
    public Text speakerNameText {get {return elements.speakerNameText;}}
    public Text speechText {get {return elements.speechText;}}

}
