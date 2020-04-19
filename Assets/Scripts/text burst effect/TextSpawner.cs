using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public GameObject RandomFlyingText;
    public int numberToSpawn;
    public Transform spawnBox;
    public GameObject Canvas;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.2f);
        Gizmos.DrawCube(spawnBox.position, spawnBox.localScale);
    }

    /// <summary>
    /// spawns n random caracters and throws them accross the screen
    /// </summary>
    /// <param name="n"></param>
    public void spawnTextWave(int n)
    {
        int i = 0;
        float x = spawnBox.position.x - spawnBox.localScale.x/2, y = spawnBox.position.y - spawnBox.localScale.y / 2;
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        while(i < n / 4)
        {
            Instantiate(RandomFlyingText, new Vector2(x, Random.Range(y, -y)), rot).transform.SetParent(Canvas.transform);
            i++;
        }
        while (i < n / 2)
        {
            Instantiate(RandomFlyingText, new Vector2(-x, Random.Range(y, -y)), rot).transform.SetParent(Canvas.transform);
            i++;
        }
        while (i <  3 * n / 4)
        {
            Instantiate(RandomFlyingText, new Vector2(Random.Range(x, -x), y), rot).transform.SetParent(Canvas.transform);
            i++;
        }
        while (i < n)
        {
            Instantiate(RandomFlyingText, new Vector2(Random.Range(x, -x), -y), rot).transform.SetParent(Canvas.transform);
            i++;
        }
    }
}
