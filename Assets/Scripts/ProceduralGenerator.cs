using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProceduralGenerator : MonoBehaviour
{
	public static int size;

	[SerializeField]
	public GameObject[] Rooms;//initialized in the inspector, ignore the null warning

	[SerializeField]
	public GameObject marker;//ditto

    [SerializeField]
    public GameObject spawner;//double ditto

    public enum RoomTypes
	{
		//11 in total, ya dingus
		CROSS = 0,
		END_RIGHT = 1,
		END_UP = 2,
		END_LEFT = 3,
		END_DOWN = 4,
		UP_LEFT = 5,
		UP_RIGHT = 6,
		DOWN_LEFT = 7,
		DOWN_RIGHT = 8,
		VERTICAL = 9,
		HORIZONTAL = 10
	}



	public class Room
	{



		static System.Random rand = new System.Random();

		public RoomTypes type;

		int x;

		int y;

		public Room(RoomTypes type, int posX, int posY)
		{
			this.type = type;
			this.x = posX;
			this.y = posY;
			map[-y + 2, x + 2] = this;

			generateFrom(type);
		}

		void generateFrom(RoomTypes type)
		{


			switch (type)
			{
			//fuck da police
			case RoomTypes.CROSS:
				{
					//top
					if (getAtCoords(x, y + 1) == null)
					{
						spawnUp(x, y);
					}

					//right
					if (getAtCoords(x + 1, y) == null)
					{
						spawnRight(x, y);
					}

					//bottom
					if (getAtCoords(x, y - 1) == null)
					{
						spawnDown(x, y);
					}

					//left
					if (getAtCoords(x - 1, y) == null)
					{
						spawnLeft(x, y);
					}

				}
				break;
			case RoomTypes.UP_LEFT:
				{
					if (getAtCoords(x - 1, y) == null)//left
					{
						if (x - 1 == -(size / 2))//termination is important
						{
							new Room(RoomTypes.END_LEFT, x - 1, y);
						}
						else
						{
							spawnLeft(x, y);
						}
					}
					if (getAtCoords(x, y - 1) == null)//down
					{
						if (y - 1 == -(size / 2))//termination is important
						{
							new Room(RoomTypes.END_UP, x, y - 1);
						}
						else
						{
							spawnDown(x, y);
						}
					}
				}
				break;
			case RoomTypes.UP_RIGHT:
				{
					if (getAtCoords(x + 1, y) == null)//right
					{
						if (x + 1 == size / 2)//termination is important
						{
							new Room(RoomTypes.END_RIGHT, x + 1, y);
						}
						else
						{
							spawnRight(x, y);
						}
					}
					if (getAtCoords(x, y - 1) == null)//down
					{
						if (y - 1 == -(size / 2))//termination is important
						{
							new Room(RoomTypes.END_DOWN, x, y - 1);
						}
						else
						{
							spawnDown(x, y);
						}
					}
				}
				break;
			case RoomTypes.DOWN_LEFT:
				{
					if (getAtCoords(x - 1, y) == null)//left
					{
						if (x - 1 == -(size / 2))//termination is important
						{
							new Room(RoomTypes.END_LEFT, x - 1, y);
						}
						else
						{
							spawnLeft(x, y);
						}
					}
					if (getAtCoords(x, y + 1) == null)//up
					{
						if (y + 1 == size / 2)//termination is important
						{
							new Room(RoomTypes.END_UP, x, y + 1);
						}
						else
						{
							spawnUp(x, y);
						}
					}
				}
				break;
			case RoomTypes.DOWN_RIGHT:
				{
					if (getAtCoords(x + 1, y) == null)//right
					{
						if (x + 1 == size / 2)//termination is important
						{
							new Room(RoomTypes.END_RIGHT, x + 1, y);
						}
						else
						{
							spawnRight(x, y);
						}
					}
					if (getAtCoords(x, y + 1) == null)//up
					{
						if (y + 1 == size / 2)//termination is important
						{
							new Room(RoomTypes.END_UP, x, y + 1);
						}
						else
						{
							spawnUp(x, y);
						}
					}
				}
				break;
			case RoomTypes.VERTICAL:
				{
					if (getAtCoords(x, y + 1) == null)//up
					{
						if (y + 1 == size / 2)//termination is important
						{
							new Room(RoomTypes.END_UP, x, y + 1);
						}
						else
						{
							spawnUp(x, y);
						}
					}
					if (getAtCoords(x, y - 1) == null)//down
					{
						if (y - 1 == -(size / 2))//termination is important
						{
							new Room(RoomTypes.END_DOWN, x, y - 1);
						}
						else
						{
							spawnDown(x, y);
						}
					}
				}
				break;
			case RoomTypes.HORIZONTAL:
				{
					if (getAtCoords(x + 1, y) == null)//right
					{
						if (x + 1 == size / 2)//termination is important
						{
							new Room(RoomTypes.END_RIGHT, x + 1, y);
						}
						else
						{
							spawnRight(x, y);
						}
					}
					if (getAtCoords(x - 1, y) == null)//left
					{
						if (x - 1 == -(size / 2))//termination is important
						{
							new Room(RoomTypes.END_LEFT, x - 1, y);
						}
						else
						{
							spawnLeft(x, y);
						}
					}
				}
				break;
			}
		}

		static void spawnUp(int x, int y)
		{
			//1 to 4
			int g = rand.Next(4) + 1;

			switch (g)
			{
			case 1:
				new Room(RoomTypes.VERTICAL, x, y + 1);
				break;
			case 2:
				new Room(RoomTypes.UP_LEFT, x, y + 1);
				break;
			case 3:
				new Room(RoomTypes.UP_RIGHT, x, y + 1);
				break;
			case 4:
				new Room(RoomTypes.END_UP, x, y + 1);
				break;
			}
		}

		static void spawnDown(int x, int y)
		{
			//1 to 4
			int g = rand.Next(4) + 1;

			switch (g)
			{
			case 1:
				new Room(RoomTypes.VERTICAL, x, y - 1);
				break;
			case 2:
				new Room(RoomTypes.DOWN_LEFT, x, y - 1);
				break;
			case 3:
				new Room(RoomTypes.DOWN_RIGHT, x, y - 1);
				break;
			case 4:
				new Room(RoomTypes.END_DOWN, x, y - 1);
				break;
			}
		}

		static void spawnLeft(int x, int y)
		{
			//1 to 4
			int g = rand.Next(4) + 1;

			switch (g)
			{
			case 1:
				new Room(RoomTypes.HORIZONTAL, x - 1, y);
				break;
			case 2:
				new Room(RoomTypes.UP_RIGHT, x - 1, y);
				break;
			case 3:
				new Room(RoomTypes.DOWN_RIGHT, x - 1, y);
				break;
			case 4:
				new Room(RoomTypes.END_LEFT, x - 1, y);
				break;
			}
		}

		static void spawnRight(int x, int y)
		{
			//1 to 4
			int g = rand.Next(4) + 1;

			switch (g)
			{
			case 1:
				new Room(RoomTypes.HORIZONTAL, x + 1, y);
				break;
			case 2:
				new Room(RoomTypes.DOWN_LEFT, x + 1, y);
				break;
			case 3:
				new Room(RoomTypes.UP_LEFT, x + 1, y);
				break;
			case 4:
				new Room(RoomTypes.END_RIGHT, x + 1, y);
				break;
			}
		}
	}

	public static Room[,] map;

	public static Room getAtCoords(int x, int y)
	{

		Room room = map[-y + 2, x + 2];

		return room;
	}

	void Start()
	{
		size = 5;

		System.Random rand = new System.Random ();

		map = new Room[5, 5];

		Vector3 pos = new Vector3 ();

		Vector3 spawnerpos = new Vector3 ();

		Room start = new Room(RoomTypes.CROSS, 0, 0);

		List<Vector3> roomOrder = new List<Vector3>();

		for (int y = 2; y >= -2; y--)
		{
			for (int x = -2; x <= 2; x++)
			{
				pos.Set((float)x*6,((float)y*6),0.0f);//why this has to be 6 and not 8, I have no fucking clue. But hey, it works!
				if (getAtCoords(x, y) != null)
				{
					roomOrder.Add (pos);
					Instantiate(Rooms [(int)getAtCoords (x, y).type], pos, Quaternion.identity);


					int spawnercount = rand.Next (5);

					if (x != 0 || y != 0)
					for (int i = 0; i <= spawnercount; i++) 
					{
						float spawnerX = rand.Next (-2,2);
						float spawnerY = rand.Next (-2,2);
						spawnerpos.Set ((float)((x*6)+spawnerX+0.5f),(float)((y*6)+spawnerY+0.5f),0.0f);
						Instantiate(spawner, spawnerpos, Quaternion.identity);
					}
				}
			}
		}
		//spawn boss
		Instantiate (marker, roomOrder.Last(), Quaternion.identity);
	}
}