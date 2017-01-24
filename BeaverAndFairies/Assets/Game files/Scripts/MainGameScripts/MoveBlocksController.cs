using UnityEngine;
using System.Collections;
using System;

public class MoveBlocksController {

	public GameLogicController gameLogicController;

	public void moveDownBlocks()
	{
		foreach (GameObject block in gameLogicController._currentBlocks) 
		{
			BlockTasksController blockTypeComponent = block.GetComponent<BlockTasksController>();
			if (blockTypeComponent.placed == false) 
			{
				tryToMoveDownBlock(block);
			}
		}
	}

	void tryToMoveDownBlock(GameObject aBlock)
	{
		aBlock.transform.localPosition = new Vector3(aBlock.transform.localPosition.x, aBlock.transform.localPosition.y - gameLogicController.blocksSpeed, 0);

		BlockTasksController blockTypeComponent = aBlock.GetComponent<BlockTasksController>();

		BlockTasksController blockTasksController = aBlock.GetComponent<BlockTasksController>();
		Renderer renderer = blockTasksController.blockRect.GetComponent<Renderer>();

		float loseHeight = (gameLogicController._blockHeight / 2);

		if (aBlock.transform.localPosition.y < loseHeight) {
			blockTypeComponent.placed = true;
			aBlock.transform.localPosition = new Vector3 (aBlock.transform.localPosition.x, aBlock.transform.localPosition.y + gameLogicController.blocksSpeed, 0);
		} else {
			foreach (GameObject blockForCollision in gameLogicController._currentBlocks)
			{
				if (aBlock != blockForCollision) 
				{
					BlockTasksController blockTasksControllerForCollision = blockForCollision.GetComponent<BlockTasksController>();
					Renderer blockForCollisionRenderer = blockTasksControllerForCollision.blockRect.GetComponent<Renderer>();

					if(renderer.bounds.Intersects(blockForCollisionRenderer.bounds) == true || aBlock.transform.localPosition.y < loseHeight)
					{
						int index =  Array.IndexOf(gameLogicController._currentBlocks.ToArray(), aBlock);
						blockTypeComponent.placed = true;
						float blockHeight = renderer.bounds.size.y;
						Vector3 finalPosition = new Vector3(blockForCollision.transform.localPosition.x, blockForCollision.transform.localPosition.y + gameLogicController.blocksSpeed + blockHeight + 0.01f, 0);
						aBlock.transform.localPosition = finalPosition;
						break;
					}
				}
			}
		}
	}

}
