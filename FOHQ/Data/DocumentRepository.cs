using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий объектов "Документ приложения".
    /// </summary>
    public class DocumentRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public string StatusMessage { get; set; }

        public DocumentRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

#if DEBUG
            // для пересоздания всех таблиц
            //      _connection.DropTableAsync<Document>().Wait();
            //      _connection.DropTableAsync<Order>().Wait();
            //      _connection.DropTableAsync<FireUnit>().Wait();
            //      _connection.DropTableAsync<CombatArea>().Wait();

            //      _connection.DropTableAsync<Quenching>().Wait();
            //      _connection.DropTableAsync<RequiredConsumption>().Wait();
            //      _connection.DropTableAsync<NumberOfDevices>().Wait();
            //      _connection.DropTableAsync<QuenchingSolution>().Wait();
            //      _connection.DropTableAsync<FilingMethods>().Wait();
#endif

            _connection.CreateTableAsync<Document>().Wait();
            _connection.CreateTableAsync<Order>().Wait();
            _connection.CreateTableAsync<FireUnit>().Wait();
            _connection.CreateTableAsync<CombatArea>().Wait();

            _connection.CreateTableAsync<Quenching>().Wait();
            _connection.CreateTableAsync<RequiredConsumption>().Wait();
            _connection.CreateTableAsync<NumberOfDevices>().Wait();
            _connection.CreateTableAsync<QuenchingSolution>().Wait();
            _connection.CreateTableAsync<FilingMethods>().Wait();
        }

        public async Task<Document> GetItemAsync(int id)
        {
            try
            {
                var document = await _connection.GetAsync<Document>(id);
                var orders = await _connection.Table<Order>().ToListAsync();
                var fireUnits = await _connection.Table<FireUnit>().ToListAsync();
                var combatAreas = await _connection.Table<CombatArea>().ToListAsync();
                var quenchings = await _connection.GetAsync<Quenching>(document.Id);
                var requiredConsumption = await _connection.GetAsync<RequiredConsumption>(document.Id);
                var numberOfDevices = await _connection.GetAsync<NumberOfDevices>(document.Id);
                var quenchingSolution = await _connection.GetAsync<QuenchingSolution>(document.Id);
                var filingMethods = await _connection.GetAsync<FilingMethods>(document.Id);

                document.Orders.AddRange(
                    orders.Where(x => x.DocumentId == document.Id));
                document.FireUnits.AddRange(
                    fireUnits.Where(x => x.DocumentId == document.Id));
                document.CombatAreas.AddRange(
                    combatAreas.Where(x => x.DocumentId == document.Id));
                document.Quenching = quenchings;
                document.Quenching.Document = document;
                document.RequiredConsumption = requiredConsumption;
                document.RequiredConsumption.Document = document;
                document.NumberOfDevices = numberOfDevices;
                document.NumberOfDevices.Document = document;
                document.QuenchingSolution = quenchingSolution;
                document.QuenchingSolution.Document = document;
                document.FilingMethods = filingMethods;
                document.FilingMethods.Document = document;
                return document;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new Document();
        }

        public async Task<List<Document>> GetItemsAsync()
        {
            try
            {
                var documents = await _connection.Table<Document>().ToListAsync();
                var orders = await _connection.Table<Order>().ToListAsync();
                var fireUnits = await _connection.Table<FireUnit>().ToListAsync();
                var combatAreas = await _connection.Table<CombatArea>().ToListAsync();

                foreach (var document in documents)
                {
                    document.Orders.AddRange(
                        orders.Where(x => x.DocumentId == document.Id));
                    document.FireUnits.AddRange(
                        fireUnits.Where(x => x.DocumentId == document.Id));
                    document.CombatAreas.AddRange(
                        combatAreas.Where(x => x.DocumentId == document.Id));
                    var quenchings = await _connection.GetAsync<Quenching>(document.Id);
                    var requiredConsumption = await _connection.GetAsync<RequiredConsumption>(document.Id);
                    var numberOfDevices = await _connection.GetAsync<NumberOfDevices>(document.Id);
                    var quenchingSolution = await _connection.GetAsync<QuenchingSolution>(document.Id);
                    var filingMethods = await _connection.GetAsync<FilingMethods>(document.Id);
                    document.Quenching = quenchings;
                    document.Quenching.Document = document;
                    document.RequiredConsumption = requiredConsumption;
                    document.RequiredConsumption.Document = document;
                    document.NumberOfDevices = numberOfDevices;
                    document.NumberOfDevices.Document = document;
                    document.QuenchingSolution = quenchingSolution;
                    document.QuenchingSolution.Document = document;
                    document.FilingMethods = filingMethods;
                    document.FilingMethods.Document = document;
                }

                return documents;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new List<Document>();
        }

        public async Task<int> DeleteItemAsync(Document item)
        {
            try
            {
                foreach (var order in item.Orders)
                {
                    await _connection.DeleteAsync(order);
                }
                foreach (var fireUnit in item.FireUnits)
                {
                    await _connection.DeleteAsync(fireUnit);
                }
                foreach (var combatArea in item.CombatAreas)
                {
                    await _connection.DeleteAsync(combatArea);
                }
                var quenching = item.Quenching;
                await _connection.DeleteAsync(quenching);
                var requiredConsumption = item.RequiredConsumption;
                await _connection.DeleteAsync(requiredConsumption);
                var numberOfDevices = item.NumberOfDevices;
                await _connection.DeleteAsync(numberOfDevices);
                var quenchingSolution = item.QuenchingSolution;
                await _connection.DeleteAsync(quenchingSolution);
                var filingMethods = item.FilingMethods;
                await _connection.DeleteAsync(filingMethods);
                return await _connection.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format($"Failed to delete item. Error: {ex.Message}");
            }

            return 0;
        }

        public async Task<int> SaveItemAsync(Document item)
        {
            try
            {
                if (item.Id != 0)
                {
                    await _connection.UpdateAsync(item);

                    foreach (var order in item.Orders)
                    {
                        if (order.Id != 0)
                        {
                            await _connection.UpdateAsync(order);
                        }
                        else
                        {
                            await _connection.InsertAsync(order);
                        }
                    }
                    foreach (var fireUnit in item.FireUnits)
                    {
                        if (fireUnit.Id != 0)
                        {
                            await _connection.UpdateAsync(fireUnit);
                        }
                        else
                        {
                            await _connection.InsertAsync(fireUnit);
                        }
                    }
                    foreach (var combatArea in item.CombatAreas)
                    {
                        if (combatArea.Id != 0)
                        {
                            await _connection.UpdateAsync(combatArea);
                        }
                        else
                        {
                            await _connection.InsertAsync(combatArea);
                        }
                    }

                    if (item.Quenching.Id != 0)
                    {
                        await _connection.UpdateAsync(item.Quenching);
                    }
                    else
                    {
                        await _connection.InsertAsync(item.Quenching);
                    }
                    if (item.RequiredConsumption.Id != 0)
                    {
                        await _connection.UpdateAsync(item.RequiredConsumption);
                    }
                    else
                    {
                        await _connection.InsertAsync(item.RequiredConsumption);
                    }

                    if (item.NumberOfDevices.Id != 0)
                    {
                        await _connection.UpdateAsync(item.NumberOfDevices);
                    }
                    else
                    {
                        await _connection.InsertAsync(item.NumberOfDevices);
                    }
                    if (item.QuenchingSolution.Id != 0)
                    {
                        await _connection.UpdateAsync(item.QuenchingSolution);
                    }
                    else
                    {
                        await _connection.InsertAsync(item.QuenchingSolution);
                    }
                    if (item.FilingMethods.Id != 0)
                    {
                        await _connection.UpdateAsync(item.FilingMethods);
                    }
                    else
                    {
                        await _connection.InsertAsync(item.FilingMethods);
                    }

                    return item.Id;
                }
                else
                {
                    await _connection.InsertAsync(item);
                    await _connection.InsertAllAsync(item.Orders);
                    await _connection.InsertAllAsync(item.FireUnits);
                    await _connection.InsertAllAsync(item.CombatAreas);
                    item.Quenching.DocumentId = item.Id;
                    item.RequiredConsumption.DocumentId = item.Id;
                    item.NumberOfDevices.DocumentId = item.Id;
                    item.QuenchingSolution.DocumentId = item.Id;
                    item.FilingMethods.DocumentId = item.Id;

                    await _connection.InsertAsync(item.Quenching);
                    await _connection.InsertAsync(item.RequiredConsumption);
                    await _connection.InsertAsync(item.NumberOfDevices);
                    await _connection.InsertAsync(item.QuenchingSolution);
                    await _connection.InsertAsync(item.FilingMethods);
                    return item.Id;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format($"Failed to save item. Error: {ex.Message}");
            }

            return 0;
        }
    }
}