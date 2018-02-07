using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL.ORM;
using SOPB.Accounting.DAL.ConnectionManager;

namespace SOPB.GUI
{
    public partial class MainForm025 : Form
    {
        #region Bindings source

        private BindingSource _customerBindingSource;
        private BindingSource _genderBindingSource;
        private BindingSource _apppTprBindingSource;
        private BindingSource _errorBindingSource;
        private BindingSource _adminDivisionBindingSource;
        private BindingSource _typeStreetBindingSource;
        private BindingSource _addressBindingSource;

        private BindingSource _registerBindingSource;
        private BindingSource _registerTypeBindingSource;
        private BindingSource _secondRegisterTypeBindingSource;
        private BindingSource _whySecondDeRegisterBindingSource;
        private BindingSource _whyDeRegisterBindingSource;
        private BindingSource _landBindingSource;

        private BindingSource _invalidBindingSource;
        private BindingSource _benefitsBindingSource;
        private BindingSource _disabilityBindingSource;
        private BindingSource _chiperBindingSource;
        private BindingSource _invalidBenefitsBindingSource;

        #endregion

        private bool isLoadData = false;
        public MainForm025()
        {
            InitializeComponent();
            Initialize();
            SetDataGridView();
        }
        private void Initialize()
        {
            _customerBindingSource = new BindingSource();
            _apppTprBindingSource = new BindingSource();
            _genderBindingSource = new BindingSource();
            _errorBindingSource = new BindingSource();

            _adminDivisionBindingSource = new BindingSource();
            _typeStreetBindingSource = new BindingSource();
            _addressBindingSource = new BindingSource();

            _registerBindingSource = new BindingSource();
            _registerTypeBindingSource = new BindingSource();
            _secondRegisterTypeBindingSource = new BindingSource();
            _whySecondDeRegisterBindingSource = new BindingSource();
            _whyDeRegisterBindingSource = new BindingSource();
            _landBindingSource = new BindingSource();

            _invalidBindingSource = new BindingSource();
            _benefitsBindingSource = new BindingSource();
            _disabilityBindingSource = new BindingSource();
            _chiperBindingSource = new BindingSource();
            _invalidBenefitsBindingSource = new BindingSource();
            MainBindingNavigator.BindingSource = _customerBindingSource;

            bindingNavigatorAddNewItem.Enabled = false;
            customerDataGridView.RowValidating +=  CustomerDataGridViewOnCellFormatting;
        }
        private void SetDataGridView()
        {
            customerDataGridView.Columns.Clear();
            DataGridViewTextBoxColumn idColumn =
                new DataGridViewTextBoxColumn();
            idColumn.Name = "№ п/п";
            idColumn.DataPropertyName = "CustomerID";
            idColumn.ReadOnly = true;
            customerDataGridView.Columns.Add(idColumn);

            idColumn =
                new DataGridViewTextBoxColumn();
            idColumn.Name = "Фамилия";
            idColumn.DataPropertyName = "LastName";
            idColumn.ReadOnly = true;
            customerDataGridView.Columns.Add(idColumn);

            idColumn =
                new DataGridViewTextBoxColumn();
            idColumn.Name = "Имя";
            idColumn.DataPropertyName = "FirstName";
            idColumn.ReadOnly = true;
            customerDataGridView.Columns.Add(idColumn);

            idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Отчество";
            idColumn.DataPropertyName = "MiddleName";
            idColumn.ReadOnly = true;
            customerDataGridView.Columns.Add(idColumn);

            customerDataGridView.ReadOnly = true;
            customerDataGridView.AutoGenerateColumns = false;
            customerDataGridView.AllowUserToAddRows = customerDataGridView.AllowUserToDeleteRows = false;
        }

        private void BindingData(object data)
        {
            _apppTprBindingSource.DataSource = data;
            _apppTprBindingSource.DataMember = "ApppTpr";

            _genderBindingSource.DataSource = data;
            _genderBindingSource.DataMember = "Gender";

            _customerBindingSource.DataSource = data;
            _customerBindingSource.DataMember = "Customer";

            textBoxLastName.DataBindings.Clear();
            textBoxFirstName.DataBindings.Clear();
            textBoxMiddleName.DataBindings.Clear();
            maskedTextBoxBirthOfDay.DataBindings.Clear();
            textBoxCodeCustomer.DataBindings.Clear();
            textBoxMedCard.DataBindings.Clear();
            textBoxError.DataBindings.Clear();

            textBoxLastName.DataBindings.Add("Text", _customerBindingSource, "LastName");
            textBoxFirstName.DataBindings.Add("Text", _customerBindingSource, "FirstName");
            textBoxMiddleName.DataBindings.Add("Text", _customerBindingSource, "MiddleName");
            maskedTextBoxBirthOfDay.DataBindings.Add("Text", _customerBindingSource, "Birthday");
            textBoxCodeCustomer.DataBindings.Add("Text", _customerBindingSource, "CodeCustomer");
            textBoxMedCard.DataBindings.Add("Text", _customerBindingSource, "MedCard");

            _errorBindingSource.DataSource = _customerBindingSource;
            _errorBindingSource.DataMember = "FK_Error_Customer_CustomerID";
            textBoxError.DataBindings.Add("Text", _errorBindingSource, "Error");

            comboBoxApppTpr.DataSource = _apppTprBindingSource;
            comboBoxApppTpr.ValueMember = "APPPTPRID";
            comboBoxApppTpr.DataBindings.Clear();
            comboBoxApppTpr.DataBindings.Add("SelectedValue", _customerBindingSource, "APPPTPRID");
            comboBoxApppTpr.DisplayMember = "Name";

            comboBoxGender.DataSource = _genderBindingSource;
            comboBoxGender.ValueMember = "GenderID";
            comboBoxGender.DataBindings.Clear();
            comboBoxGender.DataBindings.Add("SelectedValue", _customerBindingSource, "GenderID");
            comboBoxGender.DisplayMember = "Name";

            checkBoxArch.DataBindings.Clear();
            checkBoxArch.DataBindings.Add("Checked", _customerBindingSource, "Arch");
            //////////////////////////////////////////////////////////////////////////////////
            /// 
            _adminDivisionBindingSource.DataSource = data;
            _adminDivisionBindingSource.DataMember = "AdminDivision";
            _typeStreetBindingSource.DataSource = data;
            _typeStreetBindingSource.DataMember = "TypeStreet";

            _addressBindingSource.DataSource = _customerBindingSource;
            _addressBindingSource.DataMember = "FK_Address_Customer_CustomerID";

            //comboBoxAdminDivision.DataSource = adminDivisionBindingSource;
            //comboBoxAdminDivision.ValueMember = "AdminDivisionID";
            //comboBoxAdminDivision.DataBindings.Clear();
            //comboBoxAdminDivision.DataBindings.Add("SelectedValue", addressBindingSource, "AdminDivisionID");
            //comboBoxAdminDivision.DisplayMember = "Name";

            //comboBoxTypeStreet.DataSource = typeStreetBindingSource;
            //comboBoxTypeStreet.ValueMember = "TypeStreetID";
            //comboBoxTypeStreet.DataBindings.Clear();
            //comboBoxTypeStreet.DataBindings.Add("SelectedValue", addressBindingSource, "TypeStreetID");
            //comboBoxTypeStreet.DisplayMember = "Name";

            textBoxNameStreet.DataBindings.Clear();
            textBoxNameStreet.DataBindings.Add("Text", _addressBindingSource, "NameStreet");
            textBoxNumberApartment.DataBindings.Clear();
            textBoxNumberApartment.DataBindings.Add("Text", _addressBindingSource, "NumberApartment");
            textBoxNumberHouse.DataBindings.Clear();
            textBoxNumberHouse.DataBindings.Add("Text", _addressBindingSource, "NumberHouse");
            textBoxCity.DataBindings.Clear();
            textBoxCity.DataBindings.Add("Text", _addressBindingSource, "City");
            //customerBindingSource.AddingNew += CustomerBindingSourceOnAddingNew;
            _addressBindingSource.AddingNew += AddressBindingSourceOnAddingNew;
            //////////////////////////////////////////////////////////////////////////////////////////
            /// Binding data to Register contrls
            _registerTypeBindingSource.DataSource = data;
            _registerTypeBindingSource.DataMember = "RegisterType";

            _secondRegisterTypeBindingSource.DataSource = data;
            _secondRegisterTypeBindingSource.DataMember = "RegisterType";

            _whySecondDeRegisterBindingSource.DataSource = data;
            _whySecondDeRegisterBindingSource.DataMember = "WhyDeRegister";

            _whyDeRegisterBindingSource.DataSource = data;
            _whyDeRegisterBindingSource.DataMember = "WhyDeRegister";

            _landBindingSource.DataSource = data;
            _landBindingSource.DataMember = "Land";

            _registerBindingSource.DataSource = _customerBindingSource;
            _registerBindingSource.DataMember = "FK_Register_Customer_CustomerID";

            maskedTextBoxFirstRegister.DataBindings.Clear();
            maskedTextBoxFirstRegister.DataBindings.Add("Text", _registerBindingSource, "FirstRegister");
            maskedTextBoxFirstDeRegister.DataBindings.Clear();
            maskedTextBoxFirstDeRegister.DataBindings.Add("Text", _registerBindingSource, "FirstDeRegister");
            maskedTextBoxSecondRegister.DataBindings.Clear();
            maskedTextBoxSecondRegister.DataBindings.Add("Text", _registerBindingSource, "SecondRegister");
            maskedTextBoxSecondDeRegister.DataBindings.Clear();
            maskedTextBoxSecondDeRegister.DataBindings.Add("Text", _registerBindingSource, "SecondDeRegister");
            textBoxDiagnosis.DataBindings.Clear();
            textBoxDiagnosis.DataBindings.Add("Text", _registerBindingSource, "Diagnosis");

            comboBoxFirstRegisterType.DataSource = _registerTypeBindingSource;
            comboBoxFirstRegisterType.ValueMember = "RegisterTypeID";
            comboBoxFirstRegisterType.DataBindings.Clear();
            comboBoxFirstRegisterType.DataBindings.Add("SelectedValue", _registerBindingSource, "RegisterTypeID");
            comboBoxFirstRegisterType.DisplayMember = "Name";

            comboBoxSecondRegisterType.DataSource = _secondRegisterTypeBindingSource;
            comboBoxSecondRegisterType.ValueMember = "RegisterTypeID";
            comboBoxSecondRegisterType.DataBindings.Clear();
            comboBoxSecondRegisterType.DataBindings.Add("SelectedValue", _registerBindingSource, "SecondRegisterTypeID");
            comboBoxSecondRegisterType.DisplayMember = "Name";

            comboBoxFirstDeRegisterType.DataSource = _whyDeRegisterBindingSource;
            comboBoxFirstDeRegisterType.ValueMember = "WhyDeREgisterID";
            comboBoxFirstDeRegisterType.DataBindings.Clear();
            comboBoxFirstDeRegisterType.DataBindings.Add("SelectedValue", _registerBindingSource, "WhyDeRegisterID");
            comboBoxFirstDeRegisterType.DisplayMember = "Name";

            comboBoxSecondDeRegisterType.DataSource = _whySecondDeRegisterBindingSource;
            comboBoxSecondDeRegisterType.ValueMember = "WhyDeRegisterID";
            comboBoxSecondDeRegisterType.DataBindings.Clear();
            comboBoxSecondDeRegisterType.DataBindings.Add("SelectedValue", _registerBindingSource,
                "WhySecondDeRegisterID");
            comboBoxSecondDeRegisterType.DisplayMember = "Name";

            comboBoxLand.DataSource = _landBindingSource;
            comboBoxLand.ValueMember = "LandID";
            comboBoxLand.DataBindings.Clear();
            comboBoxLand.DataBindings.Add("SelectedValue", _registerBindingSource, "LandID");
            comboBoxLand.DisplayMember = "Name";
            _registerBindingSource.AddingNew += RegisterBindingSourceOnAddingNew;
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //// Binding data to Invalid contrls  ////
            _benefitsBindingSource.DataSource = data;
            _benefitsBindingSource.DataMember = "BenefitsCategory";
            
            _disabilityBindingSource.DataSource = data;
            _disabilityBindingSource.DataMember = "DisabilityGroup";

            _chiperBindingSource.DataSource = data;
            _chiperBindingSource.DataMember = "ChiperRecept";

            _invalidBindingSource.DataSource = _customerBindingSource;
            _invalidBindingSource.DataMember = "FK_Invalid_Customer_CustomerID";

            _invalidBenefitsBindingSource.DataSource = data;
            _invalidBenefitsBindingSource.DataMember = "InvalidBenefitsCategory";
            comboBoxDisabilityGroup.DataSource = _disabilityBindingSource;
            comboBoxDisabilityGroup.ValueMember = "DisabilityGroupID";
            comboBoxDisabilityGroup.DataBindings.Clear();
            comboBoxDisabilityGroup.DataBindings.Add("SelectedValue", _invalidBindingSource, "DisabilityGroupID");
            comboBoxDisabilityGroup.DisplayMember = "Name";


            comboBoxCipherRecept.DataSource = _chiperBindingSource;
            comboBoxCipherRecept.ValueMember = "ChiperReceptID";
            comboBoxCipherRecept.DataBindings.Clear();
            comboBoxCipherRecept.DataBindings.Add("SelectedValue", _invalidBindingSource, "ChiperReceptID");
            comboBoxCipherRecept.DisplayMember = "Name";

            boundChkBoxBenefits.ChildDisplayMember = "Name";
            boundChkBoxBenefits.ChildValueMember = "BenefitsID";
            boundChkBoxBenefits.ParentValueMember = "InvID";
            boundChkBoxBenefits.ParentIDMember = "InvalidID";
            boundChkBoxBenefits.ChildIDMember = "BenefitsCategoryID";
            boundChkBoxBenefits.ParentDataSource = _invalidBindingSource;
            boundChkBoxBenefits.ChildDataSource = _benefitsBindingSource;
            boundChkBoxBenefits.RelationDataSource = _invalidBenefitsBindingSource;

            boundChkBoxBenefits.LostFocus += BoundChkBoxBenefitsOnLostFocus;

            _invalidBindingSource.AddingNew += InvalidBindingSourceOnAddingNew;

            _customerBindingSource.PositionChanged += (sender, args) =>
            {
                if (_invalidBindingSource.Current == null)
                {
                    for (int i = 0; i < boundChkBoxBenefits.Items.Count; i++)
                    {
                        boundChkBoxBenefits.SetItemChecked(i, false);
                    }
                }
            };

            customerDataGridView.DataSource = _customerBindingSource;
            isLoadData = true;
            bindingNavigatorAddNewItem.Enabled = true;
            //DisableEnableControls(customerBindingSource.Count > 0);
        }

        private void RegisterBindingSourceOnAddingNew(object sender, AddingNewEventArgs e)
        {
            
        }

        private void InvalidBindingSourceOnAddingNew(object sender, AddingNewEventArgs e)
        {
            
        }

        private void BoundChkBoxBenefitsOnLostFocus(object sender, EventArgs e)
        {
            
        }

        private void AddressBindingSourceOnAddingNew(object sender, AddingNewEventArgs e)
        {
            
        }

        private void CustomerDataGridViewOnCellFormatting(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }

        private void toolStripButtonFillAll_Click(object sender, EventArgs e)
        {
           
        }
    }
}
