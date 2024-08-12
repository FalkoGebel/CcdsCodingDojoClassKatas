namespace _02_Bowling
{
    public class Game
    {
        private Frame[] _frames;

        public int TotalScore
        {
            get => _frames.Select(f => f.Score).Sum();
        }

        public Game()
        {
            _frames = [];
        }

        /// <summary>
        /// Adds the pins hit with a roll to the current frame of the game. The pins are also added to the previous frames, if
        /// there were strikes and / or spares.
        /// </summary>
        /// <param name="pins">Number of pins hit.</param>
        /// <exception cref="ArgumentException">Pins not between 0 and 10.</exception>
        public void AddRoll(int pins)
        {
            if (Over())
                throw new InvalidOperationException("Game is over - no more rolls can be added");

            if (pins < 0 || pins > 10)
                throw new ArgumentException("Invalid number of pins - has to be between 0 and 10");

            if (_frames.Length < 10 && (_frames.Length == 0 || _frames[^1].PinsRolled.Length == 2 || _frames[^1].PinsRolled.Sum() == 10))
            {
                Array.Resize(ref _frames, _frames.Length + 1);
                _frames[^1] = new Frame();
            }

            Array.Resize(ref _frames[^1].PinsRolled, _frames[_frames.Length - 1].PinsRolled.Length + 1);
            _frames[^1].PinsRolled[^1] = pins;
            _frames[^1].Score += pins;

            // Add pins to previous frame(s)
            if (_frames.Length > 1)
            {
                Frame currentFrame = _frames[^1],
                      previousFrame = _frames[^2];

                // First roll on current frame
                if (currentFrame.PinsRolled.Length == 1)
                {
                    if (previousFrame.PinsRolled.Sum() == 10)
                    {
                        previousFrame.Score += pins;

                        if (previousFrame.PinsRolled.Length == 1 && _frames.Length > 2)
                        {
                            Frame beforePreviousFrame = _frames[^3];
                            if (beforePreviousFrame.PinsRolled.Sum() == 10 && beforePreviousFrame.PinsRolled.Length == 1)
                                beforePreviousFrame.Score += pins;
                        }
                    }
                }
                // Second roll on current frame
                else if (currentFrame.PinsRolled.Length == 2)
                {
                    if (previousFrame.PinsRolled.Sum() == 10 && previousFrame.PinsRolled.Length == 1)
                        previousFrame.Score += pins;
                }
            }
        }

        public bool Over()
        {
            return _frames.Length == 10 &&
                   ((_frames[^1].PinsRolled.Length == 2 && _frames[^1].PinsRolled[0] < 10 && _frames[^1].PinsRolled.Sum() < 10) ||
                    (_frames[^1].PinsRolled.Length == 3));
        }
    }
}