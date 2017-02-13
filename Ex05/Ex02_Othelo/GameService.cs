using System;
using System.Collections.Generic;

namespace Ex02_Othelo
{
    public delegate void FlipACoinDelegate(int i_X, int i_Y, eDiscColor i_DiscColor);
    public class GameService
    {
        private static readonly Random sr_Random = new Random();
        private eDiscColor[,] m_Board;

        public eDiscColor[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public event FlipACoinDelegate OnFlip;

        private readonly List<OtheloPoint> m_AvailableMoves = new List<OtheloPoint>();
        private Dictionary<OtheloPoint, List<OtheloPoint>> m_RequiredFlips = new Dictionary<OtheloPoint, List<OtheloPoint>>();

        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Player m_Turn; // = m_WhitePlayer;

        public Player ThisTurn
        {
            get
            {
                return m_Turn;
            }
        }

        public Player GetOppositeDiscColor(Player i_Player)
        {
            return i_Player != m_FirstPlayer ? m_FirstPlayer : m_SecondPlayer;
        }

        public GameService(int i_Size)
        {
            if ((i_Size > 2) && (i_Size % 2 == 0))
            {
                m_Board = new eDiscColor[i_Size, i_Size];
            }
            else
            {
                throw new Exception("Illegal GameBoard size");
            }

            m_AvailableMoves = new List<OtheloPoint>();
            m_RequiredFlips  = new Dictionary<OtheloPoint, List<OtheloPoint>>();
            m_FirstPlayer = new Player( i_DiscColor: eDiscColor.Black );          
            m_SecondPlayer = new Player();
        }

        private void updateAvailableMoves()
        {
            m_AvailableMoves.Clear();
            m_RequiredFlips.Clear();
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    testAndAddAvailableMoves(new OtheloPoint(i, j));
                }
            }
        }

        private void testAndAddAvailableMoves(OtheloPoint i_Square)
        {
            if (isEmptySquare(i_Square))
            {
                List<OtheloPoint> toFlip = new List<OtheloPoint>();
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.Down)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.DownLeft)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.DownRight)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.Left)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.Right)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.Up)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.UpLeft)));
                toFlip.AddRange(getDiscsToFlipInSpecifiedDirection(i_Square, getDirectionDeltaPoint(eDirection.UpRight)));

                if (toFlip.Count > 0)
                {
                    m_AvailableMoves.Add(i_Square);
                    m_RequiredFlips[i_Square] = toFlip;
                }
            }
        }

        private List<OtheloPoint> getDiscsToFlipInSpecifiedDirection(OtheloPoint i_Square, OtheloPoint i_DirectionDelta)
        {
            List<OtheloPoint> toFlip = new List<OtheloPoint>();

            do
            {
                i_Square.Add(i_DirectionDelta);

                if (isOutOfBounds(i_Square) || isEmptySquare(i_Square))
                {
                    toFlip.Clear();
                    break;
                }
                else if(m_Board[i_Square.X, i_Square.Y] == GetOppositeDiscColor(m_Turn).Color)
                {
                    toFlip.Add(i_Square);
                }
            }
            while (m_Board[i_Square.X, i_Square.Y] != m_Turn.Color);

            return toFlip;
        }

        public void SetInitialBoard(eDiscColor i_FirstTurn)
        {
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    m_Board[i, j] = eDiscColor.None;
                }
            }

            int boardCenterPosition = m_Board.GetLength(0) / 2;

            m_Board[boardCenterPosition, boardCenterPosition] = eDiscColor.White; // Board Center Position
            m_Board[boardCenterPosition - 1, boardCenterPosition] = eDiscColor.Black; // Left to Board Center Position
            m_Board[boardCenterPosition - 1, boardCenterPosition - 1] = eDiscColor.White; // Up-left to Board Center Position
            m_Board[boardCenterPosition, boardCenterPosition - 1] = eDiscColor.Black; // Up to Board Center Position

            m_FirstPlayer.DiscsCounter = 2;
            m_SecondPlayer.DiscsCounter = 2;

            m_Turn = m_FirstPlayer;
            updateAvailableMoves();
        }

        public void UpdateBoard(OtheloPoint i_Square)
        {
            m_Board[i_Square.X, i_Square.Y] = m_Turn.Color;
            if (OnFlip != null)
            {
                OnFlip.Invoke(i_Square.X, i_Square.Y, m_Turn.Color);
            }

            m_Turn.DiscsCounter++;
            foreach (OtheloPoint toFlip in m_RequiredFlips[i_Square])
            {
                m_Board[toFlip.X, toFlip.Y] = m_Turn.Color;
                if (OnFlip != null)
                {
                    OnFlip.Invoke(toFlip.X, toFlip.Y, m_Turn.Color);
                }

                m_Turn.DiscsCounter++;
                GetOppositeDiscColor(ThisTurn).DiscsCounter--;
            }
        }

        public void SwitchTurns()
        {
            m_Turn = GetOppositeDiscColor(m_Turn);
            updateAvailableMoves();
        }

        public bool IsValidMove(OtheloPoint i_Move)
        {
            return m_AvailableMoves.Contains(i_Move);
        }

        public OtheloPoint GetRandomMove()
        {          
            return m_AvailableMoves[sr_Random.Next(m_AvailableMoves.Count)];
        }

        #region Private Boolean Tests
        private bool isEmptySquare(OtheloPoint i_Square)
        {
            return m_Board[i_Square.X, i_Square.Y] == eDiscColor.None;
        }

        private bool isOutOfBounds(OtheloPoint i_Square)
        {
            return i_Square.X < 0 || m_Board.GetLength(0) <= i_Square.X ||
                   i_Square.Y < 0 || m_Board.GetLength(1) <= i_Square.Y;
        }

        public bool HasMoves()
        {
            return m_AvailableMoves.Count > 0;
        }
        #endregion

        private OtheloPoint getDirectionDeltaPoint(eDirection i_Direction)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;
            OtheloPoint desiredDeltaPoint = new OtheloPoint();

            switch (i_Direction)
            {
                case eDirection.Down:
                    desiredDeltaPoint = new OtheloPoint(k_Stay, k_Down);
                    break;
                case eDirection.DownLeft:
                    desiredDeltaPoint = new OtheloPoint(k_Left, k_Down);
                    break;
                case eDirection.DownRight:
                    desiredDeltaPoint = new OtheloPoint(k_Right, k_Down);
                    break;
                case eDirection.Left:
                    desiredDeltaPoint = new OtheloPoint(k_Left, k_Stay);
                    break;
                case eDirection.Right:
                    desiredDeltaPoint = new OtheloPoint(k_Right, k_Stay);
                    break;
                case eDirection.Up:
                    desiredDeltaPoint = new OtheloPoint(k_Stay, k_Up);
                    break;
                case eDirection.UpLeft:
                    desiredDeltaPoint = new OtheloPoint(k_Left, k_Up);
                    break;
                case eDirection.UpRight:
                    desiredDeltaPoint = new OtheloPoint(k_Right, k_Up);
                    break;
            }

            return desiredDeltaPoint;
        }
    }
}