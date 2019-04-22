using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchDir
{
    None,
    Top,
    Bottom
}

public class PlayerMove : MonoBehaviour {
    public float speed = 100f;
    private EnvGenerate env;

    public TouchDir isSlide = TouchDir.None;
    public TouchDir isJump = TouchDir.None;

    private Vector3 lastMouseDown = Vector3.zero;
    private int nowLandIndex = 1;
    private int targetLand = 1;
    private float moveHorizontal =0;
    public float moveSpeed = 3f;
    private float[] xOffset = new float[3] { -14, 0, 14 };

    private Transform prisoner;
    public float jumpHeight = 20f;
    public float jumpSpeed = 10f;
    private bool isUp = true;


    void Start () {
        env = GameObject.FindWithTag(Tags.gameController).GetComponent<EnvGenerate>();
        prisoner = GameObject.Find("Prisoner").transform;
    }

    void Update () {
        if (GameController.gameState == GameState.Playing)
        {
            Vector3 targetPositon = env.forest1.GetNextTargetPoint();
            targetPositon = new Vector3(targetPositon.x+xOffset [targetLand] , targetPositon.y, targetPositon.z)-transform .position;
            transform.position += targetPositon .normalized * speed * Time.deltaTime;
            MoveControl();
        }
	}

    private void MoveControl()
    {
        GetTouchDir();
        if (nowLandIndex !=targetLand )
        {
            float movelength = Mathf.Lerp(0, moveHorizontal, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + movelength, transform.position.y, transform.position.z);
            moveHorizontal -= movelength;
            if (Mathf.Abs(moveHorizontal) < 0.5f)
            {
                transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
                moveHorizontal = 0;
                nowLandIndex = targetLand;
            }
        }

        if (isJump ==TouchDir.Top )
        {
            float yMove = jumpSpeed * Time.deltaTime;
            if (isUp )
            {
                prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y + yMove, prisoner.position.z);
                jumpHeight  -= yMove;
                if (Mathf .Abs (jumpHeight)<0.5f)
                {
                    prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y + jumpHeight, prisoner.position.z);
                    isUp = false;
                    jumpHeight =0;
                }
            }
            else
            {
                prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y - yMove, prisoner.position.z);
                jumpHeight += yMove;
                if (Mathf.Abs(jumpHeight) > 19.5f)
                {
                    prisoner.position = new Vector3(prisoner.position.x, prisoner.position.y - 20f+jumpHeight, prisoner.position.z);
                    isUp = true;
                    isJump = TouchDir.None;
                    jumpHeight = 20f;
                }
            }
        }
    }

    private void GetTouchDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseDown = Input.mousePosition;
        }
        if (Input .GetMouseButtonUp(0))
        {
            Vector3 mouseUp = Input.mousePosition;
            Vector3 touchOffset = mouseUp - lastMouseDown;
            if (Mathf .Abs (touchOffset .x)>50|| Mathf.Abs(touchOffset.y) > 50)
            {
                if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x > 0)
                {
                    if (targetLand != 2)
                    {
                        ++targetLand;
                        moveHorizontal = 14f;
                    }
                }
                else if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x < 0)
                {
                    if (targetLand != 0)
                    {
                        --targetLand;
                        moveHorizontal = -14f;
                    }
                }
                else if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y > 0)
                    isJump = TouchDir.Top;
                else if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y < 0)
                    StartCoroutine(SlideTime());
            }
        }
    }

    private IEnumerator SlideTime()
    {
        isSlide = TouchDir.Bottom;
        yield return new WaitForSeconds(1.4f);
        isSlide = TouchDir.None;
    }
}
