public static class DbContextProvider
{
    private static WaterTankDbContext _context;

    public static void InitializeContext(string connectionString)
    {
        if (_context != null)
        {
            _context.Dispose();
        }
        _context = new WaterTankDbContext(connectionString);
    }

    public static WaterTankDbContext GetContext()
    {
        if (_context == null)
        {
            throw new InvalidOperationException("DbContext has not been initialized. Call InitializeContext() first.");
        }
        return _context;
    }

    public static void DisposeContext()
    {
        _context?.Dispose();
        _context = null;
    }
}