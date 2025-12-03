using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AlienVentScene : MonoBehaviour
{
    [SerializeField] private GameObject babyAlien;
    [SerializeField] private Transform ventLid;

    private Vector3 babyStartPoint = new Vector3(-21.9759998f, 0.547999978f, 4.31899977f);
    private Vector3 alienTargetPoint = new Vector3(-21.9759998f, 0.547999978f, 2.86299992f);

    private float lidOpenAngle = 90f;
    private float lidAnimationSpeed = 2f;
    private float alienCrawlSpeed = 1.5f;

    private Quaternion lidClosedRotation;
    private Quaternion lidOpenRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lidClosedRotation = ventLid.rotation;
        lidOpenRotation = lidClosedRotation * Quaternion.Euler(lidOpenAngle, 0, 0);
    }

    public IEnumerator AlienEnterVentSequence()
    {
        babyAlien.transform.localRotation = Quaternion.Euler(0, 180, 0);
        babyAlien.transform.position = babyStartPoint;
        yield return new WaitForSeconds(0.5f);
        
        yield return StartCoroutine(OpenVent());
        
        yield return StartCoroutine(CrawlToTarget(alienTargetPoint, alienCrawlSpeed));
        
        yield return new WaitForSeconds(0.3f);
        
        yield return StartCoroutine(CloseVent());

        babyAlien.transform.localRotation = Quaternion.Euler(0, 354.157715f, 0);
        babyAlien.transform.position = new Vector3(-22.5979996f, -0.31400001f, 3.41599989f);
        babyAlien.GetComponent<Interactable>().activated = false;
        babyAlien.GetComponent<Interactable>().obj = "Baby Alien 2";
    }
    
    private IEnumerator OpenVent()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * lidAnimationSpeed;
            ventLid.rotation = Quaternion.Lerp(lidClosedRotation, lidOpenRotation, t);
            yield return null;
        }
    }
    
    private IEnumerator CloseVent()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * lidAnimationSpeed;
            ventLid.rotation = Quaternion.Lerp(lidOpenRotation, lidClosedRotation, t);
            yield return null;
        }
    }

    private IEnumerator CrawlToTarget(Vector3 target, float speed)
    {
        babyAlien.GetComponent<Animator>().SetTrigger("scooting");
        while (Vector3.Distance(babyAlien.transform.position, target) > 0.1f)
        {
            babyAlien.transform.position = Vector3.MoveTowards(babyAlien.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
