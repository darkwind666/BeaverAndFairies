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
			BlockTasksController blockTasksController = firstBlock.GetComponent<BlockTasksController>();
			List<GameObject> blockTasks = blockTasksController.blockTasks;
			GameObject firstBlockTask = blockTasks[0];

			BlockTypeController blockTypeComponent = firstBlockTask.GetComponent<BlockTypeController>();
			int blockType = blockTypeComponent.blockType;

			bool rigthSwipe = false;

			if (blockType == 1 && gameLogicController._swipeDirectionController.mMessageIndex == 3) {
				blockTasks.Remove(firstBlockTask);
				GameObject.Destroy(firstBlockTask);
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 2 && gameLogicController._swipeDirectionController.mMessageIndex == 4)
			{
				blockTasks.Remove(firstBlockTask);
				GameObject.Destroy(firstBlockTask);
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 4 && gameLogicController._swipeDirectionController.mMessageIndex == 2)
			{
				blockTasks.Remove(firstBlockTask);
				GameObject.Destroy(firstBlockTask);
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 3 && gameLogicController._swipeDirectionController.mMessageIndex == 1)
			{
				blockTasks.Remove(firstBlockTask);
				GameObject.Destroy(firstBlockTask);
				gameLogicController._score += 1;
				rigthSwipe = true;
			}

			if(blockType == 5 && Input.GetMouseButtonDown(0) == true)
			{
				blockTasks.Remove(firstBlockTask);
				GameObject.Destroy(firstBlockTask);
				gameLogicController._score += 1;
			}

			if(blockTasks.Count <= 0)
			{
				gameLogicController.removeFirstBlock();
			}

			if(blockType > 0 && blockType < 5 && rigthSwipe == false && gameLogicController._swipeDirectionController.mMessageIndex > 0){
				gameLogicController.showErrorSwipeAnimation();
			}
		}

		gameLogicController._swipeDirectionController.mMessageIndex = 0;
	}

}
