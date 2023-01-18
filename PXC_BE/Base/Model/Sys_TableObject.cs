using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class Sys_TableObject : ObjectInfoBase
    {
        public Sys_TableObject() : base()
        {
            this.TABLE_NAME = String.Empty;
            this.ID_FIELD = string.Empty;
            this.NAME_FIELD = string.Empty;
            this.TABLE_TYPE = string.Empty;
            this.CAN_GROUP = 0;
            this.ID_AUTO = 0;
            this.ID_MASK = string.Empty;
            this.ID_LENGTH = 0;
            this.ID_PARTS = 0;
            this.ID_SPLIT = string.Empty;
            this.LOAD_BY_SEARCH = 0;
        }

        public Sys_TableObject(DataRow dr) : base()
        {
            this.TABLE_NAME = (string)dr["TABLE_NAME"];
            this.ID_FIELD = (string)dr["ID_FIELD"];
            this.NAME_FIELD = (string)dr["NAME_FIELD"];
            this.TABLE_TYPE = (string)dr["TABLE_TYPE"];
            this.CAN_GROUP = (Int16)dr["CAN_GROUP"];
            this.ID_AUTO = (Int16)dr["ID_AUTO"];
            this.ID_MASK = (string)dr["ID_MASK"];
            this.ID_LENGTH = (int)dr["ID_LENGTH"];
            this.ID_PARTS = (int)dr["ID_PARTS"];
            this.ID_SPLIT = (string)dr["ID_SPLIT"];
            this.LOAD_BY_SEARCH = (Int16)dr["LOAD_BY_SEARCH"];
        }

        public string TABLE_NAME
        {
            get { return this.GetValueOrDefault<string>("TABLE_NAME"); }
            set { this.SetValue("TABLE_NAME", value); }
        }

        public string ID_FIELD
        {
            get { return this.GetValueOrDefault<string>("ID_FIELD"); }
            set { this.SetValue("ID_FIELD", value); }
        }

        public string NAME_FIELD
        {
            get { return this.GetValueOrDefault<string>("NAME_FIELD"); }
            set { this.SetValue("NAME_FIELD", value); }
        }

        public string TABLE_TYPE
        {
            get { return this.GetValueOrDefault<string>("TABLE_TYPE"); }
            set { this.SetValue("TABLE_TYPE", value); }
        }

        public Int16 CAN_GROUP
        {
            get { return this.GetValueOrDefault<Int16>("CAN_GROUP"); }
            set { this.SetValue("CAN_GROUP", value); }
        }

        public Int16 ID_AUTO
        {
            get { return this.GetValueOrDefault<Int16>("ID_AUTO"); }
            set { this.SetValue("ID_AUTO", value); }
        }

        public string ID_MASK
        {
            get { return this.GetValueOrDefault<string>("ID_MASK"); }
            set { this.SetValue("ID_MASK", value); }
        }

        public int ID_LENGTH
        {
            get { return this.GetValueOrDefault<int>("ID_LENGTH"); }
            set { this.SetValue("ID_LENGTH", value); }
        }

        public int ID_PARTS
        {
            get { return this.GetValueOrDefault<int>("ID_PARTS"); }
            set { this.SetValue("ID_PARTS", value); }
        }

        public string ID_SPLIT
        {
            get { return this.GetValueOrDefault<string>("ID_SPLIT"); }
            set { this.SetValue("ID_SPLIT", value); }
        }

        public Int16 LOAD_BY_SEARCH
        {
            get { return this.GetValueOrDefault<Int16>("LOAD_BY_SEARCH"); }
            set { this.SetValue("LOAD_BY_SEARCH", value); }
        }
    }
}