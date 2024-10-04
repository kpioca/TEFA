using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;

    [Header("Parameters")]
    [SerializeField] private float speed_move;

    public float SpeedMove => speed_move;

    [SerializeField] private float speed_jump;

    public float SpeedJump => speed_jump;

    [SerializeField] private float height_jump;

    public float HeightJump => height_jump;

    [SerializeField] private float speed_fall;
    public float SpeedFall => speed_fall;

    [SerializeField] private Vector3 target;

    [SerializeField] private Camera mainCamera;

    private Vector3[] camera_positions = new Vector3[3]
    {
        new Vector3(-2,5.08f,-7.7f),
        new Vector3(0,5.08f,-7.7f),
        new Vector3(2,5.08f,-7.7f)
    };

    private List<float> camera_positions_x = new List<float> { -2, 0, 2 };

    public int curr_camPos_num = 1;
    private Coroutine movementCoroutine;
    private Coroutine jumpingCoroutine;
    private Coroutine fallCoroutine;

    public bool isMoving = false;
    public bool stopMove = false;
    public bool stopFall = false;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isTargetChanged = false;
    public bool isJumpingEffectActivated = false;

    public void Start()
    {
        curr_camPos_num = 1;

        speed_move = gameManager.Speed_playerDash;

        speed_jump = gameManager.Speed_playerJump;
        height_jump = gameManager.Height_playerJump;

        speed_fall = gameManager.SpeedFall;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        float camera_pos_x = mainCamera.transform.position.x;

        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            //camera_positions_x.Contains(camera_pos_x) && 
            if (!isMoving && !stopMove)
            {
                if (eventData.delta.x > 0 && isPossibleToTakeNext(curr_camPos_num, 1, camera_positions))
                {
                    if (!isJumping)
                    {
                        curr_camPos_num++;
                        target = camera_positions[curr_camPos_num];
                        movementCoroutine = StartCoroutine(moveToNextPos_Coroutine(mainCamera.gameObject));
                    }
                    else if (!isFalling)
                    {
                        //isMoving = true;
                        curr_camPos_num++;
                        target = new Vector3(camera_positions[curr_camPos_num].x, target.y, camera_positions[curr_camPos_num].z);
                        isTargetChanged = true;
                    }
                }
                else if (eventData.delta.x < 0 && isPossibleToTakeNext(curr_camPos_num, -1, camera_positions))
                {
                    if (!isJumping)
                    {
                        curr_camPos_num--;
                        target = camera_positions[curr_camPos_num];
                        movementCoroutine = StartCoroutine(moveToNextPos_Coroutine(mainCamera.gameObject));
                    }
                    else if (!isFalling)
                    {
                        //isMoving = true;
                        curr_camPos_num--;
                        target = new Vector3(camera_positions[curr_camPos_num].x, target.y, camera_positions[curr_camPos_num].z);
                        isTargetChanged = true;
                    }
                }
            }
            else if (isMoving)
            {
                if (eventData.delta.x > 0 && isPossibleToTakeNext(curr_camPos_num, 1, camera_positions))
                {
                    if (!isFalling)
                    {
                        //isMoving = true;
                        curr_camPos_num++;
                        target = camera_positions[curr_camPos_num];
                        isTargetChanged = true;
                    }
                }
                else if (eventData.delta.x < 0 && isPossibleToTakeNext(curr_camPos_num, -1, camera_positions))
                {
                    if (!isFalling)
                    {
                        //isMoving = true;
                        curr_camPos_num--;
                        target = camera_positions[curr_camPos_num];
                        isTargetChanged = true;
                    }
                }
            }
        }
        else if (isJumping && !stopFall)
        {
            if (eventData.delta.y < 0)
            {
                StopCoroutine(jumpingCoroutine);
                fallCoroutine = StartCoroutine(fall_Coroutine(speed_fall, mainCamera.gameObject));
            }

        }

        else if (!isMoving && !isJumping)
        {
            if (eventData.delta.y > 0)
                jumpingCoroutine = StartCoroutine(jump_Coroutine(speed_jump, speed_jump, height_jump, mainCamera.gameObject));
        }
    }

    public void activateJumpEffect(float speed_jump, float speed_fall, float height_jump)
    {
        if (!isJumpingEffectActivated)
        {
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (fallCoroutine != null)
                StopCoroutine(fallCoroutine);
            if (jumpingCoroutine != null)
                StopCoroutine(jumpingCoroutine);

            jumpingCoroutine = StartCoroutine(jumpEffect_Coroutine(speed_jump, speed_fall, height_jump, mainCamera.gameObject));
        }
    }
    private IEnumerator moveToNextPos_Coroutine2(int curr_num, int increment, Vector3[] positions, float speed_move, float height_move, GameObject obj)
    {
        Vector3 start_pos = obj.transform.position;
        Vector3 final_pos;
        Vector3 pos = obj.transform.position;
        Vector3 target_pos = positions[curr_num + increment];

        float lowPoint = target_pos.y;
        float highPoint = target_pos.y + height_move;

        float x_distance = Mathf.Abs(target_pos.x - start_pos.x);

        float runningTime;
        float totalRunningTime;

        isMoving = true;

        final_pos = new Vector3(target_pos.x - (increment * x_distance / 2), highPoint, target_pos.z);

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, final_pos) / speed_move;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, final_pos, runningTime / totalRunningTime);
            yield return 0;
        }

        obj.transform.position = final_pos;

        start_pos = final_pos;
        final_pos = new Vector3(target_pos.x, lowPoint, target_pos.z);

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, final_pos) / speed_move;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, final_pos, runningTime / totalRunningTime);
            yield return 0;
        }

        obj.transform.position = final_pos;

        isMoving = false;
        curr_camPos_num = curr_num + increment;
    }

    private IEnumerator moveToNextPos_Coroutine(GameObject obj)
    {
        Vector3 start_pos = obj.transform.position;
        Vector3 targetPos = target;

        double runningTime;
        double totalRunningTime;

        isMoving = true;

        Vector3 pos = start_pos;
        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, targetPos) / speed_move;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, targetPos, (float)(runningTime / totalRunningTime));
            pos = obj.transform.position;
            if (isTargetChanged)
            {
                isTargetChanged = false;
                movementCoroutine = StartCoroutine(moveToNextPos_Coroutine(obj));
                yield break;
            }
            yield return 0;
        }

        if (isTargetChanged)
        {
            isTargetChanged = false;
            movementCoroutine = StartCoroutine(moveToNextPos_Coroutine(obj));
            yield break;
        }

        obj.transform.position = targetPos;

        isMoving = false;
    }

    private IEnumerator fall_Coroutine(float speed_fall, GameObject obj)
    {
        Vector3 start_pos = obj.transform.position;
        Vector3 end_pos = camera_positions[curr_camPos_num];

        float runningTime;
        float totalRunningTime;


        target = new Vector3(end_pos.x, end_pos.y, end_pos.z);

        isFalling = true;

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, target) / speed_fall;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 0;
        }

        obj.transform.position = target;

        isJumping = false;
        isFalling = false;
        stopMove = false;
        isMoving = false;
    }



    private IEnumerator jumpEffect_Coroutine(float speed_jump, float speed_fall, float height_jump, GameObject obj)
    {
        Vector3 start_pos = obj.transform.position;

        float lowPoint = start_pos.y;
        float highPoint = start_pos.y + height_jump;

        List<float> distances = new List<float>() { Vector3.Distance(start_pos, camera_positions[0]), Vector3.Distance(start_pos, camera_positions[1]), Vector3.Distance(start_pos, camera_positions[2]) };
        float min_Dist = distances.Min();
        int k = distances.FindIndex(dist => dist == min_Dist);

        float runningTime;
        float totalRunningTime;

        stopFall = true;
        stopMove = false;
        isJumping = true;
        isMoving = false;
        isFalling = false;
        isJumpingEffectActivated = true;

        target = new Vector3(camera_positions[k].x, highPoint, camera_positions[k].z);

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, target) / speed_jump;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;

            if (stopMove != true)
            {
                if (runningTime >= (Mathf.Floor(totalRunningTime / Time.deltaTime * 3 / 4) * Time.deltaTime)) //you can't move
                    stopMove = true;
            }

            if (isTargetChanged)
            {
                start_pos = obj.transform.position;
                runningTime = 0;
                totalRunningTime = Vector3.Distance(start_pos, target) / speed_jump;
                isTargetChanged = false;
            }
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 0;
        }

        //obj.transform.position = target;

        start_pos = target;
        target = new Vector3(start_pos.x, lowPoint, start_pos.z);

        stopMove = false;

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, target) / speed_fall;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;


            if (stopMove != true)
            {
                if (runningTime >= (Mathf.Floor(totalRunningTime / Time.deltaTime * 3 / 4) * Time.deltaTime))
                {  //you can't move
                    stopMove = true;
                    isJumpingEffectActivated = false;
                }
            }


            if (isTargetChanged)
            {
                start_pos = obj.transform.position;
                runningTime = 0;
                totalRunningTime = Vector3.Distance(start_pos, target) / speed_fall;
                isTargetChanged = false;
            }


            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 0;
        }

        obj.transform.position = target;

        stopFall = false;
        stopMove = false;
        isJumping = false;
        isMoving = false;
        isFalling = false;
        isJumpingEffectActivated = false;
    }
    private IEnumerator jump_Coroutine(float speed_jump, float speed_fall, float height_jump, GameObject obj)
    {
        Vector3 start_pos = obj.transform.position;

        float lowPoint = start_pos.y;
        float highPoint = start_pos.y + height_jump;

        float runningTime;
        float totalRunningTime;

        stopMove = false;
        isJumping = true;

        target = new Vector3(start_pos.x, highPoint, start_pos.z);

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, target) / speed_jump;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;

            if (isTargetChanged)
            {
                start_pos = obj.transform.position;
                runningTime = 0;
                totalRunningTime = Vector3.Distance(start_pos, target) / speed_jump;
                isTargetChanged = false;
            }
            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 0;
        }

        //obj.transform.position = target;

        start_pos = target;
        target = new Vector3(start_pos.x, lowPoint, start_pos.z);


        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, target) / speed_fall;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;

            
            if (stopMove != true)
            {
                if (runningTime >= (Mathf.Floor(totalRunningTime / Time.deltaTime * 3 / 4) * Time.deltaTime)) //you can't move
                    stopMove = true;
            }
            

            if (isTargetChanged)
            {
                start_pos = obj.transform.position;
                runningTime = 0;
                totalRunningTime = Vector3.Distance(start_pos, target) / speed_fall;
                isTargetChanged = false;
            }


            obj.transform.position = Vector3.Lerp(start_pos, target, runningTime / totalRunningTime);
            yield return 0;
        }

        obj.transform.position = target;

        isJumping = false;
        isMoving = false;
        stopMove = false;
    }

    private bool isPossibleToTakeNext<T>(int curr_num, int increment, T[] array)
    {
        int n = array.Length;

        if ((curr_num == n - 1 && increment >= 0) || (curr_num == 0 && increment < 0))
        {
            return false;
        }
        else return true;
    }

    public void changePlayerParameters(float speedMove, float heightJump)
    {
        speed_move = speedMove;
        height_jump = heightJump;
    }

    public void changeSpeedMove(float speedMove)
    {
        speed_move = speedMove;
    }

    public void changeHeightJump(float heightJump)
    {
        height_jump = heightJump;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
