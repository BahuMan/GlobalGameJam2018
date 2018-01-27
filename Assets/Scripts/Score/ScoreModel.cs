
public class ScoreModel
{
    public delegate void ScoreChangedHandler(int score);
    public static event ScoreChangedHandler OnScoreChanged;

    private static int _score = 0;
    public static int Score { get { return _score; } }

    public static void AddScore(int diff)
    {
        _score += diff;
        if (OnScoreChanged != null) OnScoreChanged(_score);
    }

    public static void ResetScore()
    {
        _score = 0;
        if (OnScoreChanged != null) OnScoreChanged(_score);
    }
}
