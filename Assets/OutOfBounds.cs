using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private Vector3 startPoint;
    private GameObject Player;
    private Rigidbody2D playerrb;

    float lx;
    float hx;
    float ly;
    float hy;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found! Please ensure there is a GameObject with the 'Player' tag.");
            return;
        }

        playerrb = Player.GetComponent<Rigidbody2D>();
        if (playerrb == null)
        {
            Debug.LogError("Rigidbody2D component not found on Player!");
            return;
        }

        startPoint = Player.transform.position;

        lx = float.MaxValue;
        hx = float.MinValue;
        ly = float.MaxValue;
        hy = float.MinValue;

        CalculateBoundaries();
    }

    void CalculateBoundaries()
    {
        GameObject[] objectsInScene = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in objectsInScene)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer == null) continue;

            Vector3 pos = obj.transform.position;
            Vector3 size = renderer.bounds.size;

            float objMinX = pos.x - size.x / 2 - 0.4f;
            float objMaxX = pos.x + size.x / 2 - 0.4f;
            float objMinY = pos.y - size.y / 2 - 0.4f;
            float objMaxY = pos.y + size.y / 2 - 0.4f;

            if (objMinX < lx) lx = objMinX;
            if (objMaxX > hx) hx = objMaxX;
            if (objMinY < ly) ly = objMinY;
            if (objMaxY > hy) hy = objMaxY;
        }

        ly *= 1.05f; // make it so you can go under some stuff without dying
    }

    void Update()
    {
        // check if player is out of bounds, if so: reset to spawn
        if (Player == null) return;

        Vector3 playerpos = Player.transform.position;

        if (playerpos.x < lx) // left wall
        {
            playerrb.velocity = new Vector2(0, playerrb.velocity.y);
            playerpos.x = lx;
            Player.transform.position = playerpos;
        }

        if (playerpos.x > hx) // right wall
        {
            playerrb.velocity = new Vector2(0, playerrb.velocity.y);
            playerpos.x = hx;
            Player.transform.position = playerpos;
        }

        if (playerpos.y < ly) // deathbox
        {
            resetPlayer();
        }

        if (playerpos.y > hy) // top wall
        {
            playerrb.velocity = new Vector2(playerrb.velocity.x, 0);
            playerpos.y = hy;
            Player.transform.position = playerpos;
        }
    }

    private void resetPlayer()
    {
        Player.transform.position = startPoint;
        playerrb.velocity = Vector2.zero; // Reset velocity to zero
    }
}
