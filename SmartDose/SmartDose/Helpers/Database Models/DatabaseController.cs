using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class DatabaseController
    {
        readonly SQLiteAsyncConnection database;
        public class HeparinRow
        {
            public int Weight { get; private set; }
            public int InitialSpeed { get; private set; }
            public int Bolus80 { get; private set; }
            public int Bolus40 { get; private set; }
            public int SpeedPlus1 { get; private set; }
            public int SpeedPlus2 { get; private set; }
            public int SpeedPlus3  { get; private set; }
            public int SpeedPlus4 { get; private set; }

            public HeparinRow(int weight, int initialSpeed, int bolus80, int bolus40, int speedPlus1, int speedPlus2, int speedPlus3, int speedPlus4)
            {
                Weight = weight;
                InitialSpeed = initialSpeed;
                Bolus80 = bolus80;
                Bolus40 = bolus40;
                SpeedPlus1 = speedPlus1;
                SpeedPlus2 = speedPlus2;
                SpeedPlus3 = speedPlus3;
                SpeedPlus4 = speedPlus4;
            }
        }

        public class HeparinRowAPTT
        {
            public float APPT { get; private set; }
            public int Bolus { get; private set; }
            public int DeltaSpeed { get; private set; }

            public HeparinRowAPTT(float appt, int bolus, int deltaSpeed)
            {
                APPT = appt;
                Bolus = bolus;
                DeltaSpeed = deltaSpeed;
            }
        }
        public DatabaseController()
        {
            database = new SQLiteAsyncConnection(App.DatabaseLocation);
            database.CreateTableAsync<HeparinTable>().Wait();
            Task task = Task.Run(async () =>
            {
                if (await database.Table<HeparinTable>().CountAsync() == 0)
                {
                    HeparinTable tableConstatns = new HeparinTable();
                    await database.InsertAsync(tableConstatns);

                    HeparinTable tableConstatnsAPTT = new HeparinTable();
                    await database.InsertAsync(tableConstatnsAPTT);
                    
                    SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation);

                    connection.CreateTable<HeparinTable>();
                    List<HeparinRow> rows = new List<HeparinRow>
                    {
                        new HeparinRow(50, 18, 4000, 2000, 1, 2, 3, 4),
                        new HeparinRow(52, 19, 4000, 2000, 1, 2, 3, 4),
                        new HeparinRow(54, 19, 4500, 2250, 1, 2, 3, 4),
                        new HeparinRow(56, 20, 4500, 2250, 1, 2, 3, 4),
                        new HeparinRow(58, 21, 4500, 2250, 1, 2, 3, 5),
                        new HeparinRow(60, 22, 5000, 2500, 1, 2, 4, 5),
                        new HeparinRow(62, 22, 5000, 2500, 1, 2, 4, 5),
                        new HeparinRow(64, 23, 5000, 2500, 1, 3, 4, 5),
                        new HeparinRow(66, 24, 5500, 2750, 1, 3, 4, 5),
                        new HeparinRow(68, 24, 5500, 2750, 1, 3, 4, 5),
                        new HeparinRow(70, 25, 5500, 2750, 1, 3, 4, 6),
                        new HeparinRow(72, 26, 6000, 3000, 1, 3, 4, 6),
                        new HeparinRow(74, 27, 6000, 3000, 1, 3, 4, 6),
                        new HeparinRow(76, 27, 6000, 3000, 2, 3, 5, 6),
                        new HeparinRow(78, 28, 6000, 3000, 2, 3, 5, 6),
                        new HeparinRow(80, 29, 6500, 3250, 2, 3, 5, 6),
                        new HeparinRow(82, 30, 6500, 3250, 2, 3, 5, 7),
                        new HeparinRow(84, 30, 6500, 3250, 2, 3, 5, 7),
                        new HeparinRow(86, 31, 7000, 3500, 2, 3, 5, 7),
                        new HeparinRow(88, 32, 7000, 3500, 2, 4, 5, 7),
                        new HeparinRow(90, 32, 7000, 3500, 2, 4, 5, 7),
                        new HeparinRow(92, 33, 7500, 3750, 2, 4, 6, 7),
                        new HeparinRow(94, 34, 7500, 3750, 2, 4, 6, 8),
                        new HeparinRow(96, 35, 7500, 3750, 2, 4, 6, 8),
                        new HeparinRow(98, 35, 8000, 4000, 2, 4, 6, 8),
                        new HeparinRow(100, 36, 8000, 4000, 2, 4, 6, 8)
                    };
                    foreach (var row in rows)
                    {
                        HeparinTable insert = new HeparinTable
                        {
                            Weight = row.Weight,
                            InitialSpeed = row.InitialSpeed,
                            Bolus80 = row.Bolus80,
                            Bolus40 = row.Bolus40,
                            SpeedPlus1 = row.SpeedPlus1,
                            SpeedPlus2 = row.SpeedPlus2,
                            SpeedPlus3 = row.SpeedPlus3,
                            SpeedPlus4 = row.SpeedPlus4,
                        }; 
                        connection.Insert(insert);
                    }
                    connection.Close();
                    
                    SQLiteConnection connectionAPTT = new SQLiteConnection(App.DatabaseLocation);
                    connectionAPTT.CreateTable<HeparinRowAPTT>();
                    
                    List<HeparinRowAPTT> rowsAptt = new List<HeparinRowAPTT>
                    {
                        new HeparinRowAPTT(1.2f, 80, 4),
                        new HeparinRowAPTT(1.5f,40,2),
                        new HeparinRowAPTT(2.3f,0,0),
                        new HeparinRowAPTT(3,0,-2),
                        new HeparinRowAPTT(3,0,0)
                    };
                    foreach (var rowAptt in rowsAptt)
                    {
                        HeparinTableAPTT insert = new HeparinTableAPTT
                        {
                            APTTR = rowAptt.APPT,
                            Bolus = rowAptt.Bolus,
                            DeltaSpeed = rowAptt.DeltaSpeed
                        }; 
                        connectionAPTT.Insert(insert);
                    }
                    connectionAPTT.Close();
                }
            });
            Task.WaitAll(task);
        }
        public async Task<HeparinTable[]> GetHeparrinConstants()
        {
            return await database.Table<HeparinTable>().ToArrayAsync();
        }
        public async Task<HeparinTableAPTT[]> GetHeparrinConstantsAPTT()
        {
            return await database.Table<HeparinTableAPTT>().ToArrayAsync();
        }
    }
}