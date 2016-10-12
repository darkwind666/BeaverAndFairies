using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputController {

	public GameLogicController gameLogicController;

	public PlayerInputController()
	{
		
	}

	public void getUserInput()
	{
		if(gameLogicController._currentBlocks.Count > 0) 
		{
			GameObject firstBlock = gameLogicController._currentBlocks.Peek();
			BlockTypeController blockTypeComponent = firstBlock.GetComponent<BlockTypeController>();
			int blockType = blockTypeComponent.blockType;

			bool rigthSwipe = false;

			if (blockType == 1 && gameLogicController._swipeDirectionController.mMessageIndex == 3) {
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 2 && gameLogicController._swipeDirectionController.mMessageIndex == 4)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 3 && gameLogicController._swipeDirectionController.mMessageIndex == 2)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 4 && gameLogicController._swipeDirectionController.mMessageIndex == 1)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 5 && Input.GetMouseButtonDown(0) == true)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
			}

			if(blockType == 6 && Input.touchCount == 2)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
			}

			if(blockType == 7 && Input.touchCount == 3)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
			}

			if(blockType == 8 && Input.touchCount == 4)
			{
				gameLogicController.removeFirstBlock();
				gameLogicController._score += 1;
			}

			if (Input.GetMouseButtonDown (0) == true && blockType > 5) {
				blockTypeComponent.blockType -= 1;
				GameObject firstChild = firstBlock.transform.GetChild(0).gameObject;
				GameObject.Destroy(firstChild);
			}

			if(blockType > 0 && blockType < 5 && rigthSwipe == false && gameLogicController._swipeDirectionController.mMessageIndex > 0){
				gameLogicController.showErrorSwipeAnimation();
			}
		}

		gameLogicController._swipeDirectionController.mMessageIndex = 0;
	}

}
