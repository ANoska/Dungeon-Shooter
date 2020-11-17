using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    #region Public Members

    [Header("Set in Inspector")]
    public GameObject[] _DungeonPieces;
    public GameObject _PlayerPrefab;
    public int _PieceLength = 8;

    [Header("Set Dynamically")]
    public LinkedList<GameObject> _ActivePieces;
    public GameObject _Player;

    #endregion

    #region Constants

    private const int SETTING_MAX_PIECES = 7;
    private const string TAG_DUNGEON_PIECE = "DungeonPiece";
    private const string GAME_OBJECT_NAME_LOAD_TRIGGER_PLUS = "LoadTrigger_Plus";
    private const string GAME_OBJECT_NAME_LOAD_TRIGGER_MINUS = "LoadTrigger_Minus";
    private const string GAME_OBJECT_NAME_SMOKE_PARENT_PLUS = "SmokeParent_Plus";
    private const string GAME_OBJECT_NAME_SMOKE_PARENT_MINUS = "SmokeParent_Minus";

    #endregion

    #region Private Members

    #endregion

    #region Unity Messages

    // Start is called before the first frame update
    void Start()
    {
        _ActivePieces = new LinkedList<GameObject>();

        InitializeDungeon();
        InitializePlayer();
        UpdateSmoke();
        UpdateLoadTriggers();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSmoke();
        UpdateLoadTriggers();
    }

    #endregion

    #region Initialize Environment

    private void InitializeDungeon()
    {
        // Place first piece at origin
        GameObject previousPiece = Instantiate(_DungeonPieces[0], new Vector3(0,0,0), Quaternion.identity);

        // Store in the collection
        _ActivePieces.AddLast(previousPiece);

        // Offset to place future pieces
        Vector3 pieceOffset = new Vector3(8, 0, 0);

        // Base generation
        while(_ActivePieces.Count < SETTING_MAX_PIECES)
        {
            int nextPiece = Random.Range(0, _DungeonPieces.Length);
            previousPiece = Instantiate(_DungeonPieces[nextPiece], previousPiece.transform.position + pieceOffset, Quaternion.identity);
            foreach(DungeonPieceLoadTrigger lt in previousPiece.GetComponentsInChildren<DungeonPieceLoadTrigger>())
            {
                lt.OnDungeonPieceLoadTriggerPlayerCollision += LtOnDungeonPieceLoadTriggerPlayerCollision;
            }
            
            _ActivePieces.AddLast(previousPiece);
        }
    }

    private void InitializePlayer()
    {
        // Calculate player position
        Vector3 playerPos = _ActivePieces.ElementAt((int)Mathf.Floor(SETTING_MAX_PIECES / 2)).transform.position + new Vector3(0, 2, 0);

        // Spawn player
        _Player = Instantiate(_PlayerPrefab, playerPos, Quaternion.identity);
    }

    #endregion

    #region Environment Updates

    private void UpdateSmoke()
    {
        foreach (GameObject piece in _ActivePieces)
        {
            if (_ActivePieces.First.Value == piece)
            {
                piece.transform.Find(GAME_OBJECT_NAME_SMOKE_PARENT_PLUS).gameObject.SetActive(true);
                piece.transform.Find(GAME_OBJECT_NAME_SMOKE_PARENT_MINUS).gameObject.SetActive(false);
            }
            else if (_ActivePieces.Last.Value == piece)
            {
                piece.transform.Find(GAME_OBJECT_NAME_SMOKE_PARENT_PLUS).gameObject.SetActive(false);
                piece.transform.Find(GAME_OBJECT_NAME_SMOKE_PARENT_MINUS).gameObject.SetActive(true);
            }
            else
            {
                piece.transform.Find(GAME_OBJECT_NAME_SMOKE_PARENT_PLUS).gameObject.SetActive(false);
                piece.transform.Find(GAME_OBJECT_NAME_SMOKE_PARENT_MINUS).gameObject.SetActive(false);
            }
        }
    }

    private void UpdateLoadTriggers()
    {
        foreach (GameObject piece in _ActivePieces)
        {
            if (_ActivePieces.ElementAt((int)Mathf.Floor(SETTING_MAX_PIECES / 2)) == piece)
            {
                piece.transform.Find(GAME_OBJECT_NAME_LOAD_TRIGGER_PLUS).GetComponent<BoxCollider>().enabled = true;
                piece.transform.Find(GAME_OBJECT_NAME_LOAD_TRIGGER_MINUS).GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                piece.transform.Find(GAME_OBJECT_NAME_LOAD_TRIGGER_PLUS).GetComponent<BoxCollider>().enabled = false;
                piece.transform.Find(GAME_OBJECT_NAME_LOAD_TRIGGER_MINUS).GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    #endregion

    #region Load Trigger Callbacks

    private void LtOnDungeonPieceLoadTriggerPlayerCollision(object sender, string loadTriggerName)
    {
        GameObject go;
        switch (loadTriggerName)
        {
            case GAME_OBJECT_NAME_LOAD_TRIGGER_MINUS:
                Destroy(_ActivePieces.Last.Value);
                _ActivePieces.RemoveLast();

                go = Instantiate(_DungeonPieces[Random.Range(0, _DungeonPieces.Length)], _ActivePieces.First.Value.transform.position - new Vector3(8, 0, 0), Quaternion.identity);
                foreach(DungeonPieceLoadTrigger lt in go.GetComponentsInChildren<DungeonPieceLoadTrigger>())
                {
                    lt.OnDungeonPieceLoadTriggerPlayerCollision += LtOnDungeonPieceLoadTriggerPlayerCollision;
                }
                _ActivePieces.AddFirst(go);
                break;

            case GAME_OBJECT_NAME_LOAD_TRIGGER_PLUS:
                Destroy(_ActivePieces.First.Value);
                _ActivePieces.RemoveFirst();

                go = Instantiate(_DungeonPieces[Random.Range(0, _DungeonPieces.Length)], _ActivePieces.Last.Value.transform.position + new Vector3(8, 0, 0), Quaternion.identity);
                foreach (DungeonPieceLoadTrigger lt in go.GetComponentsInChildren<DungeonPieceLoadTrigger>())
                {
                    lt.OnDungeonPieceLoadTriggerPlayerCollision += LtOnDungeonPieceLoadTriggerPlayerCollision;
                }
                _ActivePieces.AddLast(go);
                break;
        }
    }

    #endregion
}
