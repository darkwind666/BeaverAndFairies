using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameLogicController : MonoBehaviour {

	public float blocksSpeed;
	public int boardHeight;
	public GameObject blockExample;
	public Text scoreLabel;
	float _blockHeight;
	float _loseHeight;
	bool _lose;
	bool _stopGame;
	int _score;

	public GameObject[] blockTemplates;
	Queue<GameObject> _currentBlocks;

	public float oneBlockAnimationDuration;
	public int spawnTime;
	int _currentSpawnTime;

	SwipeDirectionController _swipeDirectionController;

	void Start () {
	
		Renderer renderer = blockExample.GetComponent<Renderer>();
		_blockHeight = renderer.bounds.size.y;
		_loseHeight = (_blockHeight * boardHeight);
		_currentSpawnTime = 0;
		_currentBlocks = new Queue<GameObject>();
		_score = 0;
		_swipeDirectionController = new SwipeDirectionController();
	}
		
	void Update () {

		if(_lose == false && _stopGame == false)
		{
			spawnNewBlock();
			_swipeDirectionController.getTouchDirection();
			getUserInput();
			moveDownBlocks();
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

	void getUserInput()
	{
		if(_currentBlocks.Count > 0) 
		{
			GameObject firstBlock = _currentBlocks.Peek();
			BlockTypeController blockTypeComponent = firstBlock.GetComponent<BlockTypeController>();
			int blockType = blockTypeComponent.blockType;

			if(blockType == 1 && _swipeDirectionController.mMessageIndex == 3)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 2 && _swipeDirectionController.mMessageIndex == 4)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 3 && _swipeDirectionController.mMessageIndex == 2)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 4 && _swipeDirectionController.mMessageIndex == 1)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 5 && Input.GetMouseButtonDown(0) == true)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 6 && Input.touchCount == 2)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 7 && Input.touchCount == 3)
			{
				removeFirstBlock();
				_score += 1;
			}

			if(blockType == 8 && Input.touchCount == 4)
			{
				removeFirstBlock();
				_score += 1;
			}

			if (Input.GetMouseButtonDown (0) == true && blockType > 5) {
				blockTypeComponent.blockType -= 1;
				GameObject firstChild = firstBlock.transform.GetChild(0).gameObject;
				Destroy(firstChild);
			}
		}

		_swipeDirectionController.mMessageIndex = 0;
	}

	void removeFirstBlock()
	{
		GameObject firstBlock = _currentBlocks.Dequeue();
		Destroy(firstBlock);

		//startFallBlocksAnimation();

		foreach (GameObject block in _currentBlocks) 
		{
			BlockTypeController blockTypeComponent = firstBlock.GetComponent<BlockTypeController>();
			if (blockTypeComponent.placed == true) 
			{
				_stopGame = true;
				Sequence fallShapesSequence = DOTween.Sequence();
				fallShapesSequence.AppendInterval(oneBlockAnimationDuration);
				fallShapesSequence.AppendCallback(() => _stopGame = false);
				break;
			}
		}

		foreach (GameObject block in _currentBlocks) 
		{
			BlockTypeController blockTypeComponent = firstBlock.GetComponent<BlockTypeController>();
			if (blockTypeComponent.placed == true) 
			{
				Vector3 finalPosition = new Vector3(block.transform.localPosition.x, block.transform.localPosition.y - _blockHeight, 0);
				block.transform.DOLocalMove(finalPosition, oneBlockAnimationDuration);
			}
		}
	}

	void startFallBlocksAnimation()
	{

	}

	void moveDownBlocks()
	{
		foreach (GameObject block in _currentBlocks) 
		{
			BlockTypeController blockTypeComponent = block.GetComponent<BlockTypeController>();
			if (blockTypeComponent.placed == false) 
			{
				tryToMoveDownBlock(block);
			}
		}
	}

	void tryToMoveDownBlock(GameObject aBlock)
	{
		aBlock.transform.localPosition = new Vector3(aBlock.transform.localPosition.x, aBlock.transform.localPosition.y - blocksSpeed, 0);

		BlockTypeController blockTypeComponent = aBlock.GetComponent<BlockTypeController>();
		Renderer renderer = aBlock.GetComponent<Renderer>();

		float loseHeight = (_blockHeight / 2);

		if (aBlock.transform.localPosition.y < loseHeight) {
			blockTypeComponent.placed = true;
			aBlock.transform.localPosition = new Vector3 (aBlock.transform.localPosition.x, aBlock.transform.localPosition.y + blocksSpeed, 0);
		} else {
			foreach (GameObject blockForCollision in _currentBlocks)
			{
				if (aBlock != blockForCollision) 
				{
					Renderer blockForCollisionRenderer = blockForCollision.GetComponent<Renderer>();
					if(renderer.bounds.Intersects(blockForCollisionRenderer.bounds) == true || aBlock.transform.localPosition.y < loseHeight)
					{
						blockTypeComponent.placed = true;
						Vector3 finalPosition = new Vector3(blockForCollision.transform.localPosition.x, blockForCollision.transform.localPosition.y + _blockHeight, 0);
						aBlock.transform.localPosition = finalPosition;
						break;
					}
				}
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
}
