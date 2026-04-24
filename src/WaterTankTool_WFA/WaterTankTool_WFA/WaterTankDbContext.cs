using Microsoft.EntityFrameworkCore;
using WaterTankTool_WFA.Entity;

public class WaterTankDbContext : DbContext
{
    private static WaterTankDbContext _instance;
    private static readonly object _lock = new object();
    //private static string _defaultConnectionString = "Data Source=default_project_path\\project_data.db"; // Set this to a default path

    private static string _defaultConnectionString;

    public DbSet<SegmentProperties> SegmentProperties { get; set; }
    public DbSet<MaterialProperties> MaterialProperties { get; set; }
    public DbSet<TankProperties> TankProperties { get; set; }

    public DbSet<WindLoadEntity> WindLoadEntity {  get; set; }

    public DbSet<LiveLoadEntity> LiveLoadEntity { get; set; }
    public DbSet<SnowLoadEntity> SnowLoadEntity { get; set; }
    public DbSet<SeismicLoadEntity> SeismicLoadEntity { get; set; }   

    public DbSet<DeadLoadEntity> DeadLoadEntity { get; set; }


    //Foundations
    public DbSet<AnchorBoltEntity> AnchorBoltEntity { get; set; }

    public DbSet<BasePlateEntity> BasePlateEntity { get; set; }


    // Private constructor to prevent direct instantiation

    public WaterTankDbContext() : base() { }
    public WaterTankDbContext(string connectionString)
    {
        _defaultConnectionString = connectionString;
    }


    public static WaterTankDbContext GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new WaterTankDbContext(_defaultConnectionString);
                }
            }
        }
        return _instance;
    }

    public static void SetConnectionString(string connectionString)
    {
        lock (_lock)
        {
            _defaultConnectionString = connectionString;
            _instance = null; // Reset the instance so it uses the new connection string on the next call
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_defaultConnectionString);
        }
    }

    public void EnsureDatabaseCreated()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error ensuring database is created: {ex.Message}");
        }
    }
}
