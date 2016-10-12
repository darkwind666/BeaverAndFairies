using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameLogicController : MonoBehaviour {

	public GameObject[] blockTemplates;
	public int explosionsBonusCount;
	public float blocksSpeed;
	public int boardHeight;
	public GameObject blockExample;
	public Text scoreLabel;

	public float oneBlockAnimationDuration;
	public float scaleAnimationDuration;
	public int spawnTime;

	public int maxLightningBounsCount;
	public int maxSlowdownBounsCount;

	public float _blockHeight;
	public int _score;
	public Queue<GameObject> _currentBlocks;
	public SwipeDirectionController _swipeDirectionController;

	int _currentSpawnTime;
	float _loseHeight;
	bool _lose;
	int _currentLightningBounsCount;
	int _currentSlowdownBounsCount;

	PlayerInputController _playerInputController;
	MoveBlocksController _moveBlocksController;

	void Start () {
	
		Renderer renderer = blockExample.GetComponent<Renderer>();
		_blockHeight = renderer.bounds.size.y;
		_loseHeight = (_blockHeight * boardHeight);
		_currentSpawnTime = 0;
		_currentBlocks = new Queue<GameObject>();
		_score = 0;
		_playerInputController = new PlayerInputController();
		_playerInputController.gameLogicController = this;
		_swipeDirectionController = new SwipeDirectionController();
		_moveBlocksController = new MoveBlocksController();
		_moveBlocksController.gameLogicController = this;
		_currentLightningBounsCount = maxLightningBounsCount;
		_currentSlowdownBounsCount = maxSlowdownBounsCount;
	}
		
	void Update () {

		if(_lose == false)
		{
			spawnNewBlock();
			_swipeDirectionController.getTouchDirection();
			_playerInputController.getUserInput();
			_moveBlocksController.moveDownBlocks();
			checkGameResult();
		}

		scoreLabel.text = _score.ToString();
	}

	void spawnNewBlock()
	{
		_currentSpawnTime++;

		if(_currentSpawnTime >= spawnTime)
		{
			_currentSpawnTime = 0;
			int blockIndex = Random.Range(0, blockTemplates.Length);
			GameObject block = Instantiate(blockTemplates[blockIndex], transform.position, Quaternion.identity) as GameObject;
			block.transform.SetParent(transform.parent, false);
			block.transform.position = transform.position;
			_currentBlocks.Enqueue(block);
		}
	}

	public void showErrorSwipeAnimation()
	{
		GameObject firstBlock = _currentBlocks.Peek();
		foreach (Transform child in firstBlock.transform)
		{
			Vector3 startScale = child.localScale;
			Vector3 newScale = new Vector3 (startScale.x * 1.5f, startScale.y * 1.5f, 0);
			Sequence scaleSequence = DOTween.Sequence();
			scaleSequence.Append(child.DOScale(newScale, scaleAnimationDuration));
			scaleSequence.Append(child.DOScale(startScale, scaleAnimationDuration));
		}
	}

	public void removeFirstBlock()
	{
		GameObject firstBlock = _currentBlocks.Dequeue();
		Destroy(firstBlock);
		startMoveUpBlocks();
	}

	void startMoveUpBlocks()
	{
		foreach (GameObject block in _currentBlocks) 
		{
			BlockTypeController blockTypeComponent = block.GetComponent<BlockTypeController>();
			if (blockTypeComponent.placed == true) 
			{
				blockTypeComponent.placed = false;
			}
		}
	}

	void checkGameResult()
	{
		foreach (GameObject block in _currentBlocks) 
		{
			BlockTypeController blockTypeComponent = block.GetComponent<BlockTypeController>();

			if (blockTypeComponent.placed == true && block.transform.localPosition.y > (_loseHeight * _blockHeight)) 
			{
				_lose = true;
				break;
			}
		}
	}

	public void useLightningBonus()
	{
		if(_currentLightningBounsCount > 0)
		{
			int explosionsCount = explosionsBonusCount;
			if(_currentBlocks.Count < explosionsBonusCount)
			{
				explosionsCount = _currentBlocks.Count;
			}

			for(int explosionIndex = 0; explosionIndex < explosionsCount; explosionIndex++)
			{
				GameObject firstBlock = _currentBlocks.Dequeue();
				Destroy(firstBlock);
			}

			startMoveUpBlocks();

			_currentLightningBounsCount--;
		}
	}
}
