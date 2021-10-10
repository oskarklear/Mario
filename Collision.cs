using System;
using System.Collections.Generic;
using Mario.Map;

public interface ICollider : ISprite
{
	public void Collision(ICollider collider, int xOffset,int yOffset);
}

public class Collision
{
    List<ICollider>[] CollisionGrid;
	public Collision(Map map)
    {
       
    }
}
