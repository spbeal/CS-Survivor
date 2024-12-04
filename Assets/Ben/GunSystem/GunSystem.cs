using UnityEditorInternal;
using UnityEngine;

// needs shot effect from unity particle pack

public class GunSystem : MonoBehaviour
{
    // --- Gun stats
    // the time between each firing of the gun; the time it takes to fire the gun again after already firing it
    [SerializeField]
    private float timeBetweenShooting = 1.0f, 
    // the amount of spread for each bullet the gun should have
    spread = 0.1f, 
    // the range that the bullets can reach (anything after this distance can't be hit)
    range = 100.0f, 
    // the time it takes to fire another bullet in the same shoot cycle (we clicked fire once, but there was more than one bullet per tap)
    timeBetweenShots = 1.0f;
    // the amount of bullets to be fired every time the gun shoots
    public int bulletsPerTap = 1;
    // whether we should allow the user to hold down the firing button and automatically fire every time it is available to fire
    [SerializeField]
    private bool allowButtonHold = true;
    // a variable to store how many bullets we have left to shoot for each shooting cycle
    private int bulletsShot;
    // how many bullets are left in the gun
    public int bulletsLeft;

    // whether we are currently shooting, ready to shoot, and/or reloading
    private bool shooting, readyToShoot, reloading;

    // --- Reference
    // the camera to use for where the bullets are being fired
    [SerializeField]
    private Camera fpsCam;
    // the place to show the muzzle flash at (the end of the barrel of the gun)
    [SerializeField]
    private Transform attackPoint;
    // the object to use to detect whether an enemy is hit by the bullet fired
    [SerializeField]
    private RaycastHit rayHit;
    // the layer that the enemies are on so that they can be damaged
    [SerializeField]
    private LayerMask whatIsEnemy;
    // the object that contains all of the player data so that we can update the damage, magazine size, and reload speed
    [SerializeField]
    private PlayerStats playerStats;

    // --- Graphics
    // the muzzle flash to show when the gun fires
    [SerializeField]
    private GameObject muzzleFlash;

    // --- Dynamic binding objects setup
    // dynamic
    private gunNotFiring checkGunFireObjectSub = new gunFiring();
    // static
    private gunNotFiring checkGunFireObjectSuper = new gunNotFiring();
    // whether the dynamic binding check should be enabled or not
    [SerializeField]
    private bool checkForGunFiringOnOrOff = false;

    // create singelton instance to reference the class
    public static GunSystem instance;

    void Awake()
    {
        instance = this;
        bulletsLeft = playerStats.GetMagSize();
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();

        // Demonstration of Dynamic binding using the objects defined above
        if( shooting && checkForGunFiringOnOrOff ){
            // calls the override version of the printGunFire function in the subclass
            checkGunFireObjectSub.printGunFire();
        }else if( checkForGunFiringOnOrOff ){
            // calls the virtual version of the printGunFire function in the superclass
            checkGunFireObjectSuper.printGunFire();
        }
    }

    // function for checking input (could've been in Update but it is in a function instead. It makes Update look a little better)
    private void MyInput()
    {
        // checks whether you can hold down the shoot button and uses GetKey for checking for holding the button down and uses GetKeyDown for if it isn't allowed
        if (allowButtonHold) 
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        } 
        else 
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        // checks for whether reloading has been requested and if we can currently
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < playerStats.GetMagSize() && !reloading)
        {
            Reload();
        }

        // checks to see if we should shoot the gun
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    
    // fires the gun
    public void Shoot()
    {
        // now that we are going to shoot, make sure we don't shoot again until the gun is ready to fire again (delay between shots)
        readyToShoot = false;

        // create spread for the bullets fired from the gun
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // calculate the direction to fire the bullet with the spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // if the bullet hit an enemy, damage it
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.gameObject.GetComponent<EnemyBehavior>().TakeDamage( playerStats.GetDamage() );
            }
        }

        // create the muzzle flash for the gun firing
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        // reduce the amount of bullets left in the magazine by one
        bulletsLeft--;
        // reduce the amount of bullets to fire in this shoot cycle by one
        bulletsShot--;
        
        // make us able to fire again after the specified time between shooting
        Invoke("ResetShot", timeBetweenShooting);

        // if we have another bullet to fire and there's still more bullets in the magazine, then shoot again
        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    // allow firing again
    private void ResetShot()
    {
        readyToShoot = true;
    }

    // start reloading the gun
    public void Reload()
    {
        reloading = true;
        Invoke( "ReloadFinished", playerStats.GetReloadSpeed() );
    }

    // finish reloading the gun after the reloadTime timer is up
    private void ReloadFinished()
    {
        bulletsLeft = playerStats.GetMagSize();
        reloading = false;
    }
}

// --- Dynamic binding classes
// subclass
public class gunFiring : gunNotFiring {
    public override void printGunFire(){
        Debug.Log("Gun is firing");
    }
}

// superclass
public class gunNotFiring {
    public virtual void printGunFire(){
        Debug.Log("Gun is not currently firing");
    }
}