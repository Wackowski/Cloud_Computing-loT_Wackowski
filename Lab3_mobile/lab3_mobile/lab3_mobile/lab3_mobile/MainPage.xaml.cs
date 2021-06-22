using PeopleStoreAppDataCoontracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lab3_mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly IPeopleClient client;
        Person person = new Person();
        public MainPage(IPeopleClient client)
        {
            InitializeComponent();

            this.client = client;
            btnPhoto.Clicked += BtnPhoto_Clicked;
            btnSave.Clicked += BtnSave_Clicked;
            tbxFirstName.TextChanged += TbxFirstName_TextChanged;
            tbxSecondName.TextChanged += TbxSecondName_TextChanged;
            tbxPhone.TextChanged += TbxPhone_TextChanged;
            
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (!Validate())
            {
                await DisplayAlert("Validation Error","Wymagane wszystkie dane","OK");
                return;
            }
            try
            {
                await client.AddPersonAsync(person);
                await DisplayAlert("Sukces", "Dane zapisane", "ok");
                Clear();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ok");
            }
        }

        private void Clear()
        {
            tbxFirstName.Text = string.Empty;
            tbxSecondName.Text = string.Empty;
            tbxPhone.Text = string.Empty;
            imgPhoto.Source = null;
            person = new Person();
        }

        private void TbxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            person.Phone = e.NewTextValue;
        }

        private void TbxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            person.FirstName = e.NewTextValue;
        }

        private void TbxSecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            person.SecondName = e.NewTextValue;
        }

        private async void BtnPhoto_Clicked(object sender, EventArgs e)
        {
            var photo= await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions(){ });
            if (photo == null)
            {
                return;
            }

            imgPhoto.Source = ImageSource.FromStream(() => photo.GetStream());
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                photo.GetStream().CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            person.PictureBase64 = base64;
        }

        private bool Validate()
        {
            return !(string.IsNullOrWhiteSpace(person.FirstName)) || (string.IsNullOrWhiteSpace(person.SecondName)) || (string.IsNullOrWhiteSpace(person.Phone)) || (string.IsNullOrWhiteSpace(person.PictureBase64));
        }
    }
}
