using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator animator;

    void Awake()
        {
            animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoorOpening();
        //DoorClosing();
    }


    private void DoorOpening()
    {
        animator.SetBool("DoorOpen", true);
        animator.SetBool("DoorClose", false);
    }

    //private void DoorClosing()
    //{
    //    animator.SetBool("DoorOpen", false);
    //    animator.SetBool("DoorClose", true);
    //}


    //    private void OnTriggerEnter(Collider other)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            animator.SetBool("DoorOpen", true);
    //        }
    //    }

    //private void OnTriggerExit(Collider other)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            animator.SetBool("DoorOpen", ;
    //            animator.SetBool("DoorClose", true);
    //        }
    //    }

}
