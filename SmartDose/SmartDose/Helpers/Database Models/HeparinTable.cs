using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class HeparinTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Weight { get; set; }
        public int InitialSpeed { get; set; }
        public int Bolus80 { get; set; }
        public int Bolus40 { get; set; }
        public int SpeedPlus1 { get; set; }
        public int SpeedPlus2 { get; set; }
        public int SpeedPlus3  { get; set; }
        public int SpeedPlus4 { get; set; }
    }
}