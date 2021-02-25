using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class DatabaseController
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseController()
        {
            // Database connection
            _database = new SQLiteAsyncConnection(App.DatabaseLocation);
            _database.CreateTableAsync<HeparinTableAPTT>().Wait();
            var createHeparinTable = Task.Run(async () =>
            {
                if (await _database.Table<HeparinTableAPTT>().CountAsync() == 0)
                {
                    var tableConstatns = new HeparinTableAPTT();
                    await _database.InsertAsync(tableConstatns);

                    var connection = new SQLiteConnection(App.DatabaseLocation);

                    // Preparing Values to DB 
                    connection.CreateTable<HeparinTableAPTT>();
                    var rows = new List<APPTRow>
                    {
                        new APPTRow(1.2f, 80),
                        new APPTRow(1.5f, 40),
                        new APPTRow(2.3f, 0),
                        new APPTRow(3f, 0)
                    };
                    // Loading Values to DB 
                    foreach (var row in rows)
                    {
                        var insert = new HeparinTableAPTT
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
            return await _database.Table<HeparinTableAPTT>().ToArrayAsync();
        }

        // Support class used only for inicialization
        public class APPTRow
        {
            public APPTRow(float aptt, int bolus)
            {
                this.aptt = aptt;
                this.bolus = bolus;
            }

            public float aptt { get; }
            public int bolus { get; }
        }
    }
}