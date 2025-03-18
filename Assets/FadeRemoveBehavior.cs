using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
    
{
    [SerializeField]
    public float fadetime = 0.5f;
    public float fadeDelay = 0f;
    private float fadeDelayElapse = 0f;
    private float timeElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;

        //Vector3 scale = objToRemove.transform.localScale;
        //scale.x = Mathf.Abs(scale.x); // Force the x-scale to be positive
        //objToRemove.transform.localScale = scale;

    }

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (fadeDelay > fadeDelayElapse)
        {
            fadeDelayElapse += Time.deltaTime;

        }
        else
        {
            
            timeElapsed += Time.deltaTime;


        float newAlpha = startColor.a * (1 - (timeElapsed / fadetime));

        spriteRenderer.color = new Color(startColor.r, startColor.b, startColor.b, newAlpha);

            

        if (timeElapsed> fadetime)
        {
            Destroy(objToRemove);
        }

        }


            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
