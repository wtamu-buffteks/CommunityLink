public interface IMessageService
{
    bool HasUnreadMessages(int userId);
    bool HasActionNeeded(int userId);
    int TotalMessagesCount(int userId);
}
