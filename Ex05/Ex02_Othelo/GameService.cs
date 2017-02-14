using System;
using System.Collections.Generic;

namespace Ex02_Othelo
{
    public delegate void TurnOverACoinDelegate(int i_X, int i_Y, eDiscColor i_DiscColor);

    public class GameService
    {
        private static readonly Random sr_Random = new Random();
        private readonly List<OthelloPoint> r_AvailableMoves = new List<OthelloPoint>();
        private readonly Dictionary<OthelloPoint, List<OthelloPoint>> r_RequiredFlips = new Dictionary<OthelloPoint, List<OthelloPoint>>();
        private eDiscColor[,] m_Board;

        public eDiscColor[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public event TurnOverACoinDelegate TurningOverACoin;

        private Player m_ThisTurn;

        public IPlayer ThisTurn
        {
            get
            {
                return m_ThisTurn;
            }
        }

        private Player m_FirstPlayer;

        public IPlayer FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }
        }

        private Player m_SecondPlayer;

        public IPlayer SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }
        }

        private int m_NumberOfPlayedRounds;

        public int NumberOfPlayedRounds
        {
            get
            {
                return m_NumberOfPlayedRounds;
            }
        }

        private Player getOpponent(Player i_Player)
        {
            return i_Player != m_FirstPlayer ? m_FirstPlayer : m_SecondPlayer;
        }

        public GameService(int i_BoardSize, string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsComputerOpponent)
        {
            const bool v_FirstPlayerIsComputer = false;
            r_AvailableMoves = new List<OthelloPoint>();
            r_RequiredFlips = new Dictionary<OthelloPoint, List<OthelloPoint>>();

            if ((i_BoardSize > 2) && (i_BoardSize % 2 == 0))
            {
                m_Board = new eDiscColor[i_BoardSize, i_BoardSize];
            }
            else
            {
                throw new Exception("Illegal GameBoard size");
            }

            m_FirstPlayer = new Player(i_FirstPlayerName, eDiscColor.FirstColor, v_FirstPlayerIsComputer);
            m_SecondPlayer = new Player(i_SecondPlayerName, eDiscColor.SecondColor, i_IsComputerOpponent);
            m_NumberOfPlayedRounds = 0;
        }

        private void updateAvailableMoves()
        {
            r_AvailableMoves.Clear();
            r_RequiredFlips.Clear();
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    testAndAddAvailableMoves(new OthelloPoint(i, j));
                }
            }
        }

        private void testAndAddAvailableMoves(OthelloPoint i_Square)
        {
            if (isEmptySquare(i_Square))
            {
                List<OthelloPoint> toFlip = new List<OthelloPoint>();
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
                    r_AvailableMoves.Add(i_Square);
                    r_RequiredFlips[i_Square] = toFlip;
                }
            }
        }

        private List<OthelloPoint> getDiscsToFlipInSpecifiedDirection(OthelloPoint i_Square, OthelloPoint i_DirectionDelta)
        {
            List<OthelloPoint> toFlip = new List<OthelloPoint>();

            do
            {
                i_Square.Add(i_DirectionDelta);
                if (isOutOfBounds(i_Square) || isEmptySquare(i_Square))
                {
                    toFlip.Clear();
                    break;
                }
                else if(m_Board[i_Square.X, i_Square.Y] == getOpponent(m_ThisTurn).Color)
                {
                    toFlip.Add(i_Square);
                }
            }
            while (m_Board[i_Square.X, i_Square.Y] != m_ThisTurn.Color);

            return toFlip;
        }

        public void SetInitialBoard(eDiscColor i_FirstTurn)
        {
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    OnTurningOverACoin(i, j, eDiscColor.None);
                }
            }

            int boardCenterPosition = m_Board.GetLength(0) / 2;
            OnTurningOverACoin(boardCenterPosition, boardCenterPosition, eDiscColor.SecondColor); // Board Center Position
            OnTurningOverACoin(boardCenterPosition - 1, boardCenterPosition, eDiscColor.FirstColor); // Left to Board Center Position
            OnTurningOverACoin(boardCenterPosition - 1, boardCenterPosition - 1, eDiscColor.SecondColor); // Up-left to Board Center Position
            OnTurningOverACoin(boardCenterPosition, boardCenterPosition - 1, eDiscColor.FirstColor); // Up to Board Center Position
            m_FirstPlayer.DiscsCounter = 2;
            m_SecondPlayer.DiscsCounter = 2;
            m_ThisTurn = m_FirstPlayer;
            updateAvailableMoves();
        }

        public eGameResult GetGameResult()
        {
            eGameResult result = eGameResult.Tie;
            if(m_FirstPlayer.DiscsCounter > m_SecondPlayer.DiscsCounter)
            {
                m_FirstPlayer.RoundsWon++;
                result = eGameResult.FirstPlayerWon;
            }
            else if(m_FirstPlayer.DiscsCounter < m_SecondPlayer.DiscsCounter)
            {
                m_SecondPlayer.RoundsWon++;
                result = eGameResult.SecondPlayerWon;
            }

            m_NumberOfPlayedRounds++;
            return result;
        }

        private void OnTurningOverACoin(int i_X, int i_Y, eDiscColor i_DiscColor)
        {
            m_Board[i_X, i_Y] = i_DiscColor;
            if (TurningOverACoin != null)
            {
                TurningOverACoin.Invoke(i_X, i_Y, i_DiscColor);
            }
        }

        public void UpdateBoard(int i_X, int i_Y)
        {
            UpdateBoard(new OthelloPoint(i_X, i_Y));
        }

        public void UpdateBoard(OthelloPoint i_Square)
        {
            OnTurningOverACoin(i_Square.X, i_Square.Y, m_ThisTurn.Color);
            m_ThisTurn.DiscsCounter++;
            foreach (OthelloPoint toFlip in r_RequiredFlips[i_Square])
            {
                OnTurningOverACoin(toFlip.X, toFlip.Y, m_ThisTurn.Color);

                m_ThisTurn.DiscsCounter++;
                getOpponent(m_ThisTurn).DiscsCounter--;
            }
        }

        public void SwitchTurns()
        {
            m_ThisTurn = getOpponent(m_ThisTurn);
            updateAvailableMoves();
        }

        public bool IsValidMove(OthelloPoint i_Move)
        {
            return r_AvailableMoves.Contains(i_Move);
        }

        public bool IsValidMove(int i_X, int i_Y)
        {
            return IsValidMove(new OthelloPoint(i_X, i_Y));
        }

        public OthelloPoint GetRandomMove()
        {          
            return r_AvailableMoves[sr_Random.Next(r_AvailableMoves.Count)];
        }

        #region Private Boolean Tests
        private bool isEmptySquare(OthelloPoint i_Square)
        {
            return m_Board[i_Square.X, i_Square.Y] == eDiscColor.None;
        }

        private bool isOutOfBounds(OthelloPoint i_Square)
        {
            return i_Square.X < 0 || m_Board.GetLength(0) <= i_Square.X ||
                   i_Square.Y < 0 || m_Board.GetLength(1) <= i_Square.Y;
        }

        public bool HasMoves()
        {
            return r_AvailableMoves.Count > 0;
        }
        #endregion

        private OthelloPoint getDirectionDeltaPoint(eDirection i_Direction)
        {
            const int k_Up = -1, k_Left = -1, k_Down = 1, k_Right = 1, k_Stay = 0;
            OthelloPoint desiredDeltaPoint = new OthelloPoint();
            switch (i_Direction)
            {
                case eDirection.Down:
                    desiredDeltaPoint = new OthelloPoint(k_Stay, k_Down);
                    break;
                case eDirection.DownLeft:
                    desiredDeltaPoint = new OthelloPoint(k_Left, k_Down);
                    break;
                case eDirection.DownRight:
                    desiredDeltaPoint = new OthelloPoint(k_Right, k_Down);
                    break;
                case eDirection.Left:
                    desiredDeltaPoint = new OthelloPoint(k_Left, k_Stay);
                    break;
                case eDirection.Right:
                    desiredDeltaPoint = new OthelloPoint(k_Right, k_Stay);
                    break;
                case eDirection.Up:
                    desiredDeltaPoint = new OthelloPoint(k_Stay, k_Up);
                    break;
                case eDirection.UpLeft:
                    desiredDeltaPoint = new OthelloPoint(k_Left, k_Up);
                    break;
                case eDirection.UpRight:
                    desiredDeltaPoint = new OthelloPoint(k_Right, k_Up);
                    break;
            }

            return desiredDeltaPoint;
        }
    }
}