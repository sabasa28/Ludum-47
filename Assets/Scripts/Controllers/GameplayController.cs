using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] Clickable[] clickableObjects = null;
    [SerializeField] Draggable stepSemillaCompleter = null;
    List <StepObject[]> stepsObjects = new List<StepObject[]>();
    [SerializeField] StepObject[] step1Objs = null; 
    [SerializeField] StepObject[] step2Objs = null; 
    [SerializeField] StepObject[] step3Objs = null; 
    [SerializeField] StepObject[] step4Objs = null; 
    [SerializeField] StepObject[] step5Objs = null; 
    [SerializeField] StepObject[] step6Objs = null; 
    public enum CurrentStep
    { 
        semilla,
        lluvia,
        sol,
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
        stepsObjects.Add(step2Objs);
        stepsObjects.Add(step3Objs);
        stepsObjects.Add(step4Objs);
        stepsObjects.Add(step5Objs);
        stepsObjects.Add(step6Objs);
        SunMoonController smController = FindObjectOfType<SunMoonController>();
        smController.OnStateChange = OnDayStart;
        stepSemillaCompleter.Reaction = OnStepDone;
        CloudManager.OnCloudCompleted = OnStepDone;
        clickableObjects = FindObjectsOfType<Clickable>();
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
        for (int i = 0; i < clickableObjects.Length; i++)
        {
            if (clickableObjects[i].gameObject.activeInHierarchy)
            {
                if ((clickableObjects[i].interactableDuring == Clickable.InteractableDuring.day) == day)
                    clickableObjects[i].OnStartCorrectState();
                else
                    clickableObjects[i].OnEndCorrectState();
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