using CommunityLink.Models;
using Microsoft.EntityFrameworkCore;

public class MessageService : IMessageService
{
    private readonly CommunityLinkDbContext _context;

    public MessageService(CommunityLinkDbContext context)
    {
        _context = context;
    }

    public bool HasUnreadMessages(int userId)
    {
        return _context.Messages.Any(m => m.ReceiverID == userId && !m.Read);
    }

    public bool HasActionNeeded(int userId)
    {
        return _context.ActionNeededs.Any(a => a.UserID == userId);
    }

    public int TotalMessagesCount(int userId)
    {
        int unreadMessageCount = _context.Messages.Count(m => m.ReceiverID == userId && !m.Read);
        int actionNeededCount = _context.ActionNeededs.Count(a => a.UserID == userId);
        return unreadMessageCount + actionNeededCount;
    }
}
