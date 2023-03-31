using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript : MonoBehaviour
{
    public Bullet bulletprefab;
    public float reload;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0 && time <= reload && GameManager.instance.currentGameState == GameState.PlayMode)
        {

            Bullet temp = Instantiate(bulletprefab, new Vector3(this.transform.position.x + transform.forward.x, this.transform.position.y + transform.forward.y), this.transform.rotation);
            temp.transform.position = this.transform.position + this.transform.forward * 0.4f * Mathf.Sign(this.transform.localScale.x);
            time = 0f;
            temp.setDirection(transform.forward);

        }
    }
}
