using UnityEngine;

// needs shot effect from unity particle pack

public class GunSystem : MonoBehaviour
{
    //Gun stats
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float timeBetweenShooting = 1.0f, spread = 0.1f, range = 100.0f, timeBetweenShots = 1.0f;
    public float reloadTime = 2.0f;
    public int magazineSize = 10, bulletsPerTap = 1;
    [SerializeField]
    private bool allowButtonHold = true;
    private int bulletsShot;
    public int bulletsLeft;

    //bools to check during the firing process
    private bool shooting, readyToShoot, reloading;

    //Reference
    [SerializeField]
    private Camera fpsCam;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private RaycastHit rayHit;
    [SerializeField]
    private LayerMask whatIsEnemy;

    //Graphics
    [SerializeField]
    private GameObject muzzleFlash;

    // Dynamic binding objects setup
    private gunNotFiring checkGunFireObjectSub = new gunFiring();
    private gunNotFiring checkGunFireObjectSuper = new gunNotFiring();

    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();

        // Demonstration of Dynamic binding using the objects defined above
        if(shooting){
            // calls the override version of the printGunFire function in the subclass
            checkGunFireObjectSub.printGunFire();
        }else{
            // calls the virtual version of the printGunFire function in the superclass
            checkGunFireObjectSuper.printGunFire();
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) 
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        } 
        else 
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
            }
        }

        //Graphics
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

// Dynamic binding classes
public class gunFiring : gunNotFiring {
    public override void printGunFire(){
        Debug.Log("Gun is firing");
    }
}

public class gunNotFiring {
    public virtual void printGunFire(){
        Debug.Log("Gun is not currently firing");
    }
}