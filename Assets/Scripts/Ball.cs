using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{ 
    [SerializeField] private float amplitude;
    [SerializeField] private float step;

    private float startAmplitude;
    private AudioSource audioSource;
    private Rigidbody rb;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startAmplitude = amplitude;
    }

    // Update is called once per frame
    public void Restart()
    {
        amplitude = startAmplitude;
        rb.MovePosition(Vector3.zero);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * amplitude; // change to send to losing side
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "PaddleLeft" || collision.gameObject.name == "PaddleRight")
        {
            if(collision.gameObject.name == "PaddleLeft")
            {
                //play sound
                audioSource.PlayOneShot(clip1);
            }
            if (rb.velocity.magnitude > 10 && rb.velocity.magnitude < 12 && collision.gameObject.name == "PaddleLeft")
            {
                //play "finish him" clip
                audioSource.PlayOneShot(clip3);
            }

            if(collision.gameObject.name == "PaddleRight")
            {
                //play sound
                audioSource.PlayOneShot(clip2);
            }
            if (rb.velocity.magnitude > 10.5 && rb.velocity.magnitude < 14 && collision.gameObject.name == "PaddleRight")
            {
                //play "denied" clip
                audioSource.PlayOneShot(clip4);
            }


            amplitude += step;
            float offset = Mathf.Pow((transform.position.z - collision.transform.position.z), 2);
            offset = (transform.position.z - collision.transform.position.z < 0) ? offset * -1 : offset;

            rb.velocity = (collision.gameObject.name == "PaddleLeft")
                ? new Vector3(amplitude, 0, offset)
                : new Vector3(-amplitude, 0, offset);
           
        }
        

    }


}
