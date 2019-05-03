using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AlbumLieux.ViewModels;
using Storm.Mvvm.Forms;

namespace AlbumLieux.Pages
{
	public partial class RegisterPage : BaseContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();
			BindingContext = new RegisterViewModel();
		}
	}
}
