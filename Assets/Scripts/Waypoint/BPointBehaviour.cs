using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPointBehaviour : MonoBehaviour
{
    public int originalX = 70;
    public int originalY = -70;
    private int eggCount = 0;

    void Start()
    {
        transform.position = new Vector3(originalX, originalY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Egg(Clone)")
        {
            float newOpacity = GetComponent<SpriteRenderer>().color.a * 0.75f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newOpacity);
            eggCount++;
            if (eggCount == 4){
                float newX = originalX + Random.Range(-15, 15);
                float newY = originalY + Random.Range(-15, 15);
                transform.position = new Vector3(newX, newY, 0);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                eggCount = 0;
            }
        }
        
    }

    public Vector3 getPosition(){
        return transform.position;
    }
}
