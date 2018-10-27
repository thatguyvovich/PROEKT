using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.dao.ado.connectors;
using CyberPushkin.model;
using System.Data.SqlClient;

namespace CyberPushkin.dao.ado
{
    public class ADOVerseDAO : VerseDAO
    {
        private ADOSQLiteConnector connector;

        public ADOVerseDAO(ADOSQLiteConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentException();
            }

            this.connector = connector;
        }

        
  public List<Verse> getAll()
        {
            List<Verse> verse = new List<Verse>();

            try {
                SqlConnection con = connector.getConnection();
                String sqlSelect = "SELECT Id, Text FROM ExpandableTexts";
                SqlCommand command = new SqlCommand(sqlSelect, con);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        verse.Add(new Verse(reader.GetString(1), getCharacteristicsValues(con, reader.GetInt32(1))));
                    }

                }
                
            } catch (SqlException ignored)
            {
            }

            return verse;
        }

        
  public void addAll(List<Verse> verse)  {
    if (verse == null ||
        verse.Count == 0) {
      throw new EmptyRecordException("Expandable texts is null or empty");
    }

    try
    {
      SqlConnection con = connector.getConnection();
                //con.BeginTransaction();

                // prepare sql query
                //

                String sqlInsert;

      //

      foreach (Verse expandableText in verse) {
        // check
        //

        if (expandableText == null ||
            expandableText.getText().Equals("") ||
            expandableText.getCharacteristics() == null ||
            expandableText.getCharacteristics().Count == 0) {
          throw new EmptyRecordException("Expandable text is null or empty");
        }

        if (!fillCharacteristicNamesAndValuesIDs(con, expandableText)) {
          throw new NotExistsException("Characteristic value not exists");
        }

                    // insert
                    //

        sqlInsert = String.Format( "INSERT INTO ExpandableTexts (Text) VALUES ({'0'})", expandableText.getText());
        SqlCommand command = new SqlCommand(sqlInsert, con);
        int numberOfinserted = command.ExecuteNonQuery();

                    if (numberOfinserted>0) {
          // save all positon to DB
          //

          foreach (KeyValuePair<Position, PositionValue> entry in expandableText.getCharacteristics()) {
            insertToExpandableTextsCharacteristicsTable(con, numberOfinserted, entry.Key, entry.Value);
          }
        }
      }

      //con.Commit();
      //con.setAutoCommit(true);
    } catch (SqlException ignored) {
    }
  }

  private Dictionary<Position, PositionValue> getCharacteristicsValues(SqlConnection con, int expandableTextId) 
{
    Dictionary<Position, PositionValue> positon = new Dictionary<Position, PositionValue>();

    String sqlSelect = String.Format("SELECT CharacteristicsNames.Id AS CharacteristicId, " +
        "CharacteristicsNames.Name AS CharacteristicName, " +
        "CharacteristicsValues.Id AS CharacteristicValueId, " +
        "CharacteristicsValues.Value AS CharacteristicValue " +
        "FROM ExpandableTextsCharacteristics " +
        "LEFT JOIN CharacteristicsNames " +
        "ON ExpandableTextsCharacteristics.CharacteristicsNameId = CharacteristicsNames.Id " +
        "LEFT JOIN CharacteristicsValues " +
        "ON ExpandableTextsCharacteristics.CharacteristicsValueId = CharacteristicsValues.Id " +
        "AND ExpandableTextsCharacteristics.CharacteristicsNameId = CharacteristicsValues.CharacteristicsNameId " +
        "WHERE ExpandableTextsCharacteristics.ExpandableTextId = {0}", expandableTextId);

            SqlCommand command = new SqlCommand(sqlSelect, con);
            SqlDataReader reader = command.ExecuteReader();


            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    Position characteristic = new Position(reader.GetInt32(0), reader.GetString(1));
                    PositionValue characteristicValue = new PositionValue(reader.GetInt32(2), reader.GetString(3));
                    positon[characteristic] = characteristicValue;
                }
            }

    return positon;
  }

  private bool fillCharacteristicNamesAndValuesIDs(SqlConnection con, Verse expandableText) 
{
    String sqlSelect; 

    foreach (KeyValuePair<Position, PositionValue> entry in expandableText.getCharacteristics()) {
        sqlSelect = String.Format( "SELECT CharacteristicsNames.Id AS CharacteristicId, " +
            "CharacteristicsValues.Id AS CharacteristicValueId " +
            "FROM CharacteristicsValues JOIN CharacteristicsNames " +
            "ON CharacteristicsValues.CharacteristicsNameId = CharacteristicsNames.Id " +
            "WHERE CharacteristicsNames.Name = {0} AND CharacteristicsValues.Value = {1}", entry.Key.getName(), entry.Value.getValue());
                SqlCommand command = new SqlCommand(sqlSelect, con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        entry.Key.setId(reader.GetInt32(0));
                        entry.Value.setId(reader.GetInt32(1));
                    }
                }
                else
                {
                    return false;
                }
    }

    return true;
}

private void insertToExpandableTextsCharacteristicsTable(SqlConnection con, int expandableTextId, Position characteristic, PositionValue characteristicValue) 
{
    String sqlInsert = String.Format("INSERT INTO ExpandableTextsCharacteristics (ExpandableTextId, CharacteristicsNameId, CharacteristicsValueId) VALUES ({}, {}, {})", expandableTextId, characteristic.getId(), characteristicValue.getId());
    SqlCommand command = new SqlCommand(sqlInsert, con);
    int numberOfinserted = command.ExecuteNonQuery();
        }
    }
}
