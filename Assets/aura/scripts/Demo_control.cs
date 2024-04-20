using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_control : MonoBehaviour
{
    public Material[] materials;

    public Renderer renderer;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

   

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.index++;
            if (this.index >= this.materials.Length)
                this.index = 0;

            this.renderer.material = this.materials[this.index];
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.index--;
            if (this.index <=-1)
                this.index = (this.materials.Length-1);

            this.renderer.material = this.materials[this.index];
        }

    }
}
