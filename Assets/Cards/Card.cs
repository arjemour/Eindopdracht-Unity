using UnityEngine;

public class Card : MonoBehaviour
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public bool Clickable { get; set; }
    public CardEvent CardEvent { get; set; }
    public GameManager GameManager;
    public bool Turned { get; set; }

    private void Awake()
    {
        Clickable = false;
    }
}