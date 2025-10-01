/// <summary>
/// Interface containing a LevelStart, LevelWin and LevelLose function, for a level script to inherit from
/// </summary>
public interface Level {
    public void LevelStart();
    public void LevelWin();
    public void LevelLose();
}
