using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class DatabaseController
    {
        readonly SQLiteAsyncConnection database;
        // Support class used only for inicialization
        public class APPTRow
        {
            public float aptt { get; private set; }
            public int bolus { get; private set; }

            public APPTRow(float aptt, int bolus)
            {
                this.aptt = aptt;
                this.bolus = bolus;
            }
        }
        public DatabaseController()
        {
            // Database connection
            database = new SQLiteAsyncConnection(App.DatabaseLocation);
            database.CreateTableAsync<HeparinTableAPTT>().Wait();
            Task createHeparinTable = Task.Run(async () =>
            {
                if (await database.Table<HeparinTableAPTT>().CountAsync() == 0)
                {
                    HeparinTableAPTT tableConstatns = new HeparinTableAPTT();
                    await database.InsertAsync(tableConstatns);
            
                    SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation);
            
                    // Preparing Values to DB 
                    connection.CreateTable<HeparinTableAPTT>();
                    List<APPTRow> rows = new List<APPTRow>
                    {
                        new APPTRow(1.2f,80),
                        new APPTRow(1.5f,40),
                        new APPTRow(2.3f,0),
                        new APPTRow(3f,0),
                    };
                    // Loading Values to DB 
                    foreach (var row in rows)
                    {
                        HeparinTableAPTT insert = new HeparinTableAPTT
                        {
                            Aptt = row.aptt,
                            Bolus = row.bolus
                        }; 
                        connection.Insert(insert);
                    }
                    connection.Close();
                }
            });
            Task.WaitAll(createHeparinTable);
        }
        public async Task<HeparinTableAPTT[]> GetHeparrinConstants()
        {
            return await database.Table<HeparinTableAPTT>().ToArrayAsync();
        }
    }
}