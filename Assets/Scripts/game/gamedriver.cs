using UnityEngine;

public class gamedriver : MonoBehaviour
{
    [SerializeField] basechar leftCharacter;
    [SerializeField] basechar rightCharacter;
    private Battlemagnager Battlemagnager;



    private void Start()
    {
        Battlemagnager = new Battlemagnager(leftCharacter, rightCharacter);
        BeginBattle();
    }

    private void BeginBattle()
    {
        Battlemagnager .BeginBattle();
    }
}
