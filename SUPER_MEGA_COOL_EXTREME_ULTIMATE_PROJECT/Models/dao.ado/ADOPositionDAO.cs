using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CyberPushkin.dao.ado.connectors;
using CyberPushkin.model;
using CyberPushkin.dao;
using CyberPushkin.addirionDLL;


namespace CyberPushkin.dao.ado
{
    class ADOPositionDAO : PositionDAO
    {

        private ADOSQLiteConnector connector;
        private int idCount = 0;

        public ADOPositionDAO(ADOSQLiteConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentException();
            }

            this.connector = connector;
        }

        public List<Position> getAllCharacteristics()
        {
            List<Position> positons = new List <Position> ();
            try  {
                SqlConnection con = connector.getConnection();

                String sqlSelect = "SELECT Id, Name FROM CharacteristicsNames";
                SqlCommand command = new SqlCommand(sqlSelect, con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        Position positon = new Position(reader.GetInt32(0), reader.GetString(1));
                        // get all possible values
                        positon.setPossibleValues(getAllPossibleValues(con, positon));

                        positons.Add(positon);
                    }

                }

            } catch (SqlException ignored)
            {
            }

            return positons;
        }

        public Position addCharacteristic(Position positon) {
            throw new NotImplementedException();
            if (positon == null ||
                positon.getName().Equals("") ||
                positon.getPossibleValues() == null ||
                positon.getPossibleValues().Count == 0) {
                 throw new EmptyRecordException("Characteristic and/or Possible values are null or empty");
            }

            

            try
            {
                SqlConnection con = connector.getConnection();
                if (isCharacteristicExistsInDB(con, positon))
                {
                    throw new AlreadyExistsException("Characteristic already exists");
                }
            

            String sqlInsert = String.Format("INSERT INTO CharacteristicsNames (Name) VALUES '{0}'", positon.getName());
            SqlCommand command = new SqlCommand(sqlInsert, con);
            int numberOfinserted = command.ExecuteNonQuery();

            
      
      if (numberOfinserted>0) {
        // set inserted row Id to Characteristic
        positon.setId(idCount++);

        // insert possible values
        //

        foreach (PositionValue possibleValue in positon.getPossibleValues()) {
          insertPossibleValue(con, positon, possibleValue);
        }
      }
    } catch (SqlException ignored) {
    }

    return positon;
  }



        private ISet<PositionValue> getAllPossibleValues(SqlConnection con, Position positon) 
        {
            ISet<PositionValue> possibleValues = new OrderedSet<PositionValue>();

            String sqlSelect = String.Format("SELECT Id, Value FROM CharacteristicsValues WHERE CharacteristicsNameId = {0}", positon.getId());
            SqlCommand command = new SqlCommand(sqlSelect, con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    possibleValues.Add(new PositionValue(reader.GetInt32(0), reader.GetString(1)));
                }

            }
            return possibleValues;
        }


        private bool isCharacteristicExistsInDB(SqlConnection con, Position positon) 
        {
            String sqlSelect = String.Format("SELECT Id FROM CharacteristicsNames WHERE Name = '{0}'", positon.getName());
            SqlCommand command = new SqlCommand(sqlSelect, con);
            SqlDataReader reader = command.ExecuteReader();

            return reader.HasRows;
        }

        private void insertPossibleValue(SqlConnection con, Position positon, PositionValue positonValue) 
        {
            if (positonValue != null &&
                !positonValue.getValue().Equals("")) {

                // try to find Value in DB
                int newCharacteristicValueId = searchCharacteristicPossibleValue(con, positon, positonValue);

                if (newCharacteristicValueId == -1)
                { // not found -> insert it
                    newCharacteristicValueId = getLastCharacteristicPossibleValueId(con, positon) + 1;

                    String sqlInsert = String.Format("INSERT INTO CharacteristicsValues (Id, CharacteristicsNameId, Value) VALUES ({0}, {1}, '{2}')", newCharacteristicValueId, positon.getId(), positonValue.getValue());
                    SqlCommand command = new SqlCommand(sqlInsert, con);
                    int numberOfinserted = command.ExecuteNonQuery();
                }

                // set inserted row Id to CharacteristicValue
                positonValue.setId(newCharacteristicValueId);
            }
        }

        private int searchCharacteristicPossibleValue(SqlConnection con, Position positon, PositionValue positonValue) 
        {
            String sqlSelect = String.Format("SELECT Id FROM CharacteristicsValues WHERE CharacteristicsNameId = {0} AND Value = '{1}'", positon.getId(), positonValue.getValue());
            SqlCommand command = new SqlCommand(sqlSelect, con);
            SqlDataReader reader = command.ExecuteReader();
            
            if (reader.HasRows) {
                return reader.GetInt32(0); //id
            }

    return -1; // not found in Db
        }

        private int getLastCharacteristicPossibleValueId(SqlConnection con, Position positon) 
        {
            String sqlSelect = String.Format("SELECT MAX(Id) AS MaxID FROM CharacteristicsValues WHERE CharacteristicsNameId = {0}", positon.getId());
            SqlCommand command = new SqlCommand(sqlSelect, con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return reader.GetInt32(0); //MaxID last possible values id
            }            

            return 0; // possible values not found in DB
        }


    }
}
