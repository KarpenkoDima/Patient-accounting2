using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BAL.ORM;
using NLog;
using SOPB.Accounting.DAL.ConnectionManager;
using SOPB.GUI.DialogForms;
using SOPB.GUI.Utils;
using static System.Windows.Forms.DialogResult;

namespace SOPB.GUI
{
    public partial class MainForm2 : Form
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
        public MainForm2()
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
            textBoxCustomerNotaBene.DataBindings.Clear();

            textBoxLastName.DataBindings.Add("Text", _customerBindingSource, "LastName");
            textBoxFirstName.DataBindings.Add("Text", _customerBindingSource, "FirstName");
            textBoxMiddleName.DataBindings.Add("Text", _customerBindingSource, "MiddleName");
            maskedTextBoxBirthOfDay.DataBindings.Add("Text", _customerBindingSource, "Birthday");
            textBoxCodeCustomer.DataBindings.Add("Text", _customerBindingSource, "CodeCustomer");
            textBoxMedCard.DataBindings.Add("Text", _customerBindingSource, "MedCard");
            textBoxCustomerNotaBene.DataBindings.Add("Text", _customerBindingSource, "NotaBene");

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
            textBoxRegisterNotaBene.DataBindings.Clear();
            textBoxRegisterNotaBene.DataBindings.Add("Text", _registerBindingSource, "NotaBene");

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

            maskedTextBoxDateIncapable.DataBindings.Clear();
            maskedTextBoxDateIncapable.DataBindings.Add("Text", _invalidBindingSource, "DateIncapable");
            maskedTextBoxDateInvalid.DataBindings.Clear();
            maskedTextBoxDateInvalid.DataBindings.Add("Text", _invalidBindingSource, "DataInvalidity");
            maskedTextBoxPeriodInvalid.DataBindings.Clear();
            maskedTextBoxPeriodInvalid.DataBindings.Add("Text", _invalidBindingSource, "PeriodInvalidity");

            comboBoxCipherRecept.DataSource = _chiperBindingSource;
            comboBoxCipherRecept.ValueMember = "ChiperReceptID";
            comboBoxCipherRecept.DataBindings.Clear();
            comboBoxCipherRecept.DataBindings.Add("SelectedValue", _invalidBindingSource, "ChiperReceptID");
            comboBoxCipherRecept.DisplayMember = "Name";

            checkBoxInCapability.DataBindings.Clear();
            checkBoxInCapability.DataBindings.Add("Checked", _invalidBindingSource, "Incapable");

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
            MainBindingNavigator.BindingSource = _customerBindingSource;
            bindingNavigatorAddNewItem.Enabled = true;
            if (_customerBindingSource.Count > 0)
                isLoadData = true;
            else isLoadData = false;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (!isLoadData)
            {
                try
                {
                    CustomerService service = new CustomerService();
                    BindingData(service.GetGlossaries());
                }
                catch (Exception exception)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Warn(exception.Message);
                    MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }

            textBoxFirstName.Text = textBoxLastName.Text = @"[Новое Имя]";
                comboBoxApppTpr.SelectedIndex = comboBoxGender.SelectedIndex = 0;
                _customerBindingSource.EndEdit();
        }
        private void AddressBindingSourceOnAddingNew(object sender, AddingNewEventArgs e)
        {
            if (_addressBindingSource.List.Count >= 1) return;
            DataView view = _addressBindingSource.List as DataView;

            DataRowView row = view.AddNew();
            row["City"] = "Славянск";
            row["AdminDivisionID"] = 5;
            row["CustomerID"] = ((DataRowView)_customerBindingSource.Current)[0];
            e.NewObject = (object)row;
            _addressBindingSource.MoveLast();
        }

        private void RegisterBindingSourceOnAddingNew(object sender, AddingNewEventArgs e)
        {
            if (_registerBindingSource.List.Count >= 1) return;
            DataView view = _registerBindingSource.List as DataView;

            DataRowView row = view.AddNew();
            row["LandID"] = 1;
            row["CustomerID"] = ((DataRowView)_customerBindingSource.Current)[0];
            e.NewObject = (object)row;
            _registerBindingSource.MoveLast();
        }

        private void BoundChkBoxBenefitsOnLostFocus(object sender, EventArgs e)
        {
            if (isLoadData)
            {
                _invalidBindingSource.EndEdit();
                _benefitsBindingSource.EndEdit();
                _invalidBenefitsBindingSource.EndEdit();
            }
        }
        private void InvalidBindingSourceOnAddingNew(object sender, AddingNewEventArgs e)
        {
            if (_invalidBindingSource.List.Count >= 1) return;
            DataView view = _invalidBindingSource.List as DataView;

            DataRowView row = view.AddNew();
            row["Incapable"] = 1;

            row["CustomerID"] = ((DataRowView)_customerBindingSource.Current)[0];
            e.NewObject = (object)row;
            _invalidBindingSource.MoveLast();
        }

        private void toolStripButtonFillAll_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerService service = new CustomerService();
                if (isLoadData)
                {
                    _customerBindingSource.EndEdit();
                    _addressBindingSource.EndEdit();
                    _registerBindingSource.EndEdit();
                    _invalidBindingSource.EndEdit();
                    _invalidBenefitsBindingSource.EndEdit();
                
                    BindingData(service.UpdateAllCustomers());
                }
                BindingData(service.FillAllCustomers());
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isLoadData)
                {
                    _customerBindingSource.EndEdit();
                    _addressBindingSource.EndEdit();
                    _registerBindingSource.EndEdit();
                    _invalidBindingSource.EndEdit();
                    _invalidBenefitsBindingSource.EndEdit();
                    CustomerService service = new CustomerService();
                    BindingData(service.UpdateAllCustomers());
                }
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void Cusrtomer_Validated(object sender, EventArgs e)
        {
            if (isLoadData)
            {
                _customerBindingSource.EndEdit();
            }
        }
        private void Registred_Validated(object sender, EventArgs e)
        {
            if (isLoadData)
            {
                if (_registerBindingSource.Current == null)
                {
                    string registerText = ((Control)sender).Text;
                    _registerBindingSource.AddNew();
                    ((Control)sender).Text = registerText;
                }
                _registerBindingSource.EndEdit();
            }
        }

        private void maskedTextBoxBirthOfDay_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                Debug.WriteLine("BirthDay");
                if (maskedTextBoxBirthOfDay.MaskedTextProvider != null &&
                    (maskedTextBoxBirthOfDay.MaskedTextProvider.MaskCompleted))
                {
                    if (Utilits.ValidateText(maskedTextBoxBirthOfDay))
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime birthday = DateTime.Parse(maskedTextBoxBirthOfDay.Text);

                        if (birthday > DateTime.Now || birthday <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Error!! Date out of range");
                            ((Control)sender).ResetText();
                            maskedTextBoxBirthOfDay.ForeColor = Color.Red;
                            maskedTextBoxBirthOfDay.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }

                        if (Utilits.ValidateText(maskedTextBoxFirstRegister))
                        {
                            DateTime date = DateTime.Parse(maskedTextBoxFirstRegister.Text);
                            if (date <= birthday)
                            {
                                errorProviderRegDate.SetError((Control)sender,
                                    "Error!! BirthDate grate than or equal first register date. Correct it");
                                maskedTextBoxBirthOfDay.ForeColor = Color.Red;
                                maskedTextBoxBirthOfDay.BackColor = Color.Yellow;
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else
                {
                    ((Control)sender).ResetText();
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxBirthOfDay.ForeColor = DefaultForeColor;
                    maskedTextBoxBirthOfDay.BackColor = Color.White;
                    errorProviderRegDate.SetError(maskedTextBoxBirthOfDay, "");
                }

            }
        }

        private void maskedTextBoxFirstRegister_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                if (errorProviderRegDate.GetError(maskedTextBoxFirstDeRegister).Length != 0)
                {
                    errorProviderRegDate.SetError(maskedTextBoxFirstDeRegister, "");
                }
                if (errorProviderRegDate.GetError(maskedTextBoxSecondDeRegister).Length != 0)
                {
                    errorProviderRegDate.SetError(maskedTextBoxSecondDeRegister, "");
                }
                if (errorProviderRegDate.GetError(maskedTextBoxSecondRegister).Length != 0)
                {
                    errorProviderRegDate.SetError(maskedTextBoxSecondRegister, "");
                }
                Debug.WriteLine("FirstReg Validting");
                if (maskedTextBoxFirstRegister.MaskedTextProvider != null && (maskedTextBoxFirstRegister.MaskedTextProvider.MaskCompleted))
                {
                    if (!Utilits.ValidateText(maskedTextBoxFirstRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка! Значение введёное в поле Ввзят на учёт не является корректной датой. Введите корректную дату в поле Взят на учёт.");
                        ((Control)sender).ResetText();
                        e.Cancel = true;
                    }
                    else
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime date = DateTime.Parse(maskedTextBoxFirstRegister.Text);

                        if (date > DateTime.Now || date <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Ошибка!!! Дата выходит за допустимый диапозон");
                            ((Control)sender).ResetText();
                            maskedTextBoxFirstRegister.ForeColor = Color.Red;
                            maskedTextBoxFirstRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        if (Utilits.ValidateText(maskedTextBoxBirthOfDay))
                        {
                            DateTime birthday = DateTime.Parse(maskedTextBoxBirthOfDay.Text);
                            if (date <= birthday)
                            {
                                errorProviderRegDate.SetError((Control)sender,
                                    "Ошибка!! Дата Взятия на учёт не может быть меньше или равной дате рождения пациента.");
                                ((Control)sender).ResetText();
                                maskedTextBoxFirstRegister.ForeColor = Color.Red;
                                maskedTextBoxFirstRegister.BackColor = Color.Yellow;
                                e.Cancel = true;
                            }
                        }
                        if (Utilits.ValidateText(maskedTextBoxFirstDeRegister))
                        {
                            DateTime postdate = DateTime.Parse(maskedTextBoxFirstDeRegister.Text);
                            if (date >= postdate)
                            {
                                errorProviderRegDate.SetError((Control)sender,
                                    "Ошибка!! Дата Взятия на учёт не может быть больше или равной дате снятия с учёта.");
                                ((Control)sender).ResetText();
                                maskedTextBoxFirstRegister.ForeColor = Color.Red;
                                maskedTextBoxFirstRegister.BackColor = Color.Yellow;
                                e.Cancel = true;
                            }
                        }

                    }
                }
                else
                {
                    if (Utilits.ValidateText(maskedTextBoxFirstDeRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата Взятия на учёт не может отсутствовать если есть дата снятия с учёта");
                        ((Control)sender).ResetText();
                        maskedTextBoxFirstRegister.ForeColor = Color.Red;
                        maskedTextBoxFirstRegister.BackColor = Color.Yellow;
                        e.Cancel = true;
                    }
                    ((Control)sender).ResetText();
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxFirstRegister.BackColor = Color.White;
                    maskedTextBoxFirstRegister.ForeColor = DefaultForeColor;
                    errorProviderRegDate.SetError(maskedTextBoxFirstRegister, "");
                }

            }
        }

        private void maskedTextBoxFirstDeRegister_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                Debug.WriteLine("FirstDe__Reg Validting");
                if ((maskedTextBoxFirstDeRegister.MaskedTextProvider.MaskCompleted))
                {
                    if (!Utilits.ValidateText(maskedTextBoxFirstRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка! Поле Взяти на учёт пустое. Введите сначала дату Взятие на учёт");
                        ((Control)sender).ResetText();
                        e.Cancel = true;
                    }
                    else if (!Utilits.ValidateText(maskedTextBoxFirstDeRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка!!! Значение не является датой.");
                        e.Cancel = true;
                    }
                    else
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime date = DateTime.Parse(maskedTextBoxFirstDeRegister.Text);
                        DateTime preDate = DateTime.Parse(maskedTextBoxFirstRegister.Text);
                        if (date > DateTime.Now || date <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Ошибка!! Дата выходит за допустимый диапозон");
                            ((Control)sender).ResetText();
                            maskedTextBoxFirstDeRegister.ForeColor = Color.Red;
                            maskedTextBoxFirstDeRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        else if (date <= preDate)
                        {
                            errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата Снятие с учёта не может быть меньше или равной дате взятия на учёт.");
                            ((Control)sender).ResetText();
                            maskedTextBoxFirstDeRegister.ForeColor = Color.Red;
                            maskedTextBoxFirstDeRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        else
                        {
                            if (Utilits.ValidateText(maskedTextBoxSecondRegister))
                            {
                                DateTime secondDate = DateTime.Parse(maskedTextBoxSecondRegister.Text);
                                if (date >= secondDate)
                                {
                                    errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата снятия с учёта не может быть больше или равна дате повторного взятия на учёт.");
                                    ((Control)sender).ResetText();
                                    maskedTextBoxFirstDeRegister.ForeColor = Color.Red;
                                    maskedTextBoxFirstDeRegister.BackColor = Color.Yellow;
                                    e.Cancel = true;
                                }
                            }
                            if (Utilits.ValidateText(maskedTextBoxSecondDeRegister))
                            {
                                DateTime seondReDate = DateTime.Parse(maskedTextBoxSecondDeRegister.Text);
                                if (date >= seondReDate)
                                {
                                    errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата снятия с учёта не может быть больше или равна дате повторного снятия с учёта.");
                                    ((Control)sender).ResetText();
                                    maskedTextBoxFirstDeRegister.ForeColor = Color.Red;
                                    maskedTextBoxFirstDeRegister.BackColor = Color.Yellow;
                                    e.Cancel = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Utilits.ValidateText(maskedTextBoxSecondRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата снтия с учёт не может быть пустой если есть дата повторного взтия на учёт.");
                        ((Control)sender).ResetText();
                        maskedTextBoxFirstDeRegister.ForeColor = Color.Red;
                        maskedTextBoxFirstDeRegister.BackColor = Color.Yellow;
                        e.Cancel = true;
                    }
                    ((Control)sender).ResetText();
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxFirstDeRegister.ForeColor = DefaultForeColor;
                    maskedTextBoxFirstDeRegister.BackColor = Color.White;
                    errorProviderRegDate.SetError(maskedTextBoxFirstDeRegister, "");
                }
            }
        }

        private void maskedTextBoxSecondRegister_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                Debug.WriteLine("SecondReg Validting");
                if (maskedTextBoxSecondRegister.MaskedTextProvider != null && (maskedTextBoxSecondRegister.MaskedTextProvider.MaskCompleted))
                {
                    if (!Utilits.ValidateText(maskedTextBoxFirstDeRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка!! Сначало нужно ввести дату в поле Снятие с учёта.");
                        ((Control)sender).ResetText();
                        e.Cancel = true;
                    }
                    else if (!Utilits.ValidateText(maskedTextBoxSecondRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка!!! Значение не является датой.");
                        e.Cancel = true;
                    }
                    else
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime date = DateTime.Parse(maskedTextBoxSecondRegister.Text);
                        DateTime preDate = DateTime.Parse(maskedTextBoxFirstDeRegister.Text);
                        if (date > DateTime.Now || date <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Ошибка!! Дата выходит за допустимый диапозон.");
                            ((Control)sender).ResetText();
                            maskedTextBoxSecondRegister.ForeColor = Color.Red;
                            maskedTextBoxSecondRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        else if (date <= preDate)
                        {
                            errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата меньше или равна дате первый раз снят с учёта.");
                            ((Control)sender).ResetText();
                            maskedTextBoxSecondRegister.ForeColor = Color.Red;
                            maskedTextBoxSecondRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        else
                        {
                            if (Utilits.ValidateText(maskedTextBoxSecondDeRegister))
                            {
                                DateTime secondDate = DateTime.Parse(maskedTextBoxSecondDeRegister.Text);
                                if (date >= secondDate)
                                {
                                    errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата больше или равна дате повторно снят с учёта.");
                                    ((Control)sender).ResetText();
                                    maskedTextBoxSecondRegister.ForeColor = Color.Red;
                                    maskedTextBoxSecondRegister.BackColor = Color.Yellow;
                                    e.Cancel = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Utilits.ValidateText(maskedTextBoxSecondDeRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата не может отсутствовть если есть дата повторного снятия с учёта.");
                        ((Control)sender).ResetText();
                        maskedTextBoxSecondRegister.ForeColor = Color.Red;
                        maskedTextBoxSecondRegister.BackColor = Color.Yellow;
                        e.Cancel = true;
                    }
                    ((Control)sender).ResetText();
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxSecondRegister.BackColor = Color.White;
                    maskedTextBoxSecondRegister.ForeColor = DefaultForeColor;
                    errorProviderRegDate.SetError(maskedTextBoxSecondRegister, "");
                }
            }
        }

        private void maskedTextBoxSecondDeRegister_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                Debug.WriteLine("SecondDe_Reg Validting");
                if (maskedTextBoxSecondDeRegister.MaskedTextProvider != null && (maskedTextBoxSecondDeRegister.MaskedTextProvider.MaskCompleted))
                {
                    if (!Utilits.ValidateText(maskedTextBoxSecondRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка!! Введите сначало дату в поле повторно снят с учёта.");
                        ((Control)sender).ResetText();
                        e.Cancel = true;
                    }
                    else if (!Utilits.ValidateText(maskedTextBoxSecondDeRegister))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка!!! Значние не является датой.");
                        e.Cancel = true;
                    }
                    else
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime date = DateTime.Parse(maskedTextBoxSecondDeRegister.Text);
                        DateTime preDate = DateTime.Parse(maskedTextBoxSecondRegister.Text);
                        if (date > DateTime.Now || date <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Ошибка!! Дата выходит за допустимый диапазон.");
                            ((Control)sender).ResetText();
                            maskedTextBoxSecondDeRegister.ForeColor = Color.Red;
                            maskedTextBoxSecondDeRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        else if (date <= preDate)
                        {
                            errorProviderRegDate.SetError((Control)sender,
                                "Ошибка!! Дата не может быть меньше чем или равна дате повторного взятия на учёт.");
                            ((Control)sender).ResetText();
                            maskedTextBoxSecondDeRegister.ForeColor = Color.Red;
                            maskedTextBoxSecondDeRegister.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }

                    }
                }
                else
                {
                    ((Control)sender).ResetText();
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxSecondDeRegister.ForeColor = DefaultForeColor;
                    maskedTextBoxSecondDeRegister.BackColor = Color.White;
                    errorProviderRegDate.SetError(maskedTextBoxSecondDeRegister, "");
                }
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FindForm find = new FindForm();
                if (find.ShowDialog() == DialogResult.OK)
                {
                    string lName = find.LastName;
                    CustomerService customer = new CustomerService();
                    if (isLoadData)
                    {
                        customer.GetCustomersByLastName(lName);
                    }
                    else BindingData(customer.GetCustomersByLastName(lName));
                }
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ////test
            //// If you are not at the end of the list, move to the next item
            //// in the BindingSource.
            //while (true)
            //{


            //    if (_customerBindingSource.Position + 1 < _customerBindingSource.Count)
            //    {
            //        DataRowView curr = (DataRowView)_customerBindingSource.Current;
            //        string str = curr[3].ToString();
            //        if (str.Length > 0 && str[0]=='\'')
            //        {
            //            str = str.Remove(0, 1);
            //            // str = str.Remove(str.Length - 1);
            //            curr[3] = str;

            //        }
            //        else
            //        {
            //            str = "'"+str;
            //            curr[3] = str;
            //        }

            //        _customerBindingSource.MoveNext();
            //    }
            //    // Otherwise, move back to the first item.
            //    else
            //    {
            //        _customerBindingSource.MoveFirst();
            //        break;
            //    }
            //}
            try
            {
                FindForm find = new FindForm();
                if (find.ShowDialog() == OK)
                {
                    string lName = find.LastName;
                    CustomerService customer = new CustomerService();
                    customer.GetCustomersByLastName(lName);
                }
                // Force the form to repaint.
                this.Invalidate();
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void boundChkBoxBenefits_Validated(object sender, EventArgs e)
        {
            if (isLoadData)
            {
                if (_invalidBindingSource.Current == null)
                {
                    List<int> checkedList = new List<int>(boundChkBoxBenefits.Items.Count);
                    for (int i = 0; i < boundChkBoxBenefits.Items.Count; i++)
                    {
                        if (boundChkBoxBenefits.GetItemChecked(i))
                        {
                            checkedList.Add(i);
                        }
                    }
                    _invalidBindingSource.AddNew();
                    _invalidBindingSource.EndEdit();
                    for (int i = 0; i < checkedList.Count; i++)
                    {
                        boundChkBoxBenefits.SetItemChecked(checkedList[i], true);
                    }

                }
                _invalidBindingSource.EndEdit();
            }
        }

        private void MainForm2_Shown(object sender, EventArgs e)
        {
            
            while (true)
            {
                EnterForm enterForm = new EnterForm();
                DialogResult result = enterForm.ShowDialog();
                if (result == Cancel)
                {
                    this.Close();
                    
                    return;
                }
                if (result == Retry)
                {
                    continue;
                }

                break;
            }
            CustomerService service = new CustomerService();

            //BindingData(service.GetEmptyData());
        }

        private void findByFirstNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FindForm find = new FindForm();
            //find.ShowDialog();
            //string name = find.LastName;
            //CustomerService customer = new CustomerService();
            //BindingData(customer.GetCustomersByFirstName(name));
        }

        private void findByBirthdayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FindByBirthday findByBirthday = new FindByBirthday();
                if (findByBirthday.ShowDialog() == OK)
                {
                    CustomerService customer = new CustomerService();
                    if (isLoadData) customer.GetCustomersByBirthdayWithPredicate(findByBirthday.BithDay, findByBirthday.Predicate);
                    else
                    {
                        BindingData(customer.GetCustomersByBirthdayWithPredicate(findByBirthday.BithDay, findByBirthday.Predicate));
                    }
                   
                }
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void findByAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FindForm find = new FindForm("Адрес");
                if (find.ShowDialog() == OK)
                {
                    string name = find.LastName;
                    CustomerService customer = new CustomerService();
                    if(isLoadData) customer.GetCustomerByAddress(name);
                    else
                    {
                        BindingData(customer.GetCustomerByAddress(name));
                    }
                }
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void findByInvalidsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name =  ((ToolStripMenuItem)sender).Tag.ToString();
            FindByGlossary find = new FindByGlossary(GetSource(name), name);
            find.ShowDialog();
           
            int id = find._ID;
            CustomerService customer = new CustomerService();
            try
            {
                BindingData(customer.GetCustomerByGlossary(name, id));
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void landsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isLoadData)
            {
                CustomerService customer = new CustomerService();
                BindingData(customer.GetGlossaries());
            }
            string glossary = ((ToolStripMenuItem)sender).Tag.ToString();
            DialogResult result = DialogResult.No;
            if (string.Equals(glossary, "regType"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("Тип регистрации", _registerTypeBindingSource);
                result = glossaryForm.ShowDialog();
            }
            else if (string.Equals(glossary, "Disability"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("Группа инвалидности", _disabilityBindingSource);
                result = glossaryForm.ShowDialog();
            }
            else if (string.Equals(glossary, "apppTpr"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("АППП/ТПР", _apppTprBindingSource);
                result = glossaryForm.ShowDialog();
            }
            else if (string.Equals(glossary, "Benefits"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("Льготы", _benefitsBindingSource);
                result = glossaryForm.ShowDialog();
            }
            else if (string.Equals(glossary, "chiperRecept"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("Шифр рецепта", _chiperBindingSource);
                result = glossaryForm.ShowDialog();
            }
            else if (string.Equals(glossary, "Land"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("Участок", _landBindingSource);
                result = glossaryForm.ShowDialog();
            }
            else if (string.Equals(glossary, "whyDeReg"))
            {
                GlossaryForm glossaryForm = new GlossaryForm("Причина снятия с учета", _whyDeRegisterBindingSource);
                result = glossaryForm.ShowDialog();
            }

            if (result == Abort)
            {
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }


        #region My Functions

        private BindingSource GetSource(string name)
        {
            switch (name.ToUpper())
            {
                case "BENEFITSCATEGORY":
                    return _benefitsBindingSource;
                case "LAND":
                    return _landBindingSource;
                case "APPPTPR":
                    return _apppTprBindingSource;

            }

            return _benefitsBindingSource;
        }

        #endregion

        private void exportToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportTo export = new ExportTo(_customerBindingSource);
            export.ShowDialog();
        }

        private void MainForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (isLoadData)
                {
                    _customerBindingSource.EndEdit();
                    _addressBindingSource.EndEdit();
                    _registerBindingSource.EndEdit();
                    _invalidBindingSource.EndEdit();
                    _invalidBenefitsBindingSource.EndEdit();
                    CustomerService service = new CustomerService();
                    service.UpdateAllCustomers();
                }
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.Close();
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (isLoadData)
                {
                    _customerBindingSource.EndEdit();
                    _addressBindingSource.EndEdit();
                    _registerBindingSource.EndEdit();
                    _invalidBindingSource.EndEdit();
                    _invalidBenefitsBindingSource.EndEdit();
                    CustomerService service = new CustomerService();
                    service.UpdateAllCustomers();
                }
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void Address_Validated(object sender, EventArgs e)
        {
            if (isLoadData)
            {
                if (_addressBindingSource.Current == null)
                {
                    string registerText = ((Control)sender).Text;
                    _addressBindingSource.AddNew();
                    ((Control)sender).Text = registerText;
                }
                _addressBindingSource.EndEdit();
            }
        }

        private void Invalid_Validated(object sender, EventArgs e)
        {
            if (isLoadData)
            {
                if (_invalidBindingSource.Current == null)
                {
                    string invalidText = ((Control)sender).Text;
                    _invalidBindingSource.AddNew();
                    ((Control)sender).Text = invalidText;
                }
                _invalidBindingSource.EndEdit();
            }
        }

        private void maskedTextBoxDateInvalid_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                if (errorProviderRegDate.GetError(maskedTextBoxDateInvalid).Length != 0)
                {
                    errorProviderRegDate.SetError(maskedTextBoxDateInvalid, "");
                }
                if (maskedTextBoxDateInvalid.MaskedTextProvider != null && (maskedTextBoxDateInvalid.MaskedTextProvider.MaskCompleted))
                {
                    if (!Utilits.ValidateText(maskedTextBoxDateInvalid))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка! Значение введёное в поле ПРИЗНАН ИНВАЛИДОМ не является корректной датой. Введите корректную дату в поле Взят на учёт.");
                    }
                    else
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime date = DateTime.Parse(maskedTextBoxDateInvalid.Text);

                        if (date > DateTime.Now || date <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Ошибка!!! Дата выходит за допустимый диапозон");
                            ((Control)sender).ResetText();
                            maskedTextBoxDateInvalid.ForeColor = Color.Red;
                            maskedTextBoxDateInvalid.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        if (Utilits.ValidateText(maskedTextBoxDateInvalid))
                        {
                            DateTime birthday = DateTime.Parse(maskedTextBoxBirthOfDay.Text);
                            if (date <= birthday)
                            {
                                errorProviderRegDate.SetError((Control)sender,
                                    "Ошибка!! Дата ПРИЗНАН ИНВАЛИДОМ не может быть меньше или равной дате рождения пациента.");
                                ((Control)sender).ResetText();
                                maskedTextBoxDateInvalid.ForeColor = Color.Red;
                                maskedTextBoxDateInvalid.BackColor = Color.Yellow;
                                e.Cancel = true;
                            }
                        }
                    }
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxDateInvalid.BackColor = Color.White;
                    maskedTextBoxDateInvalid.ForeColor = DefaultForeColor;
                    errorProviderRegDate.SetError(maskedTextBoxDateInvalid, "");
                }
            }
        }

        private void maskedTextBoxPeriodInvalid_Validating(object sender, CancelEventArgs e)
        {
            if (isLoadData)
            {
                if (errorProviderRegDate.GetError(maskedTextBoxPeriodInvalid).Length != 0)
                {
                    errorProviderRegDate.SetError(maskedTextBoxPeriodInvalid, "");
                }
                if (maskedTextBoxPeriodInvalid.MaskedTextProvider != null && (maskedTextBoxPeriodInvalid.MaskedTextProvider.MaskCompleted))
                {
                    if (!Utilits.ValidateText(maskedTextBoxPeriodInvalid))
                    {
                        errorProviderRegDate.SetError((Control)sender, "Ошибка! Значение введёное в поле СРОК ИНВАЛИДНОСТИ не является корректной датой. Введите корректную дату в поле Взят на учёт.");
                    }
                    else
                    {
                        DateTime minDate = new DateTime(1900, 1, 1);
                        DateTime date = DateTime.Parse(maskedTextBoxPeriodInvalid.Text);

                        if (date > DateTime.Now || date <= minDate)
                        {
                            errorProviderRegDate.SetError((Control)sender, "Ошибка!!! Дата выходит за допустимый диапозон");
                            ((Control)sender).ResetText();
                            maskedTextBoxPeriodInvalid.ForeColor = Color.Red;
                            maskedTextBoxPeriodInvalid.BackColor = Color.Yellow;
                            e.Cancel = true;
                        }
                        if (Utilits.ValidateText(maskedTextBoxPeriodInvalid))
                        {
                            DateTime birthday = DateTime.Parse(maskedTextBoxBirthOfDay.Text);
                            if (date <= birthday)
                            {
                                errorProviderRegDate.SetError((Control)sender,
                                    "Ошибка!! Дата СРОК ИНВАЛИДНОСТИ не может быть меньше или равной дате рождения пациента.");
                                ((Control)sender).ResetText();
                                maskedTextBoxPeriodInvalid.ForeColor = Color.Red;
                                maskedTextBoxPeriodInvalid.BackColor = Color.Yellow;
                                e.Cancel = true;
                            }
                        }
                    }
                }
                if (e.Cancel == false)
                {
                    maskedTextBoxPeriodInvalid.BackColor = Color.White;
                    maskedTextBoxPeriodInvalid.ForeColor = DefaultForeColor;
                    errorProviderRegDate.SetError(maskedTextBoxPeriodInvalid, "");
                }
            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerService service = new CustomerService();
            service.ExportToExcel();
        }

        private void toolStripButtonValidation_Click(object sender, EventArgs e)
        {
            _customerBindingSource.EndEdit();
            CustomerService service = new CustomerService();
            service.Validation();           
        }
    }
}
