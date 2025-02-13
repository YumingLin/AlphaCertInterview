﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CanWeFixItService
{
    public class DatabaseService : IDatabaseService
    {
        // See SQLite In-Memory example:
        // https://github.com/dotnet/docs/blob/main/samples/snippets/standard/data/sqlite/InMemorySample/Program.cs

        // Using a name and a shared cache allows multiple connections to access the same
        // in-memory database
        const string connectionString = "Data Source=DatabaseService;Mode=Memory;Cache=Shared";
        const string createInstruments = @"
                CREATE TABLE instruments
                (
                    id     int,
                    sedol  text,
                    name   text,
                    active int
                );
                INSERT INTO instruments
                VALUES (1, 'Sedol1', 'Name1', 0),
                       (2, 'Sedol2', 'Name2', 1),
                       (3, 'Sedol3', 'Name3', 0),
                       (4, 'Sedol4', 'Name4', 1),
                       (5, 'Sedol5', 'Name5', 0),
                       (6, '', 'Name6', 1),
                       (7, 'Sedol7', 'Name7', 0),
                       (8, 'Sedol8', 'Name8', 1),
                       (9, 'Sedol9', 'Name9', 0)";

        const string createMarketData = @"
                CREATE TABLE marketdata
                (
                    id        int,
                    datavalue int,
                    sedol     text,
                    active    int
                );
                INSERT INTO marketdata
                VALUES (1, 1111, 'Sedol1', 0),
                       (2, 2222, 'Sedol2', 1),
                       (3, 3333, 'Sedol3', 0),
                       (4, 4444, 'Sedol4', 1),
                       (5, 5555, 'Sedol5', 0),
                       (6, 6666, 'Sedol6', 1)";
        
        const string instrumentsQuery = "SELECT id, sedol, name, 'true' as active from instruments where active = 1";
        const string marketDataQuery = "SELECT m.id, m.DataValue, i.id as instrumentId, 'true' as active FROM MarketData m join instruments i on m.sedol = i.sedol WHERE m.Active = 1";
        const string marketValueQuery = "SELECT 'DataValueTotal' as name, sum(datavalue) as total FROM MarketData where active = 1";


        private SqliteConnection _connection;

        protected void ensureConnectionOpen()
        {
            if (_connection != null) return;
            
            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        }

        protected async Task<IEnumerable<T>> runQuery<T>(string query)
        {
            try
            {
                ensureConnectionOpen();
                return await _connection.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                //simplified exception processing
                throw new ApplicationException($"Exception while accessing the database: {ex.Message}");
            }
        }

        /// <summary>
        /// This is complete and will correctly load the test data.
        /// It is called during app startup 
        /// </summary>
        public void SetupDatabase()
        {
            try
            {
                ensureConnectionOpen();
                _connection.Execute(createInstruments);
                _connection.Execute(createMarketData);
            }
            catch (Exception ex)
            {
                //simplified exception processing
                throw new ApplicationException($"Exception while setting up the database: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Instrument>> Instruments()
        {
            return await runQuery<Instrument>(instrumentsQuery);
        }

        public async Task<IEnumerable<MarketDataDto>> MarketDataDto()
        {
            return await runQuery<MarketDataDto>(marketDataQuery);
        }

        public async Task<IEnumerable<MarketValuation>> MarketValuation()
        {
            return await runQuery<MarketValuation>(marketValueQuery);
        }
    }
}