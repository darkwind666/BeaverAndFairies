using UnityEngine;
using System.Collections;

public class MoveBlocksController {

	public GameLogicController gameLogicController;

	public void moveDownBlocks()
	{
		foreach (GameObject block in gameLogicController._currentBlocks) 
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
		aBlock.transform.localPosition = new Vector3(aBlock.transform.localPosition.x, aBlock.transform.localPosition.y - gameLogicController.blocksSpeed, 0);

		BlockTypeController blockTypeComponent = aBlock.GetComponent<BlockTypeController>();
		Renderer renderer = aBlock.GetComponent<Renderer>();

		float loseHeight = (gameLogicController._blockHeight / 2);

		if (aBlock.transform.localPosition.y < loseHeight) {
			blockTypeComponent.placed = true;
			aBlock.transform.localPosition = new Vector3 (aBlock.transform.localPosition.x, aBlock.transform.localPosition.y + gameLogicController.blocksSpeed, 0);
		} else {
			foreach (GameObject blockForCollision in gameLogicController._currentBlocks)
			{
				if (aBlock != blockForCollision) 
				{
					Renderer blockForCollisionRenderer = blockForCollision.GetComponent<Renderer>();
					if(renderer.bounds.Intersects(blockForCollisionRenderer.bounds) == true || aBlock.transform.localPosition.y < loseHeight)
					{
						blockTypeComponent.placed = true;
						Vector3 finalPosition = new Vector3(blockForCollision.transform.localPosition.x, blockForCollision.transform.localPosition.y + gameLogicController._blockHeight + 0.01f, 0);
						aBlock.transform.localPosition = finalPosition;
						break;
					}
				}
			}
		}
	}

}
