using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private GameObject enemy1 = null;
    [SerializeField]
    private GameObject enemy2 = null;
    [SerializeField]
    private GameObject enemy3 = null;

    private UIManager uIManager = null;
    private PlayerMove playerMove = null;
    public Vector2 minPosition { get; private set; }

    public Vector2 maxPosition { get; private set; }
    public PoolManager poolManager { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        minPosition = new Vector2(-7f, -12F);
        maxPosition = new Vector2(7f, 12F);
        poolManager = FindObjectOfType<PoolManager>();
        playerMove = FindObjectOfType<PlayerMove>();
        StartCoroutine(SummonOne());
        StartCoroutine(SummonTwo());
        StartCoroutine(SummonThree());
    }

    private IEnumerator SummonOne()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 4f));
        while (true)
        {
            
            for (int i=0; i<uIManager.stage+2; i++)
            {
                    float rand = Random.Range(-7f, 7f);
                    GameObject enemy = null;
                    enemy = Instantiate(enemy1, new Vector2(rand, 17), Quaternion.identity);
                    enemy.transform.SetParent(null);
                    yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));             
            }
            yield return new WaitForSeconds(Random.Range(2.5f, 4f));
        }
    }
    private IEnumerator SummonTwo()
    {
        yield return new WaitForSeconds(Random.Range(4f, 6f));
        while (true)
        {
            float rand = Random.Range(-4f, 4f);
            for (int i = 0; i <uIManager.stage*0.5f; i++)
            {
                    GameObject enemy = null;
                    enemy = Instantiate(enemy2, new Vector2(rand - (i * 2), 17), Quaternion.identity);
                    enemy.transform.SetParent(null);
                    enemy = Instantiate(enemy2, new Vector2(rand + (i * 2), 17), Quaternion.identity);
                    enemy.transform.SetParent(null);
                    yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(Random.Range(5f, 8f));
        }
    }
    private IEnumerator SummonThree()
    {
        yield return new WaitForSeconds(Random.Range(4f, 6f));
        while (true)
        {
            float rand = Random.Range(-6f, 6f);
            for (int i = 0; i < 1; i++)
            {
                    GameObject enemy = null;
                    enemy = Instantiate(enemy3, new Vector2(rand, 17), Quaternion.identity);
                    enemy.transform.SetParent(null);
                    yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(Random.Range(5f-(uIManager.stage*0.3f), 10f-(uIManager.stage*0.7f)));
        }
    }
}
