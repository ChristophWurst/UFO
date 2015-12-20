using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UFO.BL;
using UFO.BL.execptions;
using UFO.DomainClasses;

namespace UFO.Commander.ViewModels {

	internal class ArtistViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private IBusinessLogic bl;

		private Artist Artist;

		public ArtistViewModel(Artist artist) {
			this.Artist = artist;
			bl = BusinessLogicFactory.GetBusinessLogic();
		}

		public int Id {
			get { return Artist.Id; }
			set {
				if (Artist.Id != value) {
					Artist.Id = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Id)));
				}
			}
		}

		public int CategoryId {
			get { return Artist.CategoryId; }
			set {
				if (Artist.CategoryId != value) {
					Artist.CategoryId = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.CategoryId)));
				}
			}
		}

		public int CountryId {
			get { return Artist.CountryId; }
			set {
				if (Artist.CountryId != value) {
					Artist.CountryId = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.CountryId)));
				}
			}
		}

		public String Name {
			get { return Artist.Name; }
			set {
				if (Artist.Name != value) {
					Artist.Name = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Name)));
				}
			}
		}

		public String Image {
			get { return Artist.Image; }
			set {
				if (Artist.Image != value) {
					Artist.Image = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Image)));
				}
			}
		}

		public String Video {
			get { return Artist.Video; }
			set {
				if (Artist.Video != value) {
					Artist.Video = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Video)));
				}
			}
		}

		public String Email {
			get { return Artist.Email; }
			set {
				if (Artist.Email != value) {
					Artist.Email = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Artist.Email)));
				}
			}
		}

		public void SaveArtist() {
			try {
				if (Artist.Id == 0) {
					Artist = bl.CreateArtist(Artist);
				} else {
					bl.UpdateArtist(Artist);
				}
			} catch (BusinessLogicException e) {
				MessageBox.Show(e.Message, "Error");
			}
		}

		public void DeleteArtist() {
			try {
				if (Artist != null && Artist.Id != 0) {
					bl.DeleteArtist(Artist);
				}
			} catch (BusinessLogicException e) {
				MessageBox.Show(e.Message, "Error");
			}
		}
	}
}