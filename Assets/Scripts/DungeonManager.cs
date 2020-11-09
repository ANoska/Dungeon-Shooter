using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject[] _DungeonPieces;
    public GameObject _Player;
    public int _MaxPieces = 5;

    private int _ActivePieces { get { return GameObject.FindGameObjectsWithTag("DungeonPiece").Length; } }

    // Start is called before the first frame update
    void Start()
    {
        InitializeDungeon();
        InitializePlayer();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    private void InitializeDungeon()
    {
        // Place first piece at origin
        GameObject previousPiece = Instantiate(_DungeonPieces[0], new Vector3(0,0,0), Quaternion.identity);
        Vector3 pieceOffset = new Vector3(8, 0, 0);
        while(_ActivePieces < _MaxPieces)
        {
            int nextPiece = Random.Range(0, _DungeonPieces.Length);
            previousPiece = Instantiate(_DungeonPieces[nextPiece], previousPiece.transform.position + pieceOffset, Quaternion.identity);
        }
    }

    private void InitializePlayer()
    {
        Instantiate(_Player, new Vector3(0, 2, 0), Quaternion.identity);
    }
}
