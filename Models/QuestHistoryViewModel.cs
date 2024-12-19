namespace DigiGall.Models
{
    public class QuestHistoryViewModel
    {
        public IEnumerable<PemberianQuest> PemberianQuests { get; set; }
        public IEnumerable<Quest> Quests { get; set; }
    }
}