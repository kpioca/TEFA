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
    [SerializeField] private float height_move;

    [SerializeField] private float speed_jump;
    [SerializeField] private float height_jump;

    [SerializeField] private Vector3 target;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Vector3[] camera_positions = new Vector3[3]
    {
        new Vector3(-2,4.38f,-7.64f),
        new Vector3(0,4.38f,-7.64f),
        new Vector3(2,4.38f,-7.64f)
    };

    private List<float> camera_positions_x = new List<float> { -2, 0, 2 };

    private int curr_camPos_num;
    private Coroutine movementCoroutine;
    private Coroutine jumpingCoroutine;

    private bool isMoving = false;
    private bool isJumping = false;
    private bool isTargetChanged = false;

    public void Start()
    {
        curr_camPos_num = 1;

        speed_move = gameManager.Speed_playerDash;
        height_move = gameManager.Height_playerDash;

        speed_jump = gameManager.Speed_playerJump;
        height_jump = gameManager.Height_playerJump;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        float camera_pos_x = mainCamera.transform.position.x;

        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (camera_positions_x.Contains(camera_pos_x) && !isMoving)
            {
                if (eventData.delta.x > 0 && isPossibleToTakeNext(curr_camPos_num, 1, camera_positions))
                {
                    if (!isJumping)
                        movementCoroutine = StartCoroutine(moveToNextPos_Coroutine(curr_camPos_num, 1, camera_positions, speed_move, height_move, mainCamera.gameObject));
                    else
                    {
                        isMoving = true;
                        target = new Vector3(camera_positions[curr_camPos_num + 1].x, target.y, camera_positions[curr_camPos_num + 1].z);
                        curr_camPos_num++;
                        isTargetChanged = true;
                    }
                }
                else if (eventData.delta.x < 0 && isPossibleToTakeNext(curr_camPos_num, -1, camera_positions))
                {
                    if (!isJumping)
                        movementCoroutine = StartCoroutine(moveToNextPos_Coroutine(curr_camPos_num, -1, camera_positions, speed_move, height_move, mainCamera.gameObject));
                    else
                    {
                        isMoving = true;
                        target = new Vector3(camera_positions[curr_camPos_num - 1].x, target.y, camera_positions[curr_camPos_num - 1].z);
                        curr_camPos_num--;
                        isTargetChanged = true;
                    }
                }
            }
        }
        else if (!isMoving && !isJumping)
        {
            if (eventData.delta.y > 0)
                jumpingCoroutine = StartCoroutine(jump_Coroutine(speed_jump, height_jump, mainCamera.gameObject));
        }
    }

    private IEnumerator moveToNextPos_Coroutine(int curr_num, int increment, Vector3[] positions, float speed_move, float height_move, GameObject obj)
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

    private IEnumerator jump_Coroutine(float speed_jump, float height_jump, GameObject obj)
    {
        Vector3 start_pos = obj.transform.position;

        float lowPoint = start_pos.y;
        float highPoint = start_pos.y + height_jump;

        float runningTime;
        float totalRunningTime;


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

        isMoving = false;

        runningTime = 0;
        totalRunningTime = Vector3.Distance(start_pos, target) / speed_jump;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;

            if (isMoving != true)
            {
                if (runningTime >= (Mathf.Floor(totalRunningTime / Time.deltaTime * 3 / 4) * Time.deltaTime)) //you can't move
                    isMoving = true;
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

        obj.transform.position = target;

        isJumping = false;
        isMoving = false;
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

    public void changePlayerParameters(float speedMove, float heightMove, float speedJump, float heightJump)
    {
        speed_move = speedMove;
        height_move = heightMove;
        speed_jump = speedJump;
        height_jump = heightJump;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
