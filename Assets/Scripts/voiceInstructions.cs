using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;

public class voiceInstructions : MonoBehaviour
{
    //recognition of sentences or specific words
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private NavMeshAgent _agent;

    void Start()
    {
        actions.Add("Taxi holding", goWaiting);
        actions.Add("Line up runway 0 1 and wait", lineUp);
        actions.Add("Runway 0 1, cleared for take-off", takeOff);
        actions.Add("Right turn approved", rightTurnA);
        _agent = this.GetComponent<NavMeshAgent>();

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

        if (_agent == null)
        {
            Debug.LogError("The Nav Mesh Agent is not attached to " + gameObject.name);
        }
        else
        {
            keywordRecognizer.Start();
        }
        //keywordRecognizer.Stop();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void goWaiting()
    {

        Vector3 waypoint = transform.TransformVector(-32, 14, 82);
        _agent.SetDestination(waypoint);

    }

    private void lineUp()
    {
        Vector3 waypoint = transform.TransformVector(48, 16, 40);
        NextControlPoint(waypoint);
    }

    //clearance for take off
    private void takeOff()
    {
        List<Vector3> listWP = new List<Vector3>();
        Vector3 waypoint1 = transform.TransformVector(36, 166, 687);
        Vector3 waypoint2 = transform.TransformVector(48, 16, 2835);
        listWP.Add(waypoint1);
        listWP.Add(waypoint2);
        
        foreach (Vector3 wp in listWP)
        {
             NextControlPoint(wp);
        }
        
    }

    //autorization to turn right after take off
    private void rightTurnA()
    {
        List<Vector3> listWP = new List<Vector3>();
        Vector3 waypoint1 = transform.TransformVector(35, 541, 4791);
        Vector3 waypoint2 = transform.TransformVector(2022, 1097, 6898);
        listWP.Add(waypoint1);
        listWP.Add(waypoint2);

        foreach (Vector3 wp in listWP)
        {
            NextControlPoint(wp);
            transform.Translate(Vector3.forward * _agent.speed * Time.deltaTime);
        }
    }

    public void NextControlPoint(Vector3 targetVector)
    {
        _agent.SetDestination(targetVector);
    }
}
