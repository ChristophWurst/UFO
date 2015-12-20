using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UFO.BL;
using UFO.BL.execptions;
using UFO.Commander.CustomControls;

namespace UFO.Commander.ViewModels {

	public class LoginViewModel : INotifyPropertyChanged {
		private IBusinessLogic bl;

		public LoginViewModel() {
			bl = BusinessLogicFactory.GetBusinessLogic();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private string usr;

		public string UserName {
			private get { return usr; }
			set {
				if (usr != value) {
					usr = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
				}
			}
		}

		private ICommand loginCommand;

		public ICommand LoginCommand {
			get {
				if (loginCommand == null) {
					loginCommand = new RelayCommand(window => Login(window));
				}
				return loginCommand;
			}
		}

		private void Login(Object window) {
			string Password = "";
			var passwordContainer = window as IHavePassword;
			if (passwordContainer != null) {
				var secureString = passwordContainer.Password;
				Password = ConvertToUnsecureString(secureString);
			}
			try {
				bl.Login(UserName, Password);
				(new UFOWindow()).Show();
				(window as Window).Close();
			} catch (BusinessLogicException e) {
				MessageBox.Show(e.Message, "Error");
			}
		}

		private string ConvertToUnsecureString(SecureString securePassword) {
			if (securePassword == null) {
				return string.Empty;
			}

			IntPtr unmanagedString = IntPtr.Zero;
			try {
				unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
				return Marshal.PtrToStringUni(unmanagedString);
			} finally {
				Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
			}
		}
	}
}