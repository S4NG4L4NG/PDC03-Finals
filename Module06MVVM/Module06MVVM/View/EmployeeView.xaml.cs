using Module06MVVM.ViewModel;
using Module06MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Module06MVVM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeView : ContentPage
    {
        EmployeeViewModel _viewModel;
        bool _isUpdate;
        int employeeID;

        public EmployeeView()
        {
            InitializeComponent();
            //BindingContext = new EmployeeViewModel();
            _viewModel = new EmployeeViewModel();
            _isUpdate = false;
        }
        public EmployeeView(EmployeeModel obj)
        {
            InitializeComponent();
            _viewModel = new EmployeeViewModel();
            if (obj != null) 
            {
                employeeID = obj.Id;
                txtActionCode.Text = obj.ActionCode;
                txtDescription.Text = obj.Description;

                _isUpdate = true;
            } 

            if (txtCategory.ItemsSource  is IList<string> categories) { 
                txtCategory.SelectedItem = categories.FirstOrDefault(c => c ==obj.Category);
            }

            if (txtImpactLevel.ItemsSource is IList<string> impactlevel)
            {
                txtCategory.SelectedItem = impactlevel.FirstOrDefault(c => c == obj.ImpactLevel);
            }

            if (txtFrequency.ItemsSource is IList<string> frequen)
            {
                txtFrequency.SelectedItem = frequen.FirstOrDefault(c => c == obj.Frequency);
            }
        }
        private async void btnSaveUpdate_Clicked(object sender, EventArgs e) 
        {

            if (string.IsNullOrWhiteSpace(txtActionCode.Text) ||
               string.IsNullOrWhiteSpace(txtDescription.Text) ||
               txtImpactLevel.SelectedItem == null ||
               txtCategory.SelectedItem == null ||
               txtFrequency.SelectedItem == null)
            {
                await DisplayAlert("Validation Error", "All fields are required.", "OK");
                return;
            }

            EmployeeModel obj = new EmployeeModel();
            obj.ActionCode = txtActionCode.Text;
            obj.Description = txtDescription.Text;
            obj.ImpactLevel = (String)txtImpactLevel.SelectedItem;
            obj.Category = (String)txtCategory.SelectedItem;
            obj.Frequency = (String)txtFrequency.SelectedItem;

            if (_isUpdate)
            {
                obj.Id = employeeID;
                await _viewModel.UpdateEmployee(obj);

            }
            else
            {
                _viewModel.InsertEmployee(obj);
            }


            await this.Navigation.PopAsync();
        }

    }
}