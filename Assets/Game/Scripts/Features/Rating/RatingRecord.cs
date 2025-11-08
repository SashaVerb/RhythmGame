public struct RatingRecord
{
    public string name;
    public string level;
    public int score;

    public RatingRecord(string name, string level, int score)
    {
        this.name = name;
        this.level = level;
        this.score = score;
    }
}
