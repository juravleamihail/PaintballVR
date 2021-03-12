using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PhotonView))]
public class Player : Photon.PunBehaviour
{
    public float speed;
    public GameObject stainPrefab;
    public GameObject spawnObject;
    public GameObject PaintPrefab;
    public GameObject MainCameraGO;

    private AudioSource audioSource;
    private float paintScale = 0.25f;
    private bool canShoot = true;

    private PhotonView photonView;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public bool UseTransformView = true;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;

    private void Awake()
    {
        Debug.Log(transform.position);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
        gameObject.name = "Player: " + photonView.owner.NickName;

        if (!photonView.isMine)
        {
            Destroy(GetComponent<Player>());
        }
    }

    private void FixedUpdate()
    {
        CheckInput();
    }

    //Update is called once per frame
    //void Update()
    //{
    //    if (photonView.isMine)
    //        CheckInput();
    //    else
    //        SmoothMove();
    //}

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (UseTransformView)
            return;

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void CheckInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            // DebugText.text = "Horizontal = " + h + " , Vertical = " + v;
            //Debug.Log("Horizontal = " + h + " , Vertical = " + v);
        }

        //if((h >= 0.5 || h <= -0.5) || (v >= 0.5 || v <= -0.5))
        //{
        //    DebugText.text = "H = " + h + " , V = " + v;
        //}

        transform.position += MainCameraGO.transform.right * (h * Time.deltaTime * speed);
        transform.position += MainCameraGO.transform.forward * (v * Time.deltaTime * speed);

        if ((Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Mouse0)) && canShoot)
        {
            Shoot();
            canShoot = false;
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            canShoot = true;
        }
    }

    private void SmoothMove()
    {
        if (UseTransformView)
            return;

        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
    }

    private void Shoot()
    {
        //by default the sound is "gun sound"
        audioSource.Play();

        GameObject bulletGO = Instantiate(stainPrefab, spawnObject.transform.position, spawnObject.transform.rotation);
        bulletGO.GetComponent<Rigidbody>().AddForce(spawnObject.transform.forward * 5000f);
        Destroy(bulletGO, 2);

        RaycastHit hit;

        if (Physics.Raycast(spawnObject.transform.position, spawnObject.transform.forward, out hit, Mathf.Infinity))
        {
            var paintSplatter = GameObject.Instantiate(PaintPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            var scaler = paintScale;
            paintSplatter.transform.localScale = new Vector3(
                    paintSplatter.transform.localScale.x * scaler,
                    paintSplatter.transform.localScale.y * scaler,
                    paintSplatter.transform.localScale.z
                );
            Destroy(paintSplatter.gameObject, 20);
        }
    }
}