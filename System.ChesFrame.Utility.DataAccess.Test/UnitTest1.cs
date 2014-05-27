using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ChesFrame.Utility.DataAccess.DataConfig;
using System.Data.Common;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.Data;
using System.Transactions;

namespace System.ChesFrame.Utility.DataAccess.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 测试ExecuteScalar()
        {
            LoadConfig();
            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery4");
            var t = dbcommand.ExecuteScalar<int>();
        }

        [TestMethod]
        public void 测试ExecuteDataTable()
        {
            LoadConfig();
            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery4");
            DataTable dt = dbcommand.ExecuteDataTable();
        }

        [TestMethod]
        public void 测试存储过程()
        {
            LoadConfig();
            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery9");
            var p = dbcommand.ExecuteEntity<Person>();
        }


        [TestMethod]
        public void 测试ExecuteDataSet()
        {
            LoadConfig();
            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery4");
            DataSet ds = dbcommand.ExecuteDataSet();
        }

        [TestMethod]
        public void 测试ExecuteEntity()
        {
            LoadConfig();
            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery4");
            var p = dbcommand.ExecuteEntity<Person>();
        }

        [TestMethod]
        public void 无参数测试()
        {
            LoadConfig();

            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery4");
            var t= dbcommand.ExecuteEntityList<Person>();
        }

        [TestMethod]
        public void 带参数测试()
        {
            LoadConfig();

            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery5");
            new SqlBuilder(dbcommand).SetDbParameterValue("@name", "张三");
            var t= dbcommand.ExecuteEntityList<Person>();
        }

        [TestMethod]
        public void 带参多个数测试()
        {
            LoadConfig();

            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery6");
            new SqlBuilder(dbcommand).SetDbParameterValue("@name", "张三")
                .SetDbParameterValue("@age", 500);
            var t = dbcommand.ExecuteEntityList<Person>();
        }

        [TestMethod]
        public void 多条件测试()
        {
            LoadConfig();
            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery7");
            new SqlBuilder(dbcommand)
                .AddCondition(ConditionConnectionType.And, "name", OperationType.Equal, "@name", Data.DbType.String, 20, "张三")
                .AddCondition(ConditionConnectionType.And, "age", OperationType.Equal, "@age", Data.DbType.Int32, 4, 500)
                .MakeCondition4DbCommand();
             var t = dbcommand.ExecuteEntityList<Person>();
        }

        [TestMethod]
        public void 分页测试()
        {
            LoadConfig();

            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = 0,
                PageSze = 50,
                OrderBy = "ID Desc"
            };

            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery8");
            new SqlBuilder(dbcommand)
                .AddPageInfo(pageInfo);
            var t = dbcommand.ExecuteEntityList<Person>();
            var totalCount = dbcommand.GetTotalCount();

        }


        [TestMethod]
        public void 多次执行测试()
        {
           // LoadConfig();

            DbCommand dbcommand = DbCommandManager.GetDbCommand("PersonQuery4");
            var t = dbcommand.ExecuteEntityList<Person>();

            DbCommand dbcommand2 = DbCommandManager.GetDbCommand("PersonQuery4");
            var p = dbcommand2.ExecuteEntity<Person>();
        }

        private void LoadConfig()
        {
            var fileDirector = @"C:\Users\Administrator\Desktop\2014.05.26.DataAccess\System.ChesFrame.Utility\System.ChesFrame.Utility.DataAccess\DataConfigFile\Data";
            DbCommandManager dbCommndContainer = DbCommandManager.Instance;
            dbCommndContainer.Load(fileDirector);
        }



        #region  注释掉的内容
        //[TestMethod]
        //public void CreateConfigFiles()
        //{
        //    //ConnectionStringConfig config = new ConnectionStringConfig()
        //    //{
        //    //    ConnectionStrings = new ConnectionStrings()
        //    //    {
        //    //        ConnectionString = new Collections.Generic.List<ConnectionStringItem>(){
        //    //            new ConnectionStringItem(){
        //    //                Name = "Server1",
        //    //                Encrypted = false,
        //    //                DbProviderName = "System.Data.SqlClient",
        //    //                ConnectionString = "server=.;database=UserManager;uid=sa;pwd=zhangyao"
        //    //            }
        //    //         }
        //    //    }
        //    //};

        //    //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //    //ns.Add("", "");
        //    //XmlSerializer serializer = new XmlSerializer(typeof(ConnectionStringConfig));
        //    //XmlWriterSettings settings = new XmlWriterSettings();
        //    //settings.Encoding = Encoding.UTF8;

        //    //string strFile = "c:\\Config\\BbServer.config";
        //    //using (XmlWriter writer = XmlWriter.Create(strFile, settings))
        //    //{
        //    //    serializer.Serialize(writer, config, ns);
        //    //}

        //    //DBCommandFilesConfig filesConfig = new DBCommandFilesConfig()
        //    //{
        //    //    CommandFiles = new Collections.Generic.List<CommandFile>(){
        //    //        new CommandFile(){
        //    //            FilePath = "Data\\Person.Config"
        //    //        },
        //    //        new CommandFile(){
        //    //            FilePath = "Data\\Order.Config"
        //    //        }
        //    //     }
        //    //};


        //    //XmlSerializerNamespaces ns2 = new XmlSerializerNamespaces();
        //    //ns2.Add("", "");
        //    //XmlSerializer serializer2 = new XmlSerializer(typeof(DBCommandFilesConfig));
        //    //XmlWriterSettings settings2 = new XmlWriterSettings();
        //    //settings2.Encoding = Encoding.UTF8;

        //    //string strFile2 = "c:\\Config\\DbCommandFiles.config";
        //    //using (XmlWriter writer2 = XmlWriter.Create(strFile2, settings2))
        //    //{
        //    //    serializer2.Serialize(writer2, filesConfig, ns2);
        //    //}

        //    DBCommandFileConfig commandFileConfig = new DBCommandFileConfig()
        //    {
        //        DBCommands = new Collections.Generic.List<DBCommand>(){
        //             new DBCommand(){
        //                 CommandTimeout = 30,
        //                CommandText = "select * from Person",
        //                CommandType = "Text",
        //                Name = "PersonQuery",
        //                Parameters = new Collections.Generic.List<DBParameter>(){
        //                    new DBParameter(){
        //                        Name = "@name",
        //                        DbType = "String",
        //                        Size = 20
        //                    },
        //                    new DBParameter(){
        //                        Name = "@age",
        //                        DbType = "Int32",
        //                        Size = 20
        //                    }
        //                },
        //                ServerName = "Server1",
        //             },
        //             new DBCommand(){
        //                CommandText = "select * from Person where ID = 1",
        //                CommandType = "Text",
        //                Name = "PersonQuery2",
        //                Parameters = null,
        //                ServerName = "Server1"
        //             },
        //             new DBCommand(){
        //                CommandText = "select * from Person where name=@name",
        //                CommandType = "Text",
        //                Name = "PersonQuery3",
        //                Parameters = new Collections.Generic.List<DBParameter>(){
        //                    new DBParameter(){
        //                        Name = "@name",
        //                        DbType = "String",
        //                        Size = 20
        //                    }
        //                },
        //                ServerName = "Server1"
        //             },
        //              new DBCommand(){
        //                CommandText = "select * from Person #Where#",
        //                CommandType = "Text",
        //                Name = "PersonQuery4",
        //                Parameters = null,
        //                ServerName = "Server1"
        //             }
        //        }
        //    };

        //    XmlSerializerNamespaces ns3 = new XmlSerializerNamespaces();
        //    ns3.Add("", "");
        //    XmlSerializer serializer3 = new XmlSerializer(typeof(DBCommandFileConfig));
        //    XmlWriterSettings settings3= new XmlWriterSettings();
        //    settings3.Encoding = Encoding.UTF8;

        //    string strFile3 = "c:\\Config\\Data\\Person.config";
        //    using (XmlWriter writer3 = XmlWriter.Create(strFile3, settings3))
        //    {
        //        serializer3.Serialize(writer3, commandFileConfig, ns3);
        //    }


        //}

        //[TestMethod]
        //public void SqlBuilder()
        //{
        //    DbCommandContainer dbCommndContainer = DbCommandContainer.Instance;
        //    dbCommndContainer.Load("c:\\Config");
        //    DbCommand dbcommand = dbCommndContainer.GetDbCommandByName("PersonQuery4");

        //    SqlBuilder builder = new DataAccess.SqlBuilder(dbcommand);
        //    builder.AddCondition(ConditionConnectionType.And,"name", OperationType.Equal, "@name", Data.DbType.String, 20, "张三");



        //    //builder.AddCondition(new SqlConditionSet(){ConditionConnectionType = ConditionConnectionType.And , 
        //    //    SqlConditionList = new Collections.Generic.List<SqlCondition>(){
        //    //        new SqlCondition(){

        //    //        }
        //    //    }})


        //    dbcommand.Parameters["@name"].Value = "张三";
        //    // dbcommand.Parameters["@age"].Value = 20;
        //    var p = dbcommand.ExecuteEntityList<Person>();

        //}

        #endregion

    }
}
