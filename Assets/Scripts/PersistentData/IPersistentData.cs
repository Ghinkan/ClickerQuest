namespace ClickerQuest.PersistentData
{
    public interface IPersistentData
    {
        public void Save();
        public void Load();
        public void Restore();
    }
}