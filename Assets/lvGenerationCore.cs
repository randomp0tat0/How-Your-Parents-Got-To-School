using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newlevel : MonoBehaviour
{
    public GameObject brick; // brick
    public GameObject end; // end

    // generate level logic
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player GameObject not found.");
            return;
        }

        Vector2 pos = player.transform.position;

        // be below the player
        Vector2 newpos = new Vector2(pos.x, pos.y - 0.2f);

        addaBlock(newpos, 1.0f, 1.0f); // spawn brick

        float x = 0.0f;
        float y = 0.0f;

        float sizex = 0.7f; // block sizes
        float sizey = 1.0f;

        float ssizex = 0.25f; // block sizes
        float ssizey = 0.4f;

        float minx = 4.0f; // block locations
        float maxx = 8.0f;

        float miny = 1.5f; // block locations
        float maxy = 3.0f;

        // number of steps, averages around 3 blocks to the right per step
        for(int i = 0; i < 8; ++i){
            float xdist = Random.Range(minx, maxx);
            float ydist = Random.Range(miny, maxy);

            // go right
            if(Random.Range(0.0f, 1.0f) >= 0.5f){
                x += xdist;
                addaBlock(new Vector2(x, y), Random.Range(ssizex, sizex), Random.Range(ssizey, sizey));
            }

            // go down
            if(Random.Range(0.0f, 1.0f) >= 0.5f){
                x += xdist;
                y -= ydist;
                addaBlock(new Vector2(x, y), Random.Range(ssizex, sizex), Random.Range(ssizey, sizey));
            }
            else {
                // go up
                x += xdist;
                y += ydist;
                addaBlock(new Vector2(x, y), Random.Range(ssizex, sizex), Random.Range(ssizey, sizey));
            }

            // slanted jump
            if(Random.Range(0.0f, 1.0f) >= 0.5f){
                x += xdist;
                GameObject newbrick = Instantiate(brick); // make a slanted brick
                newbrick.transform.localScale = new Vector2(sizex, sizey * .33f);
                newbrick.tag = "Ground";
                newbrick.transform.position = new Vector2(x, y);
                setBodyRotation(newbrick, Random.Range(0.0f, 1.0f) >= 0.5f ? -45.0f : 45.0f); // set rotation of random brick to 45* left/right

                x += xdist * 0.5f;
                y += Random.Range(miny, maxy * 0.75f) * 3.0f; // go up

                // block to land on
                addaBlock(new Vector2(x, y), Random.Range(ssizex, sizex), Random.Range(ssizey, sizey));
            }

            // wallboost
            else {
                x += xdist;
                // create the wall
                float wallheight = sizey * Random.Range(2.5f, 7.5f);
                GameObject wall = Instantiate(brick);
                wall.transform.localScale = new Vector2(sizex * 0.33f, wallheight);
                wall.tag = "Ground";
                wall.transform.position = new Vector2(x, y);

                x += xdist * 0.5f;
                y += wallheight * 3.0f;

                // make a brick above to land on
                addaBlock(new Vector2(x, y), Random.Range(ssizex, sizex), Random.Range(ssizey, sizey));
            }
        }
        // add end
        addanEnd(new Vector2(x + 1.5f, y + 2.0f));
    }

    // add a new block
    private void addaBlock(Vector2 pos, float sizex, float sizey){
        GameObject newbrick = Instantiate(brick); // make new brick
        newbrick.transform.localScale = new Vector2(sizex, sizey * .33f);
        newbrick.tag = "Ground"; // can jump on this
        newbrick.transform.position = pos; // set position of brick
    }

    // add exit to level
    private void addanEnd(Vector2 pos){
        GameObject newend = Instantiate(end);
        newend.tag = "Ground"; // can jump on this
        newend.transform.position = pos;
    }

    // set rotation of object
    private void setBodyRotation(GameObject body, float angle){
        body.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}