using UnityEngine;

public class AlienVentScene : MonoBehaviour
{
    [SerializeField] private GameObject babyAlien;
    [SerializeField] private Transform ventLid;

    private Vector3 babyStartPoint = new Vector3(-18.917f, 0.091f, 4.992f);
    private Vector3 alienTargetPoint = new Vector3(-18.917f, 0.091f, 2.999f);

    private float lidOpenAngle = 90f;
    private float lidAnimationSpeed = 2f;
    private float alienCrawlSpeed = 1.5f;

    private bool isAnimating = false;
    private Quaternion lidClosedRotation;
    private Quaternion lidOpenRotation;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lidClosedRotation = ventLid.rotation;
        lidOpenRotation = lidClosedRotation * Quaternion.Euler(lidOpenAngle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (babyAlien != null)
                    {
                        StartCoroutine(AlienEnterVentSequence());
                    }
                }
            }
        }
    }

    System.Collections.IEnumerator AlienEnterVentSequence()
    {
        isAnimating = true;
        
        babyAlien.transform.position = babyStartPoint;
        yield return new WaitForSeconds(0.5f);
        
        yield return StartCoroutine(OpenVent());
        
        yield return StartCoroutine(CrawlToTarget(alienTargetPoint, alienCrawlSpeed));
        
        babyAlien.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        
        yield return StartCoroutine(CloseVent());
        
        isAnimating = false;
    }
    
    System.Collections.IEnumerator OpenVent()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * lidAnimationSpeed;
            ventLid.rotation = Quaternion.Lerp(lidClosedRotation, lidOpenRotation, t);
            yield return null;
        }
    }
    
    System.Collections.IEnumerator CloseVent()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * lidAnimationSpeed;
            ventLid.rotation = Quaternion.Lerp(lidOpenRotation, lidClosedRotation, t);
            yield return null;
        }
    }

    System.Collections.IEnumerator CrawlToTarget(Vector3 target, float speed)
    {
        
        while (Vector3.Distance(babyAlien.transform.position, target) > 0.1f)
        {
            babyAlien.transform.position = Vector3.MoveTowards(babyAlien.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        
    }
}
