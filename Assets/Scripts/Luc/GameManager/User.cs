
public class User
{
    public string Username;
    public string Password;
    public int ScoreGame1;
    public int ScoreGame2;
    public int ScoreGame3;
    public int ScoreGame4;
    public int ScoreGame5;
    public int ScoreSum;
    public User(string name, string password, int scoregame1, int scoregame2, int scoregame3, int scoregame4, int scoregame5, int scoresum)
    {
        this.Username = name;
        this.Password = password;
        this.ScoreGame1 = scoregame1;
        this.ScoreGame2 = scoregame2;
        this.ScoreGame3 = scoregame3;
        this.ScoreGame4 = scoregame4;
        this.ScoreGame5 = scoregame5;
        this.ScoreSum = scoresum;
    }
}
