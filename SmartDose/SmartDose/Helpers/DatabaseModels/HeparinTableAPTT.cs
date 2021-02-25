using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class HeparinTableAPTT
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }
        public float Aptt { get; set; }
        public int Bolus { get; set; }
    }
}