using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.dao.ado.connectors;
using System.IO;
using System.Data.SqlClient;

namespace CyberPushkin.dao.ado
{
    public class ADOBaseCreator : StorageCreator
    {
        private ADOSQLiteConnector connector;

        public ADOBaseCreator(ADOSQLiteConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentException();
            }

            this.connector = connector;
        }
        public void createStorageFolder(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        public void createStorage()
        {
            List<String> sqlQueries = new List<String>();

            // create database structure
            //

            sqlQueries.Add("CREATE TABLE IF NOT EXISTS CharacteristicsNames " +
                "( `Id` INTEGER PRIMARY KEY AUTOINCREMENT, `Name` TEXT UNIQUE )");
            sqlQueries.Add("CREATE TABLE IF NOT EXISTS CharacteristicsValues " +
                "( `Id` INTEGER, `CharacteristicsNameId` INTEGER, `Value` TEXT, PRIMARY KEY(`Id`,`CharacteristicsNameId`,`Value`) )");
            sqlQueries.Add("CREATE TABLE IF NOT EXISTS ClassifiableTexts " +
                "( `Id` INTEGER PRIMARY KEY AUTOINCREMENT, `Text` TEXT )");
            sqlQueries.Add("CREATE TABLE IF NOT EXISTS ClassifiableTextsCharacteristics " +
                "( `ClassifiableTextId` INTEGER, `CharacteristicsNameId` INTEGER, `CharacteristicsValueId` INTEGER, PRIMARY KEY(`ClassifiableTextId`,`CharacteristicsNameId`,`CharacteristicsValueId`) )");
            sqlQueries.Add("CREATE TABLE IF NOT EXISTS Vocabulary " +
                "( `Id` INTEGER PRIMARY KEY AUTOINCREMENT, `Value` TEXT UNIQUE )");

            executeQueries(sqlQueries);
        }

      
  public void clearStorage()
        {
            List<String> sqlQueries = new List<String>();

            sqlQueries.Add("DELETE FROM CharacteristicsNames");
            sqlQueries.Add("DELETE FROM CharacteristicsValues");
            sqlQueries.Add("DELETE FROM ClassifiableTexts");
            sqlQueries.Add("DELETE FROM ClassifiableTextsCharacteristics");
            sqlQueries.Add("DELETE FROM Vocabulary");

            // reset autoincrement keys
            sqlQueries.Add("DELETE FROM sqlite_sequence WHERE name IN " +
                "('CharacteristicsNames', 'CharacteristicsValues', 'ClassifiableTexts', " +
                "'ClassifiableTextsCharacteristics', 'Vocabulary')");

            executeQueries(sqlQueries);
        }

        private void executeQueries(List<String> sqlQueries)
        {
            try {
                SqlConnection con = connector.getConnection();

                foreach (String sqlQuery in sqlQueries)
                {
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.ExecuteNonQuery();

                }
            } catch (SqlException ignored)
            {
            }
        }


    }
}
