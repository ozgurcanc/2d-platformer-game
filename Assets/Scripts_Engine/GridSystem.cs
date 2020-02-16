using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cyclone
{
    public class Cell
    {
        private List<PColliderGenerator> colliders = new List<PColliderGenerator>();

        public void Add(PColliderGenerator _cg)
        {
            colliders.Add(_cg);
        }

        public PColliderGenerator this[int index]
        {
            get
            {
                return colliders[index];
            }
            set
            {
                colliders[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return colliders.Count;
            }
        }

        public void Clear()
        {
            colliders.Clear();
        }
        
    } 

    
    public class Map
    {

        private Vector2D origin;
        private float rightExtend;
        private float leftExtend;
        private float upExtend;
        private float downExtend;

        private float cellSizeX;
        private float cellSizeY;

        private int rowCount;
        private int columnCount;

        private Cell[] cells;


        public Map(Vector2D _origin,float _rightExtend,float _leftExtend,float _upExtend,float _downExtend, float _cellSizeX,float _cellSizeY)
        {
            origin = _origin;
            rightExtend = _rightExtend;
            leftExtend = _leftExtend;
            upExtend = _upExtend;
            downExtend = _downExtend;
            cellSizeX = _cellSizeX;
            cellSizeY = _cellSizeY; 

            columnCount = (int) ((rightExtend + leftExtend)/cellSizeX);
            rowCount = (int)((upExtend*downExtend)/cellSizeY);

            int cellCount = columnCount * rowCount;

            cells = new Cell[cellCount];

            for(int i=0;i<cellCount;i++)
            {
                cells[i] = new Cell();
            }
        }

        public Cell this[int row,int column]
        {
            get
            {
                return cells[row*columnCount +column];
            }
        }

        public void SetupMap(List<PColliderGenerator> colliders)
        {
            foreach (PColliderGenerator pcg in colliders)
            {
                PBoxCollider2D boxColliders =  (PBoxCollider2D) pcg;

                Vector2D colliderOrigin = (Vector2D) boxColliders.owner.transform.position;
                
                Vector2D size = new Vector2D(boxColliders.xSize,boxColliders.ySize);
                Vector2D TopRight = colliderOrigin + size;
                Vector2D BottomLeft = colliderOrigin - size;

                float x = TopRight.x;
                float y = TopRight.y;

                int trColumnIndex = (int) (( x+leftExtend)/cellSizeX);
                int trRowIndex = (int) (( y+downExtend)/cellSizeY);

                x = BottomLeft.x;
                y = BottomLeft.y;

                int blColumnIndex = (int) (( x+leftExtend)/cellSizeX);
                int blRowIndex = (int) (( y+downExtend)/cellSizeY);

                for(int i=blRowIndex; i<= trRowIndex ; i++)
                {
                    for(int j=blColumnIndex ; j <= trColumnIndex ; j++)
                    {
                        this[i,j].Add(pcg);
                    }
                }
            }
        }

        public List<PContact> GenerateContacts()
        {
            List<PContact> contacts = new List<PContact>();

            foreach (Cell aCell in cells)
            {
                int cellSize = aCell.Count;

                for(int i = 0 ; i<cellSize ; i++)
                {
                    for(int j = i+1; j<cellSize ; j++)
                    {
                        if(IntersectionTests.IntersectionTest(aCell[i],aCell[j]))
                        {   
                            PContact newContact = cyclone.GenerateContacts.GenerateContact(aCell[i],aCell[j]);
                            if(newContact != null) contacts.Add(newContact);
                        }
                    }
                }
                aCell.Clear();
            }

            return contacts;
        }
        
        public void Clear()
        {
            foreach (Cell aCell in cells)
            {
                aCell.Clear();
            }
        }

    }      
}

