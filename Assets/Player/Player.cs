using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int XPos { get; set; }
    public int YPos { get; set; }

    private bool _move = false;
    private Transform _target;

    private readonly float _speed = 400f;

    private void Start()
    {
        PlayerStats.Health = 15;
        PlayerStats.Gold = 15;
        PlayerStats.Supplies = 2;
    }

    private void Update()
    {
        if (_move)
        {
            MovePlayer();
        }
    }

    public void CheckAvailableMovement(List<Card> cards)
    {
        int left = (XPos - 1);
        int right = (XPos + 1);
        int above = (YPos + 1);
        int below = (YPos - 1);

        foreach (Card card in cards)
        {
            if (card.XPos == left && card.YPos == YPos
                || card.XPos == right && card.YPos == YPos
                || card.YPos == above && card.XPos == XPos
                || card.YPos == below && card.XPos == XPos)

                card.Clickable = true;
        }
    }

    public void MoveToPosition(Transform target)
    {
        _target = target;
        _move = true;

        if (transform.position.x - target.position.x < 0)
        {
            XPos += 1;
        }
        else if (transform.position.x - target.position.x > 0)
        {
            XPos -= 1;
        }
        if (transform.position.y - target.position.y > 0)
        {
            YPos += 1;
        }
        else if (transform.position.y - target.position.y < 0)
        {
            YPos -= 1;
        }
    }

    private void MovePlayer()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _target.position, step);

        if (transform.position == _target.position)
            _move = false;
    }
}