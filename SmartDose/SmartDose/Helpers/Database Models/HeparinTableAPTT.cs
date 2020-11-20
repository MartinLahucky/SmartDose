using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class HeparinTableAPTT
    {
        [PrimaryKey, AutoIncrement]
        public float APTTR { get; set; }
        public int Bolus { get; set; }
        public int DeltaSpeed { get; set; }
    }
}