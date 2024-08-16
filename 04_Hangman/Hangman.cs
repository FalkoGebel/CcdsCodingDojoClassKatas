namespace _04_Hangman
{
    public class Hangman
    {
        private string _searchedWord;
        private char[] _currentGuessStatus;

        public Hangman(string searchedWord)
        {
            _searchedWord = searchedWord;
            _currentGuessStatus = Enumerable.Range(0, _searchedWord.Length).Select(i => '-').ToArray();
        }

        public string GuessLetter(char v)
        {
            for (int i = 0; i < _searchedWord.Length; i++)
            {
                if (char.ToLower(_searchedWord[i]) == char.ToLower(v) &&
                    char.ToLower(_currentGuessStatus[i]) != char.ToLower(v))
                    _currentGuessStatus[i] = _searchedWord[i];
            }

            return string.Concat(_currentGuessStatus);
        }
    }
}