using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce=5f;
    public float rotationSpeed=10f;
    public GameObject gun, bulletPrefab;
    private Rigidbody _rigid;
    public static int SCORE=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid= GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet= Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Bullet balasScrit= bullet.GetComponent<Bullet>();
            balasScrit.targetVector=transform.right;
        }
    }

    // Update is called once per frame
 private void FixedUpdate()
    {
        
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        float thrust = Input.GetAxis("Vertical") * thrustForce;

        Vector3 thrustDirection = transform.right;

        transform.Rotate(Vector3.forward, -rotation);
        _rigid.AddForce(thrust * thrustDirection);

        Vector3 leftLimit=Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
        Vector3 rightLimit=Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
        Vector3 topLimit=Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
        Vector3 bottomLimit=Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        var position=transform.position;
        if(transform.position.x<leftLimit.x)
        {
            position =new Vector3(rightLimit.x,transform.position.y,transform.position.z);
        } 
        else if(transform.position.x>rightLimit.x)
        {
            position =new Vector3(leftLimit.x,transform.position.y,transform.position.z);
        } else if(transform.position.y>topLimit.y)
        {
            position=new Vector3(transform.position.x,bottomLimit.y,transform.position.z);
        } 
        else if(transform.position.y<bottomLimit.y)
        {
            position=new Vector3(transform.position.x,topLimit.y,transform.position.z);
        }
            transform.position=position;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            SCORE=0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
