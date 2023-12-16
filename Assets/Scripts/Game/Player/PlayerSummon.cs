using System.Collections;
using UnityEngine;

public class PlayerSummon : MonoBehaviour
{
    [SerializeField] private GameObject spawnWarning;
    [SerializeField] private GameObject spawnCloneNPC;
    [SerializeField] private GameObject spawnFXExplosion;
    [SerializeField] private GameObject spawnNPC;
    [SerializeField] private float npcForce;
    [SerializeField] private float SummonCD;

    private float summonCDT = 0;
    private bool summonButton = false;
    [SerializeField] private GameObject npcHealthBar;
    [SerializeField] private CDTimeBar CDTBar;
    [SerializeField] private AudioSource Explosion;

    private void Start()
    {
        CDTBar.UpdateCDTimeBar(summonCDT, SummonCD);
    }

    void Update()
    {
        SummonNPC();
    }

    public void SummonButtonClicked()
    {
        summonButton = true;
    }

    void SummonNPC()
    {
        if ((Input.GetKeyDown(KeyCode.R) || summonButton) && summonCDT <= 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.5f, -0.5f, 0).normalized;
            Vector3 spawnWarningPosition = new Vector3(mousePos.x, mousePos.y, 0f);
            Instantiate(spawnWarning, spawnWarningPosition, Quaternion.identity);

            Vector3 spawnCloneNPCPosition = spawnWarningPosition + new Vector3(0f, 60f, 0f);
            GameObject npcFall = Instantiate(spawnCloneNPC, spawnCloneNPCPosition, Quaternion.identity);
            Rigidbody2D rb = npcFall.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * npcForce, ForceMode2D.Impulse);
            summonCDT = SummonCD;
            summonButton = false;

            StartCoroutine(SpawnExplosion(spawnWarningPosition));
        }

        summonCDT -= Time.deltaTime;
        CDTBar.UpdateCDTimeBar(summonCDT, SummonCD);
    }

    IEnumerator SpawnExplosion(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2f);

        Explosion.Play();
        Instantiate(spawnFXExplosion, spawnPosition, Quaternion.identity);
        Instantiate(spawnNPC, spawnPosition, Quaternion.identity);
        npcHealthBar.SetActive(true);
    }

    public void ReduceSummonCDT(float amount)
    {
        summonCDT -= amount;
        CDTBar.UpdateCDTimeBar(summonCDT, SummonCD);
    }
}
