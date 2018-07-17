using System;
using System.Data;

namespace BAL.DataTables
{
    public class Tables
    {
        private DataTable _customerDataTable = new DataTable("Customer");
        private DataTable _apppDataTable = new DataTable("ApppTpr");
        private DataTable _genderDataTable = new DataTable("Gender");

        private DataTable _adminDivisionDataTable = new DataTable("AdminDivision");
        private DataTable _typeStreetDataTable = new DataTable("TypeStreet");
        private DataTable _addressDataTable = new DataTable("Address");

        private DataTable _registerDataTable = new DataTable("Register");
        private DataTable _landDataTable = new DataTable("Land");
        private DataTable _registerTypeDataTable = new DataTable("RegisterType");
        private DataTable _whyDeRegisterDataTable = new DataTable("WhyDeRegister");

        private DataTable _invalidDataTable = new DataTable("Invalid");
        private DataTable _chiperReceptDataTable = new DataTable("ChiperRecept");
        private DataTable _disabilityGroupDataTable = new DataTable("DisabilityGroup");
        private DataTable _benefitsDataTable = new DataTable("BenefitsCategory");
        private DataTable _invalidBenefitsDataTable = new DataTable("InvalidBenefitsCategory");
        private DataTable _errorDataTable = new DataTable("Error");

        private DataSet _dispancerDataSet = new DataSet("DispensaryCustomerRegister");

        #region Properties

        public DataTable CustomerDataTable
        {
            get { return _customerDataTable; }

            set { _customerDataTable = value; }
        }

        public DataSet DispancerDataSet
        {
            get { return _dispancerDataSet; }
            set { _dispancerDataSet = value; }
        }

        public DataTable ApppDataTable
        {
            get { return _apppDataTable; }
            set { _apppDataTable = value; }
        }

        public DataTable GenderDataTable
        {
            get { return _genderDataTable; }
        }

        public DataTable AddressDataTable
        {
            get { return _addressDataTable; }
            set { _addressDataTable = value; }
        }

        public DataTable AdminDivisionDataTable
        {
            get { return _adminDivisionDataTable; }
            set { _adminDivisionDataTable = value; }
        }

        public DataTable TypeStreetDataTable
        {
            get { return _typeStreetDataTable; }
            set { _typeStreetDataTable = value; }
        }

        public DataTable RegisterDataTable
        {
            get { return _registerDataTable; }
            set { _registerDataTable = value; }
        }

        public DataTable LandDataTable
        {
            get { return _landDataTable; }
            set { _landDataTable = value; }
        }

        public DataTable RegisterTypeDataTable
        {
            get { return _registerTypeDataTable; }
            set { _registerTypeDataTable = value; }
        }

        public DataTable WhyDeRegisterDataTable
        {
            get { return _whyDeRegisterDataTable; }
            set { _whyDeRegisterDataTable = value; }
        }
        public DataTable InvalidDataTable
        {
            get { return _invalidDataTable; }
            set { _invalidDataTable = value; }
        }

        public DataTable ChiperReceptDataTable
        {
            get { return _chiperReceptDataTable; }
            set { _chiperReceptDataTable = value; }
        }

        public DataTable DisabilityGroupDataTable
        {
            get { return _disabilityGroupDataTable; }
            set { _disabilityGroupDataTable = value; }
        }

        public DataTable BenefitsDataTable
        {
            get { return _benefitsDataTable; }
            set { _benefitsDataTable = value; }
        }

        public DataTable InvalidBenefitsDataTable
        {
            get { return _invalidBenefitsDataTable; }
            set { _invalidBenefitsDataTable = value; }
        }

        public DataTable ErrorDataTable
        {
            get { return _errorDataTable; }
            set { _errorDataTable = value; }
        }

        #endregion
        public Tables()
        {
            InitTables();
        }

        private void InitTables()
        {
            #region Gender

            DataColumn genderColumn = new DataColumn("GenderID");
            genderColumn.ReadOnly = true;
            genderColumn.AllowDBNull = false;
            genderColumn.DataType = typeof(System.Int32);
            genderColumn.Caption = "№";
            GenderDataTable.Columns.Add(genderColumn);
            GenderDataTable.PrimaryKey = new DataColumn[1] { genderColumn };

            genderColumn = new DataColumn("Name");
            genderColumn.AllowDBNull = false;
            genderColumn.DataType = typeof(System.String);
            genderColumn.MaxLength = 1;
            genderColumn.Caption = "Пол";
            GenderDataTable.Columns.Add(genderColumn);
            DispancerDataSet.Tables.Add(GenderDataTable);

            #endregion Gender

            #region APPP Date Table

            DataColumn apppColumn = new DataColumn("APPPTPRID");
            apppColumn.ReadOnly = true;
            apppColumn.AllowDBNull = false;
            apppColumn.AutoIncrement = true;
            apppColumn.AutoIncrementSeed = -1;
            apppColumn.AutoIncrementStep = -1;
            apppColumn.Caption = "№";
            apppColumn.DataType = typeof(System.Int32);
            ApppDataTable.Columns.Add(apppColumn);
            ApppDataTable.PrimaryKey = new[] { apppColumn };

            apppColumn = new DataColumn("Name");
            apppColumn.ReadOnly = false;
            apppColumn.AllowDBNull = false;
            apppColumn.DataType = typeof(string);
            apppColumn.MaxLength = 5;
            apppColumn.Caption = "АППП/ТПР";
            ApppDataTable.Columns.Add(apppColumn);

            DispancerDataSet.Tables.Add(ApppDataTable);

            #endregion APPP Date Table


            #region CustomerTable

            DataColumn customerID = new DataColumn("CustomerID");
            customerID.AllowDBNull = false;
            customerID.AutoIncrement = true;
            customerID.AutoIncrementSeed = -1;
            customerID.AutoIncrementStep = -1;
            customerID.Unique = true;
            customerID.ReadOnly = false;
            customerID.DataType = typeof(System.Int32);
            customerID.Caption = "№ пациента";
            CustomerDataTable.Columns.Add(customerID);

            DataColumn medCard = new DataColumn("MedCard");
            medCard.AllowDBNull = true;
            medCard.ReadOnly = false;
            medCard.DataType = typeof(System.Int32);
            medCard.Caption = "№ медкарты";
            CustomerDataTable.Columns.Add(medCard);

            DataColumn cardCustomer = new DataColumn("CodeCustomer");
            cardCustomer.AllowDBNull = true;
            cardCustomer.ReadOnly = false;
            cardCustomer.DataType = typeof(Int32);
            cardCustomer.Caption = "Код пациента";
            CustomerDataTable.Columns.Add(cardCustomer);

            DataColumn lastName = new DataColumn("LastName");
            lastName.AllowDBNull = false;
            lastName.ReadOnly = false;
            lastName.DataType = typeof(String);
            lastName.MaxLength = 100;
            lastName.Caption = "Фамилия";
            CustomerDataTable.Columns.Add(lastName);

            DataColumn firstName = new DataColumn("FirstName");
            firstName.AllowDBNull = false;
            firstName.ReadOnly = false;
            firstName.DataType = typeof(string);
            firstName.MaxLength = 100;
            firstName.Caption = "Имя";
            CustomerDataTable.Columns.Add(firstName);

            DataColumn middleName = new DataColumn("MiddleName");
            middleName.AllowDBNull = true;
            middleName.ReadOnly = false;
            middleName.DataType = typeof(string);
            middleName.MaxLength = 100;
            middleName.Caption = "Отчество";
            CustomerDataTable.Columns.Add(middleName);

            DataColumn birthDay = new DataColumn("Birthday");
            birthDay.AllowDBNull = true;
            birthDay.ReadOnly = false;
            birthDay.DataType = typeof(DateTime);
            birthDay.Caption = "День рождения";
            CustomerDataTable.Columns.Add(birthDay);


            DataColumn age = new DataColumn("Arch");
            age.AllowDBNull = false;
            age.ReadOnly = false;
            age.DataType = typeof(bool);
            age.DefaultValue = false;
            age.Caption = "Архив";
            CustomerDataTable.Columns.Add(age);

            DataColumn apppTprID = new DataColumn("APPPTPRID");
            apppTprID.ReadOnly = false;
            apppTprID.AllowDBNull = true;
            apppTprID.DataType = typeof(System.Int32);
            apppTprID.Caption = "АППП/ТПР";
            CustomerDataTable.Columns.Add(apppTprID);

            apppTprID = new DataColumn("GenderID");
            apppTprID.ReadOnly = false;
            apppTprID.AllowDBNull = true;
            apppTprID.DataType = typeof(System.Int32);
            apppTprID.Caption = "Пол";
            CustomerDataTable.Columns.Add(apppTprID);

            DataColumn notaBene = new DataColumn("NotaBene");
            notaBene.AllowDBNull = true;
            notaBene.DataType = typeof(System.String);
            notaBene.Caption = "Доп. сведения";
            CustomerDataTable.Columns.Add(notaBene);

            CustomerDataTable.PrimaryKey = new[] { customerID };
            DispancerDataSet.Tables.Add(_customerDataTable);
            ForeignKeyConstraint fkc = new ForeignKeyConstraint("FK_Customer_APPPTPR_APPPTPRID",
            ApppDataTable.Columns[0],
                CustomerDataTable.Columns["APPPTPRID"]);
            CustomerDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            DataRelation relation = new DataRelation("FK_Customer_APPPTPR_APPPTPRID", ApppDataTable.Columns[0],
            CustomerDataTable.Columns["APPPTPRID"], true);
            DispancerDataSet.Relations.Add(relation);
            relation.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
            relation.ChildKeyConstraint.DeleteRule = Rule.SetNull;
            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            fkc = new ForeignKeyConstraint("FK_Customer_Gender_GenderID", GenderDataTable.Columns[0],
                CustomerDataTable.Columns["GenderID"]);
            CustomerDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.SetDefault;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Customer_Gender_GenderID", GenderDataTable.Columns[0],
                CustomerDataTable.Columns["GenderID"], true);
            DispancerDataSet.Relations.Add(relation);
            relation.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
            relation.ChildKeyConstraint.DeleteRule = Rule.SetDefault;
            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            DataColumn errorID = new DataColumn("CustomerID");
            customerID.AllowDBNull = false;
            //customerID.AutoIncrement = true;
            //customerID.AutoIncrementSeed = -1;
            //customerID.AutoIncrementStep = -1;
            errorID.Unique = true;
            errorID.ReadOnly = false;
            errorID.DataType = typeof(System.Int32);
            errorID.Caption = "CustomerID";
            ErrorDataTable.Columns.Add(errorID);

            DataColumn flmColumn = new DataColumn("FML");
            flmColumn.AllowDBNull = true;
            flmColumn.DataType = typeof(System.String);
            flmColumn.Caption = "ФИО";
            ErrorDataTable.Columns.Add(flmColumn);            

            DataColumn errorColumn = new DataColumn("Error");
            errorColumn.AllowDBNull = true;
            errorColumn.DataType = typeof(System.String);
            errorColumn.Caption = "Ошибка";
            ErrorDataTable.Columns.Add(errorColumn);
            DispancerDataSet.Tables.Add(ErrorDataTable);

            fkc = new ForeignKeyConstraint("FK_Error_Customer_CustomerID", CustomerDataTable.Columns[0],
               ErrorDataTable.Columns["CustomerID"]);
            ErrorDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Error_Customer_CustomerID", CustomerDataTable.Columns[0],
               ErrorDataTable.Columns["CustomerID"], true);
            DispancerDataSet.Relations.Add(relation);
            relation.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
            relation.ChildKeyConstraint.DeleteRule = Rule.Cascade;
            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            #endregion CustomerTable

            #region AdminDivision Table

            DataColumn adminDivisionColumn = new DataColumn("AdminDivisionID");
            adminDivisionColumn.ReadOnly = true;
            adminDivisionColumn.AllowDBNull = false;
            adminDivisionColumn.AutoIncrementSeed = -1;
            adminDivisionColumn.AutoIncrementStep = -1;
            adminDivisionColumn.AutoIncrement = true;
            adminDivisionColumn.DataType = typeof(Int32);
            adminDivisionColumn.Caption = "№";
            AdminDivisionDataTable.Columns.Add(adminDivisionColumn);
            AdminDivisionDataTable.PrimaryKey = new[] { adminDivisionColumn };

            adminDivisionColumn = new DataColumn("Name");
            adminDivisionColumn.ReadOnly = false;
            adminDivisionColumn.AllowDBNull = false;
            adminDivisionColumn.DataType = typeof(string);
            adminDivisionColumn.MaxLength = 30;
            adminDivisionColumn.Caption = "Полное наименование";
            AdminDivisionDataTable.Columns.Add(adminDivisionColumn);

            adminDivisionColumn = new DataColumn("SocrName");
            adminDivisionColumn.ReadOnly = false;
            adminDivisionColumn.AllowDBNull = false;
            adminDivisionColumn.DataType = typeof(string);
            adminDivisionColumn.MaxLength = 10;
            adminDivisionColumn.Caption = "Сокращенное наименование";
            AdminDivisionDataTable.Columns.Add(adminDivisionColumn);

            DispancerDataSet.Tables.Add(AdminDivisionDataTable);

            #endregion AdminDivision Table

            #region TypeStreet Table

            DataColumn typeStreetColumn = new DataColumn("TypeStreetID");
            typeStreetColumn.ReadOnly = true;
            typeStreetColumn.AllowDBNull = false;
            typeStreetColumn.AutoIncrementSeed = -1;
            typeStreetColumn.AutoIncrementStep = -1;
            typeStreetColumn.AutoIncrement = true;
            typeStreetColumn.DataType = typeof(Int32);
            TypeStreetDataTable.Columns.Add(typeStreetColumn);
            TypeStreetDataTable.PrimaryKey = new[] { typeStreetColumn };

            typeStreetColumn = new DataColumn("Name");
            typeStreetColumn.ReadOnly = false;
            typeStreetColumn.AllowDBNull = false;
            typeStreetColumn.DataType = typeof(string);
            typeStreetColumn.MaxLength = 30;
            typeStreetColumn.Caption = "Наименование";
            TypeStreetDataTable.Columns.Add(typeStreetColumn);

            typeStreetColumn = new DataColumn("SocrName");
            typeStreetColumn.ReadOnly = false;
            typeStreetColumn.AllowDBNull = false;
            typeStreetColumn.DataType = typeof(string);
            typeStreetColumn.MaxLength = 10;
            typeStreetColumn.Caption = "Сокращенное наименование";
            TypeStreetDataTable.Columns.Add(typeStreetColumn);

            DispancerDataSet.Tables.Add(TypeStreetDataTable);

            #endregion TypeStreet Table

            #region AddressTable

            DataColumn addressColumn = new DataColumn("AddressID");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = false;
            addressColumn.AutoIncrement = true;
            addressColumn.AutoIncrementSeed = -1;
            addressColumn.AutoIncrementStep = -1;
            addressColumn.DataType = typeof(Int32);
            AddressDataTable.Columns.Add(addressColumn);
            AddressDataTable.PrimaryKey = new DataColumn[1] { addressColumn };

            addressColumn = new DataColumn("Region");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(string);
            addressColumn.MaxLength = 50;
            addressColumn.Caption = "Регион";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("Contry");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(string);
            addressColumn.MaxLength = 50;
            addressColumn.Caption = "Страна";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("City");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(string);
            addressColumn.MaxLength = 100;
            addressColumn.Caption = "Населённый пункт";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("AdminDivisionID");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = false;
            addressColumn.DataType = typeof(Int32);
            addressColumn.Caption = "Тип НП";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("TypeStreetID");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(Int32);
            addressColumn.Caption = "Тип улицы";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("NameStreet");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(string);
            addressColumn.MaxLength = 100;
            addressColumn.Caption = "Название улицы";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("NumberHouse");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(string);
            addressColumn.MaxLength = 10;
            addressColumn.Caption = "№ Дома";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("NumberApartment");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = true;
            addressColumn.DataType = typeof(string);
            addressColumn.MaxLength = 10;
            addressColumn.Caption = "№ квартиры";
            AddressDataTable.Columns.Add(addressColumn);

            addressColumn = new DataColumn("CustomerID");
            addressColumn.ReadOnly = false;
            addressColumn.AllowDBNull = false;
            addressColumn.DataType = typeof(Int32);
            AddressDataTable.Columns.Add(addressColumn);

            this.DispancerDataSet.Tables.Add(AddressDataTable);

            fkc = new ForeignKeyConstraint("FK_Address_Customer_CustomerID",
                CustomerDataTable.Columns[0], AddressDataTable.Columns["CustomerID"]);
            AddressDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Address_Customer_CustomerID", CustomerDataTable.Columns[0],
                AddressDataTable.Columns["CustomerID"], true);
            this.DispancerDataSet.Relations.Add(relation);
            relation.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
            relation.ChildKeyConstraint.DeleteRule = Rule.Cascade;
            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            fkc = new ForeignKeyConstraint("FK_Address_AdminDivision_AdminDivisionID",
                AdminDivisionDataTable.Columns[0], AddressDataTable.Columns["AdminDivisionID"]);
            AddressDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Address_AdminDivision_AdminDivisionID",
                AdminDivisionDataTable.Columns[0], AddressDataTable.Columns["AdminDivisionID"], true);
            this.DispancerDataSet.Relations.Add(relation);
            relation.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
            relation.ChildKeyConstraint.DeleteRule = Rule.Cascade;
            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            fkc = new ForeignKeyConstraint("FK_Address_TypeStreet_TypeStreetID",
                TypeStreetDataTable.Columns[0], AddressDataTable.Columns["TypeStreetID"]);
            AddressDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Address_TypeStreet_TypeStreetID",
                TypeStreetDataTable.Columns[0], AddressDataTable.Columns["TypeStreetID"], true);
            this.DispancerDataSet.Relations.Add(relation);
            relation.ChildKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
            relation.ChildKeyConstraint.DeleteRule = Rule.SetNull;
            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

            #endregion AddressTable

            #region Register Tables
            #region RegisterTable

            DataColumn regColumn = new DataColumn("RegisterID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = false;
            regColumn.AutoIncrement = true;
            regColumn.AutoIncrementSeed = -1;
            regColumn.AutoIncrementStep = -1;
            regColumn.DataType = typeof(Int32);
            regColumn.Caption = "№";
            RegisterDataTable.Columns.Add(regColumn);
            RegisterDataTable.PrimaryKey = new DataColumn[1] { regColumn };

            regColumn = new DataColumn("FirstRegister");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(DateTime);
            regColumn.Caption = "1-й раз взят на регистрацию";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("FirstDeregister");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(DateTime);
            regColumn.Caption = "1-й раз снят с регистрации";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("SecondRegister");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(DateTime);
            regColumn.Caption = "Повторно взят на регистрацию";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("SecondDeRegister");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(DateTime);
            regColumn.Caption = "Повторно снят с регистрации";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("Diagnosis");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(string);
            regColumn.MaxLength = 10;
            regColumn.Caption = "Диагноз";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("DataDiagnosis");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(DateTime);
            regColumn.Caption = "Дата диагноза";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("RegisterTypeID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(Int32);
            regColumn.Caption = "Причина взятия на учёт";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("CustomerID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = false;
            regColumn.DataType = typeof(Int32);
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("WhyDeRegisterID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(Int32);
            regColumn.Caption = "Причина снятия с учёта";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("WhySecondDeRegisterID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(Int32);
            regColumn.Caption = "Причина повторного снятия с учёт";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("SecondRegisterTypeID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(Int32);
            regColumn.Caption = "Причина повторного взятия на учёт";
            RegisterDataTable.Columns.Add(regColumn);

            regColumn = new DataColumn("LandID");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = false;
            regColumn.Caption = "№ участка";
            regColumn.DataType = typeof(Int32);
            RegisterDataTable.Columns.Add(regColumn);


            regColumn = new DataColumn("NotaBene");
            regColumn.ReadOnly = false;
            regColumn.AllowDBNull = true;
            regColumn.DataType = typeof(string);
            regColumn.MaxLength = 2000;
            regColumn.Caption = "Дополнительно";
            RegisterDataTable.Columns.Add(regColumn);

            DispancerDataSet.Tables.Add(RegisterDataTable);

            fkc = new ForeignKeyConstraint("FK_Register_Customer_CustomerID",
                CustomerDataTable.Columns[0], RegisterDataTable.Columns["CustomerID"]);
            RegisterDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Register_Customer_CustomerID", CustomerDataTable.Columns[0],
                RegisterDataTable.Columns["CustomerID"], false);
            this.DispancerDataSet.Relations.Add(relation);

            #endregion RegisterTable
            #region Land Table

            DataColumn landColumn = new DataColumn("LandID");
            landColumn.ReadOnly = true;
            landColumn.AllowDBNull = false;
            landColumn.AutoIncrementSeed = -1;
            landColumn.AutoIncrementStep = -1;
            landColumn.AutoIncrement = true;
            landColumn.DataType = typeof(Int32);
            LandDataTable.Columns.Add(landColumn);
            LandDataTable.PrimaryKey = new[] { landColumn };

            landColumn = new DataColumn("Name");
            landColumn.ReadOnly = false;
            landColumn.AllowDBNull = false;
            landColumn.DataType = typeof(string);
            landColumn.Caption = "№ участка";
            landColumn.MaxLength = 50;
            LandDataTable.Columns.Add(landColumn);

            this.DispancerDataSet.Tables.Add(_landDataTable);

            fkc = new ForeignKeyConstraint("FK_Register_Land_LandID", LandDataTable.Columns[0],
                RegisterDataTable.Columns["LandID"]);
            RegisterDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.SetDefault;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Register_Land_LandID", LandDataTable.Columns[0],
                RegisterDataTable.Columns["LandID"], false);
            DispancerDataSet.Relations.Add(relation);

            #endregion Land Table
            #region RegisterType Table

            DataColumn regTypeColumn = new DataColumn("RegisterTypeID");
            regTypeColumn.ReadOnly = false;
            regTypeColumn.AllowDBNull = false;
            regTypeColumn.AutoIncrementSeed = -1;
            regTypeColumn.AutoIncrementStep = -1;
            regTypeColumn.AutoIncrement = true;
            regTypeColumn.DataType = typeof(Int32);
            RegisterTypeDataTable.Columns.Add(regTypeColumn);
            RegisterTypeDataTable.PrimaryKey = new[] { regTypeColumn };

            regTypeColumn = new DataColumn("Name");
            regTypeColumn.ReadOnly = false;
            regTypeColumn.AllowDBNull = false;
            regTypeColumn.DataType = typeof(string);
            regTypeColumn.MaxLength = 50;
            regTypeColumn.Caption = "Тип регистации";
            RegisterTypeDataTable.Columns.Add(regTypeColumn);

            regTypeColumn = new DataColumn("NotaBene");
            regTypeColumn.ReadOnly = false;
            regTypeColumn.AllowDBNull = true;
            regTypeColumn.DataType = typeof(string);
            regTypeColumn.MaxLength = 2000;
            RegisterTypeDataTable.Columns.Add(regTypeColumn);

            this.DispancerDataSet.Tables.Add(RegisterTypeDataTable);
            fkc = new ForeignKeyConstraint("FK_Register_RegisterType_RegisterTypeID", RegisterTypeDataTable.Columns[0],
                RegisterDataTable.Columns["RegisterTypeID"]);
            RegisterDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.Cascade;
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Register_RegisterType_RegisterTypeID", RegisterTypeDataTable.Columns[0],
                RegisterDataTable.Columns["RegisterTypeID"], false);
            DispancerDataSet.Relations.Add(relation);

            //fkc = new ForeignKeyConstraint("FK_Register_RegisterType_SecondRegisterTypeID", RegisterTypeDataTable.Columns[0],
            //    RegisterDataTable.Columns["SecondRegisterTypeID"]);
            //RegisterDataTable.Constraints.Add(fkc);
            //fkc.AcceptRejectRule = AcceptRejectRule.None;
            //fkc.DeleteRule = Rule.None;
            //fkc.UpdateRule = Rule.None;

            relation = new DataRelation("FK_Register_RegisterType_SecondRegisterTypeID", RegisterTypeDataTable.Columns[0],
                RegisterDataTable.Columns["SecondRegisterTypeID"], true);
            DispancerDataSet.Relations.Add(relation);
            #endregion RegisterType Table

            #region WhyDeRegister Table

            DataColumn whyDeRegColumn = new DataColumn("WhyDeRegisterID");
            whyDeRegColumn.ReadOnly = true;
            whyDeRegColumn.AllowDBNull = false;
            whyDeRegColumn.AutoIncrementSeed = -1;
            whyDeRegColumn.AutoIncrementStep = -1;
            whyDeRegColumn.AutoIncrement = true;
            whyDeRegColumn.DataType = typeof(Int32);
            WhyDeRegisterDataTable.Columns.Add(whyDeRegColumn);
            WhyDeRegisterDataTable.PrimaryKey = new[] { whyDeRegColumn };

            whyDeRegColumn = new DataColumn("Name");
            whyDeRegColumn.ReadOnly = false;
            whyDeRegColumn.AllowDBNull = false;
            whyDeRegColumn.DataType = typeof(string);
            whyDeRegColumn.MaxLength = 50;
            whyDeRegColumn.Caption = "Причина снятия с учёта";
            WhyDeRegisterDataTable.Columns.Add(whyDeRegColumn);

            whyDeRegColumn = new DataColumn("NotaBene");
            whyDeRegColumn.ReadOnly = false;
            whyDeRegColumn.AllowDBNull = true;
            whyDeRegColumn.DataType = typeof(string);
            whyDeRegColumn.MaxLength = 2000;
            WhyDeRegisterDataTable.Columns.Add(whyDeRegColumn);

            this.DispancerDataSet.Tables.Add(WhyDeRegisterDataTable);
            fkc = new ForeignKeyConstraint("FK_Register_WhyDeRegister_WhyDeRegisterID",
                WhyDeRegisterDataTable.Columns[0], RegisterDataTable.Columns["WhyDeRegisterID"]);
            RegisterDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.Cascade;
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Register_WhyDeRegister_WhyDeRegisterID", WhyDeRegisterDataTable.Columns[0],
                RegisterDataTable.Columns["WhyDeRegisterID"], false);
            DispancerDataSet.Relations.Add(relation);


            //fkc = new ForeignKeyConstraint("FK_Register_WhyDeRegister_WhySecondDeRegisterID",
            //    WhyDeRegisterDataTable.Columns[0], RegisterDataTable.Columns["WhySecondDeRegisterID"]);
            //RegisterDataTable.Constraints.Add(fkc);
            //fkc.AcceptRejectRule = AcceptRejectRule.None;
            //fkc.DeleteRule = Rule.None;
            //fkc.UpdateRule = Rule.None;

            relation = new DataRelation("FK_Register_WhyDeRegister_WhySecondDeRegisterID",
                WhyDeRegisterDataTable.Columns[0], RegisterDataTable.Columns["WhySecondDeRegisterID"], true);
            DispancerDataSet.Relations.Add(relation);
            #endregion WhyDeRegister Table

            #endregion Register Tables

            #region Invalid Tables
            #region ChiperRecept Table

            DataColumn chiperColumn = new DataColumn("ChiperReceptID");
            chiperColumn.ReadOnly = false;
            chiperColumn.AllowDBNull = false;
            chiperColumn.AutoIncrementSeed = -1;
            chiperColumn.AutoIncrementStep = -1;
            chiperColumn.AutoIncrement = true;
            chiperColumn.DataType = typeof(Int32);
            ChiperReceptDataTable.Columns.Add(chiperColumn);
            ChiperReceptDataTable.PrimaryKey = new[] { chiperColumn };

            chiperColumn = new DataColumn("Name");
            chiperColumn.ReadOnly = false;
            chiperColumn.AllowDBNull = false;
            chiperColumn.DataType = typeof(string);
            chiperColumn.MaxLength = 50;
            chiperColumn.Caption = "Шифр рецепта";
            ChiperReceptDataTable.Columns.Add(chiperColumn);

            chiperColumn = new DataColumn("NotaBene");
            chiperColumn.ReadOnly = false;
            chiperColumn.AllowDBNull = true;
            chiperColumn.DataType = typeof(string);
            chiperColumn.MaxLength = 150;
            chiperColumn.Caption = "Доп сведения";
            ChiperReceptDataTable.Columns.Add(chiperColumn);

            DispancerDataSet.Tables.Add(ChiperReceptDataTable);

            #endregion ChiperRecept Table

            #region DisabilityGroup Table

            DataColumn disabilityColumn = new DataColumn("DisabilityGroupID");
            disabilityColumn.ReadOnly = false;
            disabilityColumn.AllowDBNull = false;
            disabilityColumn.AutoIncrementSeed = -1;
            disabilityColumn.AutoIncrementStep = -1;
            disabilityColumn.AutoIncrement = true;
            disabilityColumn.DataType = typeof(Int32);
            DisabilityGroupDataTable.Columns.Add(disabilityColumn);
            DisabilityGroupDataTable.PrimaryKey = new[] { disabilityColumn };

            disabilityColumn = new DataColumn("Name");
            disabilityColumn.ReadOnly = false;
            disabilityColumn.AllowDBNull = false;
            disabilityColumn.DataType = typeof(string);
            disabilityColumn.MaxLength = 100;
            disabilityColumn.Caption = "Название Группы инвалидности";
            DisabilityGroupDataTable.Columns.Add(disabilityColumn);

            disabilityColumn = new DataColumn("NotaBene");
            disabilityColumn.ReadOnly = false;
            disabilityColumn.AllowDBNull = true;
            disabilityColumn.DataType = typeof(string);
            disabilityColumn.MaxLength = 100;
            chiperColumn.Caption = "Доп сведения";
            DisabilityGroupDataTable.Columns.Add(disabilityColumn);

            DispancerDataSet.Tables.Add(DisabilityGroupDataTable);

            #endregion DisabilityGroup Table


            #region Benefits Table

            DataColumn benefitsColumn = new DataColumn("BenefitsCategoryID");
            benefitsColumn.ReadOnly = true;
            benefitsColumn.AllowDBNull = false;
            benefitsColumn.AutoIncrementSeed = -1;
            benefitsColumn.AutoIncrementStep = -1;
            benefitsColumn.AutoIncrement = true;
            benefitsColumn.DataType = typeof(Int32);
            BenefitsDataTable.Columns.Add(benefitsColumn);
            BenefitsDataTable.PrimaryKey = new[] { benefitsColumn };

            benefitsColumn = new DataColumn("Name");
            benefitsColumn.ReadOnly = false;
            benefitsColumn.AllowDBNull = false;
            benefitsColumn.DataType = typeof(string);
            benefitsColumn.MaxLength = 100;
            benefitsColumn.Caption = "Название льготной категории";
            BenefitsDataTable.Columns.Add(benefitsColumn);

            benefitsColumn = new DataColumn("NotaBene");
            benefitsColumn.ReadOnly = false;
            benefitsColumn.AllowDBNull = true;
            benefitsColumn.DataType = typeof(string);
            benefitsColumn.MaxLength = 100;
            chiperColumn.Caption = "Доп сведения";
            BenefitsDataTable.Columns.Add(benefitsColumn);

            DispancerDataSet.Tables.Add(BenefitsDataTable);

            #endregion Benefits Table

            #region InvalidTable

            DataColumn invalidColumn = new DataColumn("InvalidID");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = false;
            invalidColumn.AutoIncrement = true;
            invalidColumn.AutoIncrementSeed = -1;
            invalidColumn.AutoIncrementStep = -1;
            invalidColumn.DataType = typeof(Int32);
            InvalidDataTable.Columns.Add(invalidColumn);
            InvalidDataTable.PrimaryKey = new DataColumn[1] { invalidColumn };

            invalidColumn = new DataColumn("DisabilityGroupID");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = true;
            invalidColumn.DataType = typeof(Int32);
            invalidColumn.Caption = "ГШруппа инвалидности";
            InvalidDataTable.Columns.Add(invalidColumn);

            invalidColumn = new DataColumn("DataInvalidity");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = true;
            invalidColumn.DataType = typeof(DateTime);
            invalidColumn.Caption = "Дата инвалидности";
            InvalidDataTable.Columns.Add(invalidColumn);

            invalidColumn = new DataColumn("PeriodInvalidity");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = true;
            invalidColumn.DataType = typeof(DateTime);
            invalidColumn.Caption = "Период инвалидности";
            InvalidDataTable.Columns.Add(invalidColumn);

            invalidColumn = new DataColumn("ChiperReceptID");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = true;
            invalidColumn.DataType = typeof(Int32);
            invalidColumn.Caption = "Шифр рецепта";
            InvalidDataTable.Columns.Add(invalidColumn);

            invalidColumn = new DataColumn("Incapable");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = false;
            invalidColumn.DataType = typeof(bool);
            invalidColumn.Caption = "Недееспособен";
            InvalidDataTable.Columns.Add(invalidColumn);

            invalidColumn = new DataColumn("DateIncapable");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = true;
            invalidColumn.DataType = typeof(DateTime);
            invalidColumn.Caption = "Дата недееспособности";
            InvalidDataTable.Columns.Add(invalidColumn);

            invalidColumn = new DataColumn("CustomerID");
            invalidColumn.ReadOnly = false;
            invalidColumn.AllowDBNull = false;
            invalidColumn.DataType = typeof(Int32);
            InvalidDataTable.Columns.Add(invalidColumn);


            this.DispancerDataSet.Tables.Add(InvalidDataTable);

            fkc = new ForeignKeyConstraint("FK_Invalid_Customer_CustomerID",
                CustomerDataTable.Columns[0], InvalidDataTable.Columns["CustomerID"]);
            InvalidDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Invalid_Customer_CustomerID", CustomerDataTable.Columns[0],
                InvalidDataTable.Columns["CustomerID"], false);
            this.DispancerDataSet.Relations.Add(relation);

            fkc = new ForeignKeyConstraint("FK_Invalid_ChiperRecept_ChiperReceptID",
                ChiperReceptDataTable.Columns[0], InvalidDataTable.Columns["ChiperReceptID"]);
            InvalidDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Invalid_ChiperRecept_ChiperReceptID",
                ChiperReceptDataTable.Columns[0], InvalidDataTable.Columns["ChiperReceptID"], false);
            this.DispancerDataSet.Relations.Add(relation);


            fkc = new ForeignKeyConstraint("FK_Invalid_DisabilityGroup_DisabilityGroupID",
                DisabilityGroupDataTable.Columns[0], InvalidDataTable.Columns["DisabilityGroupID"]);
            InvalidDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            relation = new DataRelation("FK_Invalid_DisabilityGroup_DisabilityGroupID",
                DisabilityGroupDataTable.Columns[0], InvalidDataTable.Columns["DisabilityGroupID"], false);
            this.DispancerDataSet.Relations.Add(relation);

            #endregion InvalidTable
            #region InvalidBenefits Table

            DataColumn column = new DataColumn("InvID");
            column.AllowDBNull = false;
            column.DataType = typeof(Int32);
            InvalidBenefitsDataTable.Columns.Add(column);

            column = new DataColumn("BenefitsID");
            column.DataType = typeof(Int32);
            column.AllowDBNull = false;
            InvalidBenefitsDataTable.Columns.Add(column);
            InvalidBenefitsDataTable.PrimaryKey = new DataColumn[]
           {InvalidBenefitsDataTable.Columns[0], InvalidBenefitsDataTable.Columns[1]};
            DispancerDataSet.Tables.Add(InvalidBenefitsDataTable);

            fkc = new ForeignKeyConstraint("FK_InvalidBenefitsCategory_BenefitsCategory_BenefitsCategoryID",
            BenefitsDataTable.Columns[0], InvalidBenefitsDataTable.Columns["BenefitsID"]);
            InvalidBenefitsDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            //relation = new DataRelation("FK_InvalidBenefitsCategory_BenefitsCategory_BenefitsCategoryID",
            //   BenefitsDataTable.Columns[0], InvalidBenefitsDataTable.Columns["BenefitsID"], false);
            //this.DispancerDataSet.Relations.Add(relation);

            fkc = new ForeignKeyConstraint("FK_InvalidBenefitsCategory_Invalid_InvID",
            InvalidDataTable.Columns[0], InvalidBenefitsDataTable.Columns["InvID"]);
            InvalidBenefitsDataTable.Constraints.Add(fkc);
            fkc.AcceptRejectRule = AcceptRejectRule.None;
            fkc.DeleteRule = Rule.Cascade;
            fkc.UpdateRule = Rule.Cascade;

            //relation = new DataRelation("FK_InvalidBenefitsCategory_Invalid_InvID",
            //   InvalidDataTable.Columns[0], InvalidBenefitsDataTable.Columns["InvID"], false);
            //this.DispancerDataSet.Relations.Add(relation);

            #endregion InvalidBenefits Table

            #endregion Invalid Tables 
            CustomerDataTable.ExtendedProperties.Add("Customer", "CustomerData");
            RegisterDataTable.ExtendedProperties.Add("Customer", "CustomerData");
            InvalidDataTable.ExtendedProperties.Add("Customer", "CustomerData");
            AddressDataTable.ExtendedProperties.Add("Customer", "CustomerData");
        }

    }
}
