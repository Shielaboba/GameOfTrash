public class TrashManager {

    private static TrashManager Instance;
    private TrashData CurrentTrash;

    private TrashManager()
    {

    }

    public static TrashManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new TrashManager();
            return Instance;
        }
    }

    public TrashData GetTrash() { return CurrentTrash; }
    public void SetTrash(TrashData trash) { this.CurrentTrash = trash; }
}
