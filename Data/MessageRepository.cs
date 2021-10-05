namespace API.Data
{
  public class MessageRepository : IMessageRepository
  {
    private readonly DataContext _context;
    public MessageRepository(DataContext context)
    {
      _context = context;
    }

    public void AddMessage(Message message) {
      _context.Message.Add(message);
    }

    public void DeleteMessage(Message message) {
      _context.Message.Remove(message);
    }

    public async Task<Message> GetMessage(int id) {
      return await _context.Message.FindAsync(id);
    }

    public async Task<bool> SaveAllAsync() {
      return await _context.Message.SaveChangesAsync() > 0;
    }
  }
}