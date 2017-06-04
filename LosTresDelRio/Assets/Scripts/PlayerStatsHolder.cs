using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatsHolder : MonoBehaviour {
    public BasePlayer playerStats;
    public GameObject player;
    public GameObject target;
    public GameObject HpLabel;
    public GameObject AmmoLabel;
    public GameObject TacoButton;
    public GameObject Skill1Button;
    public GameObject Skill2Button;
    public GameObject Skill3Button;
    public GameObject Skill4Button;
    public GameObject ReloadButton;
    public GameObject ShootButton;
    public GameObject Bullet;
    public GameObject BombaDelChiliObject;
    public GameObject GunShotLight;
    public GameObject SlashAndDiceEffectParti;
    public GameObject CactusPunchoEffect;
    public GameObject InfoText1;
    public GameObject InfoText2;
    public GameObject InfoText3;
    public GameObject Canvas1;
    public GameObject DeathCanvas;
    public GameObject DeathPanel;
    public GameObject DeathInfo1;
    public GameObject DeathInfo2;
    public GameObject WinPanel;
    public GameObject WinInfo1;
    public GameObject WinInfo2;
    public GameObject MainCamera;
    public GameObject DeathCamera;
    public GameObject ExitUI;
    public AudioSource ReloadSound;
    public AudioSource ShootSound;
    public AudioSource ShootCdSound;
    public AudioSource SliceAndDiceSound;
    public AudioSource CactusPunchoSound;

    public float CDreduction;
    public float DMGbonus;
    public int HPbonus;
    public int MortaGracia; 
    public int healthPoints;
    public int MaxHP;
    public float damage;
    public int ammo;
    public int maxammo;
    public int level;
    public int experience;
    public int gold;
    public int healingTaco;
    public int Score;
    public int killsCount;

    bool exitUIon;
    bool shootCD;
    bool reloadTime;
    bool shootOffCD;
    bool sliceAndDiceCD;
    bool bombaDelChiliCD;
    bool cactusPunchoActive;
    bool cactusPunchoCD;
    bool cactusInterCD;
    bool upgrade1skill;
    bool upgrade2skill;
    bool upgrade3skill;
    bool upgrade4skill;
    bool GameWon;



    // Use this for initialization
    void Start() {
        CDreduction = 0.0f;
        DMGbonus = 0.0f;
        healthPoints = 100;
        MaxHP = 100;
        damage = 10;
        ammo = 1;
        maxammo = 6;
        level = 1;
        experience = 0;
        gold = 10;
        healingTaco = 10;
        killsCount = 0;
        shootCD = false;
        reloadTime = false;
        GameWon = false;
        exitUIon = false;

    }


    // Update is called once per frame
    void Update() {

        if(experience >=100)
        {
            level = level + 1;
            experience = 0;
            damage = damage + 10;
            healthPoints = healthPoints + 50;
            MaxHP = MaxHP + 50;
        }

        InfoText1.GetComponent<Text>().text = "Level: " + level + "\n" + "Experience: " + experience + "/100";
        InfoText2.GetComponent<Text>().text = "Score: " + Score + "\n" + "Kill Count: " + killsCount + "\n" + "Pesos: " + gold;
        

        if (Input.GetKeyDown(KeyCode.B))
        {
            TacoUsage();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadTimer());

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SliceAndDice();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BombaDelChili();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            ShootOff();
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            CactusPuncho();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!exitUIon)
            {
                ExitUI.SetActive(true);
                exitUIon = true;
            }
            else
            {
                ExitUI.SetActive(false);
                exitUIon = false;
            }
           
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && shootCD == false && reloadTime == false && GameWon == false)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Count > 0)
        {
            GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().TargertsSwap();
        }
        HpLabel.GetComponentInChildren<Text>().text = healthPoints.ToString();
        AmmoLabel.GetComponentInChildren<Text>().text = ammo.ToString();
        
        if(cactusPunchoActive)
        {
            CactusPunchoEffect.GetComponent<ParticleSystem>().Play();
            if (GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Count > 0)
            {
                if (!cactusInterCD)
                {
                    for (int i = 0; i < GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Count; i++)
                    {
                        if (!cactusInterCD)
                        {
                            GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies[i].GetComponent<EnemyStats>().TakeDmg(5f);
                        }
                    }
                }
                cactusInterCD = true;
                StartCoroutine(CactusPunchoInterDmgTimer());
            }
        }
        else
        {
            CactusPunchoEffect.GetComponent<ParticleSystem>().Stop();
        }

        if (ammo <= 0)
        {
            StartCoroutine(ReloadTimer());
        }
        if (healthPoints <= 0)
        {
            Death();
        }

        if (healingTaco == 0)
        {
            TacoButton.GetComponent<Button>().interactable = false;
        }

        if (GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Count > 0)
        {
            target = GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().TargetForPlayer;
        }

    }
    IEnumerator ReloadTimer()
    {
        //ReloadButton.GetComponent<Button>().interactable = false;
        ReloadButton.GetComponentInChildren<Text>().text = "Reloading";
        ShootButton.GetComponentInChildren<Text>().text = "Cooldown";
        reloadTime = true;
        ReloadSound.Play();
        if(CDreduction>0)
        {
            yield return new WaitForSeconds(0.8f - CDreduction);
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
        }
        
        reloadTime = false;
        ShootButton.GetComponentInChildren<Text>().text = "Singel Shot";
        Reload();
    }

    IEnumerator GunShootFlash()
    {
        yield return new WaitForSeconds(0.2f);
        GunShotLight.SetActive(false);
    }
    IEnumerator ShootCD()
    {
        ShootButton.GetComponentInChildren<Text>().text = "Cooldown";
        shootCD = true;
        ShootCdSound.Play();
        yield return new WaitForSeconds(0.5f);
        ShootButton.GetComponentInChildren<Text>().text = "Singel Shot";
        shootCD = false;
    }

    IEnumerator ShootOffCD()
    {
        Skill3Button.GetComponentInChildren<Text>().text = "Cooldown";
        shootOffCD = true;
        
        yield return new WaitForSeconds(5f);
        Skill3Button.GetComponentInChildren<Text>().text = "Shoot off";
        shootOffCD = false;
    }

    IEnumerator SliceAndDiceCD()
    {
        Skill1Button.GetComponentInChildren<Text>().text = "Cooldown";
        sliceAndDiceCD = true;
        yield return new WaitForSeconds(5f);
        Skill1Button.GetComponentInChildren<Text>().text = "Slice and Dice";
        sliceAndDiceCD = false;
    }

    IEnumerator CactusPunchoCD()
    {
        if (cactusPunchoActive)
        {
            Skill4Button.GetComponentInChildren<Text>().text = "Active/Cooldown";
            cactusPunchoCD = true;
            yield return new WaitForSeconds(10f);
            
            cactusPunchoActive = false;
            Skill4Button.GetComponentInChildren<Text>().text = "Cooldown";
            yield return new WaitForSeconds(5f);
            Skill4Button.GetComponentInChildren<Text>().text = "Cactus Puncho";
            cactusPunchoCD = false;
        }
    }

    IEnumerator CactusPunchoInterDmgTimer()
    {
        yield return new WaitForSeconds(2f);
        cactusInterCD = false;
    }

    IEnumerator BombaDelChiliCD()
    {
        bombaDelChiliCD = true;
        Skill2Button.GetComponentInChildren<Text>().text = "Cooldown";
        yield return new WaitForSeconds(5f);
        Skill2Button.GetComponentInChildren<Text>().text = "Bomb del Chili";
        bombaDelChiliCD = false;
    }

    IEnumerator BulletFlight(Vector3 pos)
    {
        while (Bullet.transform.position != pos)
        {
            Bullet.transform.position = Vector3.MoveTowards(Bullet.transform.position, pos, 20 * Time.deltaTime);
        }
        if (Bullet.transform.position == pos)
        {
            yield return null;
        }
    }


    public void TacoUsage()
    {
        if(healingTaco >0)
        {
            healthPoints = healthPoints + 50;
            if(healthPoints >=MaxHP)
            {
                healthPoints = MaxHP;
            }
            healingTaco = healingTaco - 1;
            TacoButton.GetComponentInChildren<Text>().text = "Taco x" + healingTaco;
        }

    }
    

    public void Reload()
    {
        StopCoroutine(ReloadTimer());
        //ReloadButton.GetComponent<Button>().interactable = true;
        ReloadButton.GetComponentInChildren<Text>().text = "Reload";
            ammo = maxammo;

    }

    public void Shoot()
    {
        if (target != null && ammo > 0)
        {
            ShootSound.Play();
            GunShotLight.SetActive(true);
            StartCoroutine(GunShootFlash());
            Bullet = (GameObject)Instantiate(GameObject.Find("Bullet"),transform.position,transform.rotation);
            Vector3 moveUp = new Vector3(0, 3f, 0);
            Bullet.transform.position = GameObject.Find("Player").GetComponent<Transform>().position + moveUp;
            Bullet.name = "bullet1";
            StartCoroutine(BulletFlight(target.transform.position));
            target.GetComponent<EnemyStats>().TakeDmg(damage);
            ammo = ammo - 1;
            
            StartCoroutine(ShootCD());
        }
    }

    public void ShootOff()
    {
        if(!shootOffCD && ammo > 0 && GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Count > 0)
        {
            ammo = maxammo;
            for(int i = 0; i <= ammo; i++)
            {
                
                ShootSound.Play();
                GunShotLight.SetActive(true);
                StartCoroutine(GunShootFlash());
                Bullet = (GameObject)Instantiate(GameObject.Find("Bullet"), transform.position, transform.rotation);
                Vector3 moveUp = new Vector3(0, 3f, 0);
                Bullet.transform.position = GameObject.Find("Player").GetComponent<Transform>().position + moveUp;
                Bullet.name = "bullet1";
                int TargetCount = GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies.Count - 1;
                int randomTarget = UnityEngine.Random.Range(0, TargetCount);
                target = GameObject.Find("EnemyScanner").GetComponent<EnemyScanZone>().Enemies[randomTarget];
                StartCoroutine(BulletFlight(target.transform.position));
                target.GetComponent<EnemyStats>().TakeDmg(damage);
                ammo = ammo - 1;
                StartCoroutine(ShootOffCD());
            }

        }

    }
    public void SliceAndDice()
    {

        if(!sliceAndDiceCD)
        {
            SliceAndDiceSound.Play();
            if(GameObject.Find("SliceAndDiceHitBox").GetComponent<SliceAndDiceTrigger>().Enemies.Count > 0)
            {
                
                for(int i = 0; i < GameObject.Find("SliceAndDiceHitBox").GetComponent<SliceAndDiceTrigger>().Enemies.Count; i++)
                {
                    if (GameObject.Find("SliceAndDiceHitBox").GetComponent<SliceAndDiceTrigger>().Enemies[i] != null)
                    {
                        target = GameObject.Find("SliceAndDiceHitBox").GetComponent<SliceAndDiceTrigger>().Enemies[i];
                        target.GetComponent<EnemyStats>().TakeDmg(damage * 1.5f);
                    }
                    
                }
              
            }
            SlashAndDiceEffectParti.GetComponent<ParticleSystem>().Play();
            Vector3 PushDir = GameObject.Find("Player").GetComponent<Transform>().position;
           // PushDir = -PushDir.normalized;
            GameObject.Find("Player").GetComponent<Rigidbody>().AddForce((PushDir * 2f) * -1f);
            StartCoroutine(SliceAndDiceCD());
        }


    }

    public void CactusPuncho()
    {
        if(!cactusPunchoCD && !cactusPunchoActive)
        {
            cactusPunchoActive = true;
            StartCoroutine(CactusPunchoCD());

        }
        
    }

    public void BombaDelChili()
    {

        if(!bombaDelChiliCD && target != null )
        {
          
            BombaDelChiliObject = (GameObject)Instantiate(GameObject.Find("BombaDelCopia"), transform.position, transform.rotation);
            Vector3 moveUp = new Vector3(0, 3f, 0);
            BombaDelChiliObject.transform.position =target.GetComponent<Transform>().position + moveUp;
            BombaDelChiliObject.name = "bombaDelChili";
            StartCoroutine(BombaDelChiliCD());
        }
        
    }

    public void TakeDmg(float damageAmount)
    {
        healthPoints = (int)(healthPoints - damageAmount);
    }

    public void Death()
    {
        DeathCanvas.SetActive(true);
        DeathPanel.SetActive(true);
        Canvas1.SetActive(false);
        DeathInfo1.GetComponent<Text>().text = "Level: " + level;
        DeathInfo2.GetComponent<Text>().text = "Score: " + Score + "\n" + "Kill Count: " + killsCount + "\n" + "Pesos: " + gold;
        DeathCamera.SetActive(true);
        MainCamera.SetActive(false);
        Destroy(GameObject.Find(gameObject.name));
    }

    public void Win()
    {
        GameObject.Find("TalkToUI3").SetActive(false);
        GameWon = true;
        WinPanel.SetActive(true);
        WinInfo1.GetComponent<Text>().text = "Level: " + level;
        WinInfo2.GetComponent<Text>().text = "Score: " + Score + "\n" + "Kill Count: " + killsCount + "\n" + "Pesos: " + gold;

    }


}




