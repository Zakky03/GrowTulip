using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    Tulip tulipPrefab;
    [SerializeField]
    Soujiki soujikiPrefab;
    [SerializeField]
    Drone dronePrefab;
    int plantTulipCount = 0;
    int getTulipCount = 0;
    public int kyuukonCount = 0;
    int totalKyuukonCount = 0;
    public List<Tulip> tulipList = new List<Tulip>();
    public List<Soujiki> soujikiList = new List<Soujiki>();
    public List<Drone> droneList = new List<Drone>();
    int landPrice = 5;
    float landMag = 1.5f;
    int soujikiPrice = 10;
    float soujikiMag = 1.5f;
    int dronePrice = 20;
    float droneMag = 1.5f;
    int sSpeedPrice = 30;
    float sSpeedMag = 1.5f;
    int dSpeedPrice = 40;
    float dSpeedMag = 1.5f;
    int tSpeedPrice = 50;
    float tSpeedMag = 1.5f;
    [SerializeField]
    Transform walls;
    [SerializeField]
    TMP_Text landText;
    [SerializeField]
    TMP_Text soujikiText;
    [SerializeField]
    TMP_Text droneText;
    [SerializeField]
    TMP_Text sSpeedText;
    [SerializeField]
    TMP_Text dSpeedText;
    [SerializeField]
    TMP_Text tSpeedText;
    [SerializeField]
    GameObject landButton;
    [SerializeField]
    GameObject soujikiButton;
    [SerializeField]
    GameObject droneButton;
    [SerializeField]
    GameObject sSpeedButton;
    [SerializeField]
    GameObject dSpeedButton;
    [SerializeField]
    GameObject tSpeedButton;
    void Awake()
    {
        instance = this;
        // �͂��߂ɉ��������Ă���
        //kyuukonCount = 1;
        totalKyuukonCount = kyuukonCount;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {

            //���C���J������̃}�E�X�J�[�\���̂���ʒu����Ray���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //���C���[�}�X�N�쐬
            //int layerMask = LayerMaskNo.DEFAULT;

            //Ray�̒���
            float maxDistance = 20;

            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
            if (hit)
            {
                // �`���[���b�v�����n����
                if (hit.collider.tag == "Tulip")
                {
                    var tulip = hit.collider.GetComponent<Tulip>();
                    GetTulip(tulip);
                }
            }
            else
            {
                PlantKyuukon(mousePos);
            }
        }

        // �{�^���\��
        if (!landButton.activeSelf && kyuukonCount > landPrice)
        {
            landButton.SetActive(true);
            landText.text = $"�y�n({landPrice}T)";
        }
        if (!soujikiButton.activeSelf && kyuukonCount > soujikiPrice)
        {
            soujikiButton.SetActive(true);
            soujikiText.text = $"�������n�@({soujikiPrice}T)";
        }
        if (!droneButton.activeSelf && kyuukonCount > dronePrice)
        {
            droneButton.SetActive(true);
            droneText.text = $"������܂��@({dronePrice}T)";
        }
        if (!sSpeedButton.activeSelf && kyuukonCount > sSpeedPrice)
        {
            sSpeedButton.SetActive(true);
            sSpeedText.text = $"���n�@�X�s�[�h�A�b�v({sSpeedPrice}T)";
        }
        if (!dSpeedButton.activeSelf && kyuukonCount > dSpeedPrice)
        {
            dSpeedButton.SetActive(true);
            dSpeedText.text = $"��܂��@�X�s�[�h�A�b�v({dSpeedPrice}T)";
        }
        if (!tSpeedButton.activeSelf && kyuukonCount > tSpeedPrice)
        {
            tSpeedButton.SetActive(true);
            tSpeedText.text = $"�`���[���b�v�X�s�[�h�A�b�v({tSpeedPrice}T)";
        }

        KoitanDebug.Display($"�����̏����� = {kyuukonCount}\n");
        KoitanDebug.Display($"�������l���� = {totalKyuukonCount}\n");
        KoitanDebug.Display($"���Ԃ����苅���l���� = {(totalKyuukonCount / Time.time):F2}\n");
        //KoitanDebug.Display($"�A�����`���[���b�v�̖{�� = {plantTulipCount}\n");
        //KoitanDebug.Display($"���n�����`���[���b�v�̖{�� = {getTulipCount}\n");
        KoitanDebug.Display($"�^�C�� : {(int)Time.time}s\n");
    }

    public void GetTulip(Tulip tulip)
    {
        if (tulip.state == TulipState.Tulip)
        {
            tulipList.Remove(tulip);
            Destroy(tulip.gameObject);
            getTulipCount++;
            // �Ƃ肠����2���₷
            kyuukonCount += 2;
            totalKyuukonCount += 2;
        }
    }

    /// <summary>
    /// �y�n�𔃂�
    /// </summary>
    public void BuyLand()
    {
        if (kyuukonCount > landPrice)
        {
            kyuukonCount -= landPrice;
            Camera.main.orthographicSize += 1;
            walls.localScale = Vector3.one * Camera.main.orthographicSize;
            landPrice = Mathf.FloorToInt(landPrice * landMag);
            landText.text = $"�y�n({landPrice}T)";
        }
    }

    /// <summary>
    /// �������n�@�𔃂�
    /// </summary>
    public void BuySoujiki()
    {
        if (kyuukonCount > soujikiPrice)
        {
            kyuukonCount -= soujikiPrice;
            soujikiPrice = Mathf.FloorToInt(soujikiPrice * soujikiMag);
            soujikiText.text = $"�������n�@({soujikiPrice}T)";
            var soujiki = Instantiate(soujikiPrefab);
            soujikiList.Add(soujiki);
        }
    }

    public void BuyDrone()
    {
        if (kyuukonCount > dronePrice)
        {
            kyuukonCount -= dronePrice;
            dronePrice = Mathf.FloorToInt(dronePrice * droneMag);
            droneText.text = $"������܂��@({dronePrice}T)";
            var drone = Instantiate(dronePrefab);
            droneList.Add(drone);
        }
    }

    public void BuySpeedUpSoujiki()
    {
        /*
        foreach (Soujiki soujiki in soujikiList)
        {
            soujiki.speed *= 2;
        }
        */
        if (kyuukonCount > sSpeedPrice)
        {
            kyuukonCount -= sSpeedPrice;
            sSpeedPrice = Mathf.FloorToInt(sSpeedPrice * sSpeedMag);
            sSpeedText.text = $"���n�@�X�s�[�h�A�b�v({sSpeedPrice}T)";
            Soujiki.speed *= Soujiki.speedMag;
        }
    }

    public void BuySpeedUpDrone()
    {
        if (kyuukonCount > dSpeedPrice)
        {
            kyuukonCount -= dSpeedPrice;
            dSpeedPrice = Mathf.FloorToInt(dSpeedPrice * dSpeedMag);
            dSpeedText.text = $"��܂��@�X�s�[�h�A�b�v({dSpeedPrice}T)";
            Drone.speed *= Drone.speedMag;
            Drone.interval /= Drone.speedMag;
        }
    }

    public void BuySpeedUpTulip()
    {
        if (kyuukonCount > tSpeedPrice)
        {
            kyuukonCount -= tSpeedPrice;
            tSpeedPrice = Mathf.FloorToInt(tSpeedPrice * tSpeedMag);
            tSpeedText.text = $"�`���[���b�v�X�s�[�h�A�b�v({tSpeedPrice}T)";
            Tulip.tulipTime /= Tulip.tulipTimeMag;
        }
    }

    public bool EnablePlant(Vector3 pos)
    {
        if (kyuukonCount > 0)
        {
            // �A�����邩���ׂ�
            RaycastHit2D circleHit = Physics2D.CircleCast((Vector2)pos, 0.6f, Vector2.zero, 10);
            if (!circleHit)
            {
                return true;
            }
        }
        return false;
    }

    void ForcePlantKyuukon(Vector3 pos)
    {
        if (kyuukonCount > 0)
        {
            // �`���[���b�v��A����
            var tulip = Instantiate(tulipPrefab);
            tulip.transform.position = pos;
            plantTulipCount++;
            kyuukonCount--;
        }
    }

    public bool PlantKyuukon(Vector3 pos)
    {
        // �A�����邩���ׂ�
        if (EnablePlant(pos))
        {
            ForcePlantKyuukon(pos);
        }
        return false;
    }
}
