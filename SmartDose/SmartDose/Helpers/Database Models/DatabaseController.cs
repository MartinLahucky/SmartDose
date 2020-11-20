using System.Threading.Tasks;
using SQLite;

namespace SmartDose.Helpers.Database_Models
{
    public class DatabaseController
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseController()
        {
            database = new SQLiteAsyncConnection(App.DatabaseLocation);
            database.CreateTableAsync<CalculatorModel>().Wait();
            Task task = Task.Run(async () =>
            {
                if (await database.Table<CalculatorModel>().CountAsync() == 0)
                {
                    CalculatorModel tabulkaModel = new CalculatorModel
                    {
                        ID = 0,
                    };
                    await database.InsertAsync(tabulkaModel);
                }
            });
            Task.WaitAll(task);
        }
    }
}