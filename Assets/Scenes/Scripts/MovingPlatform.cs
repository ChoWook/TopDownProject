using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    ///방향
    public enum eDirection
    {
        Left,
        Right,
        Up,
        Down,
        Diagonal
    }
    public eDirection setDirection;
    private Vector2 Direct(eDirection eDirection)
    {
        switch (eDirection)
        {
            case eDirection.Left:
                return Vector2.left;
            case eDirection.Right:
                return Vector2.right;
            case eDirection.Down:
                return Vector2.down;
            case eDirection.Up:
                return Vector2.up;
            default:
                return Vector2.zero;
        }
    }

    /// 스피드 조절
    [SerializeField] private float speed;
    float Speed
    {
        get { return this.speed * Time.deltaTime; }
    }

    ///거리
    [SerializeField] private float distance;
    public Vector2 myPos;
    public Vector2 frontPoint;
    public Vector2 backPoint;

    private void SetPos()
    {
        var dis = this.distance / 2;

        switch (this.setDirection)
        {
            case eDirection.Left:
                this.frontPoint = new Vector2(this.transform.localPosition.x - dis, this.transform.localPosition.y);
                this.backPoint = new Vector2(this.transform.localPosition.x + dis, this.transform.localPosition.y);
                break;
            case eDirection.Right:
                this.frontPoint = new Vector2(this.transform.localPosition.x + dis, this.transform.localPosition.y);
                this.backPoint = new Vector2(this.transform.localPosition.x - dis, this.transform.localPosition.y);
                break;
            case eDirection.Down:
                this.frontPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - dis);
                this.backPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + dis);
                break;
            case eDirection.Up:
                this.frontPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + dis);
                this.backPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - dis);
                break;
        }
    }

    ///대기시간
    [SerializeField] private float waitTime;

    /// 시작
    private void Start()
    {
        // 처음 시작 위치 저장
        this.myPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);

        //움직일 거리 조정
        this.SetPos();

        StartCoroutine(this.Move());
    }

    /// 움직임
    private IEnumerator Move()
    {
        while (true)
        {
            if (this.distance != 0)
            {     //1차 움직임
                while (true)
                {
                    this.transform.Translate(this.Direct(this.setDirection) * this.Speed);
                    var dis = Vector2.Distance(new Vector2(this.transform.localPosition.x, this.transform.localPosition.y), this.frontPoint);

                    if (dis <= 0.2f)
                        break;

                    yield return null;
                }

                yield return new WaitForSeconds(this.waitTime);

                //2차 움직임
                while (true)
                {
                    this.transform.Translate(-this.Direct(this.setDirection) * this.Speed);
                    var dis = Vector2.Distance(new Vector2(this.transform.localPosition.x, this.transform.localPosition.y), this.backPoint);

                    if (dis <= 0.2f)
                        break;

                    yield return null;
                }
                yield return new WaitForSeconds(this.waitTime);
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Reset"))
        {
            this.transform.localPosition = this.myPos;
            this.SetPos();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(frontPoint, backPoint);
    }

    //따로 움직이는 버그수정
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


}