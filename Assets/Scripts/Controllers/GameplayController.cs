using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static List<Clickable> clickableObjects;
    [SerializeField] Draggable stepSemillaCompleter = null;
    List <StepObject[]> stepsObjects = new List<StepObject[]>();
    [SerializeField] StepObject[] step1Objs = null;
    [SerializeField] StepObject[] step2and3Objs = null;
    [SerializeField] StepObject[] step4Objs = null;
    [SerializeField] StepObject[] step5Objs = null;
    [SerializeField] StepObject[] step6Objs = null;
    public enum CurrentStep
    { 
        semilla,
        lluviaYSol,
        temperatura,
        bichitos,
        viento,
        stepsCompletados
    }
    public CurrentStep currentStep;
    bool stateChanged = false;
    
    private void Awake()
    {
        stepsObjects.Add(step1Objs);
        stepsObjects.Add(step2and3Objs);
        stepsObjects.Add(step4Objs);
        stepsObjects.Add(step5Objs);
        stepsObjects.Add(step6Objs);
        SunMoonController smController = FindObjectOfType<SunMoonController>();
        smController.OnStateChange = OnDayStart;
        stepSemillaCompleter.Reaction = OnStepDone;
        CloudController.OnStepsCompleted = OnStepDone;
        clickableObjects = FindObjectsOfType<Clickable>().ToList();
    }

    private void Start()
    {
        currentStep = CurrentStep.semilla;
    }

    void OnStepDone()
    {
        stateChanged = false;
        StartCoroutine(ChangeStep());   
    }

    IEnumerator ChangeStep()
    {
        if (currentStep != CurrentStep.stepsCompletados)
        {
            currentStep++;

            //script de planta
            DeactivateLastStateObjs();
        }
        yield return new WaitUntil(()=>stateChanged);
        if (currentStep != CurrentStep.stepsCompletados)
            ActivateCurrentStateObjs();
    }

    public void OnDayStart(bool day)
    {
        for (int i = 0; i < clickableObjects.Count; i++)
        {
            if (clickableObjects[i].gameObject.activeInHierarchy)
            {
                if ((clickableObjects[i].interactableDuring == Clickable.InteractableDuring.day) == day)
                    clickableObjects[i].StartCorrectState();
                else
                    clickableObjects[i].EndCorrectState();
            }
        }
        stateChanged = true;
    }

    void DeactivateLastStateObjs()
    { 
        for (int i = 0; i < stepsObjects[(int)currentStep - 1].Length; i++)
        {
            stepsObjects[(int)currentStep - 1][i].Deactivate();
        }       
    }

    void ActivateCurrentStateObjs()
    { 
        for (int i = 0; i < stepsObjects[(int)currentStep].Length; i++)
        {
            stepsObjects[(int)currentStep][i].gameObject.SetActive(true);
            stepsObjects[(int)currentStep][i].Appear();
        }    
    }
}