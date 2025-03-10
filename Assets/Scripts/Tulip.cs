using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tulip : MonoBehaviour
{
    public TulipState state = TulipState.Kyuukon;
    [SerializeField]
    GameObject kyuukonSprite;
    [SerializeField]
    GameObject tulipFlowerObject;
    [SerializeField]
    GameObject tulipFlowerSprite;
    [SerializeField]
    Collider2D collider;
    float lifeTime = 0;
    public static float tulipTime = 5f;
    public static float tulipTimeMag = 1.5f;
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        state = TulipState.Kyuukon;
        //collider.enabled = false;
        kyuukonSprite.SetActive(true);
        tulipFlowerObject.SetActive(false);

        // 花の色を変更
        tulipFlowerSprite.GetComponent<SpriteRenderer>().material.SetFloat("_Hue", Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TulipState.Kyuukon:
                lifeTime += Time.deltaTime;
                if (lifeTime > tulipTime)
                {
                    state = TulipState.Tulip;
                    collider.enabled = true;
                    kyuukonSprite.SetActive(false);
                    tulipFlowerObject.SetActive(true);
                    // チューリップリストに追加
                    GameManager.instance.tulipList.Add(this);
                }
                break;
        }
    }
}

public enum TulipState
{
    Kyuukon,
    Tulip
}
